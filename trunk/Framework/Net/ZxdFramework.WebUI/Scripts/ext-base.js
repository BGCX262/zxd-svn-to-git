/**
 * 定义系统中使用的基本对象
 */


/**
 * 请求对象
 */
JsonRequest = function (config) {
	this.command = '';
	this.contextLenght = -1;
	this.isEncrypt = false;
	this.dataType = '';
	this.category = JsonRequest.Data;
	this.userId = '';

	Ext.apply(this, config);
}
/**
 * 静态属性, 设置请求为数据类型
 * @type Number
 */
JsonRequest.Data = 1,
/**
 * 请求为只读类型
 * @type Number
 */
JsonRequest.Readonly = 2;
/**
 * 请求具有删除操作类型
 * @type Number
 */
JsonRequest.Delete = 3;
/**
 * 请求具体有更新操作
 * @type Number
 */
JsonRequest.Update = 4;
/**
 * 未知的请求
 * @type Number
 */
JsonRequest.Unkonw = 5;

JsonRequest.prototype = {
		/**
		 * 设置请求命令
		 * @type String
		 */
		command : '',
		/**
		 * 设置请求内容的长度
		 * @type Number
		 */
		contextLenght : 0,
		/**
		 * 是否加密当前的请求内容
		 * @type Boolean
		 */
		isEncrypt : false,
		/**
		 * 请求数据类型
		 * @type String
		 */
		dataType : '',
		/**
		 * 请求类型
		 * @type String
		 */
		category : 1,
		/**
		 * 请求用户ID
		 * @type String
		 */
		userId : '',
		/**
		 * 请求对象
		 * @type 
		 */
		param : null,
		/**
		 * 获取请求的头部信息
		 * @return String
		 */
		getHeader : function() {
			var p = this.param;
			delete this.param;
			//this.ContextLenght = 
			var header = Ext.encode(this);
			this.param = p;
			return header;
		}
}

/**
 * 请求返回对象
 * @param {} response
 */
JsonResponse = function (response) {

	var header = response.getResponseHeader('_jsonresponseheader');

	var text = response.responseText;

	var data = Ext.decode(header);
	
	Ext.apply(this, data);
}


JsonResponse.prototype = {
	
};

/**
 * 请求之前执行函数
 * @param {} conn
 * @param {} op
 */		
function beforeRequest (conn, op) {

    if (op.isPage) return;

		var re;

		if(op.jsonData != null) {
			re = new JsonRequest();
			re.param = op.jsonData;
		}
		else if(op.params instanceof JsonRequest) {
			re = op.params;
		}
		else {
			re = new JsonRequest();
			re.param = op.params;
        }
        delete op.params;
		op.headers = { '_jsonrequestheader': re.getHeader() };
		op.jsonData = re.param;
}

/**
 * 
 * @param {} conn
 * @param {} response
 * @param {} options
 */
function requestComplete(conn, response, options) {
	response.header = new JsonResponse(response);
}

Ext.Ajax.on('beforerequest', beforeRequest);
Ext.Ajax.on('requestcomplete', requestComplete);

Ext.namespace("Ext.ux");

/**
 * 服务器数据读取对象
 * @param {} config
 */
Ext.ux.JsonReader = function (config) {
	Ext.ux.JsonReader.superclass.constructor.call(this, config);
}

/**
 * 
 * @class Ext.ux.JsonReader
 * @extends Ext.data.JsonReader
 */
Ext.extend(Ext.ux.JsonReader, Ext.data.JsonReader, {
	/**
	 * 
	 * @type JsonResponse 
	 */
	JsonResponse: null,

	/**
	 * 获取返回对象
	 * @return {JsonResponse}
	 */
	getJsonResponse: function () {
		return this.JsonResponse;
	},

	/**
	 * 
	 * @param {} response
	 * @param {} op
	 * @return {}
	 */
	read: function (response, op) {

		this.JsonResponse = response.header == null ? new JsonResponse(response) : response.header;

		var json = response.responseText;
		var o = Ext.decode(json);
		if (!o) {
			throw { message: 'JsonReader.read: Json object not found' };
		}
		return this.readRecords(o);
	},


	readResponse: function (action, response, op) {
		this.JsonResponse = response.header == null ? new JsonResponse(response) : response.header;
		
		var o = (response.responseText !== undefined) ? Ext.decode(response.responseText) : response;
		if (!o) {
			throw new Ext.data.JsonReader.Error('response');
		}

		var root = this.getRoot(o);
		if (action === Ext.data.Api.actions.create) {
			var def = Ext.isDefined(root);
			if (def && Ext.isEmpty(root)) {
				throw new Ext.data.JsonReader.Error('root-empty', this.meta.root);
			}
			else if (!def) {
				throw new Ext.data.JsonReader.Error('root-undefined-response', this.meta.root);
			}
		}

		// instantiate response object
		var res = new Ext.data.Response({
			action: action,
			success: this.getSuccess(o),
			data: (root) ? this.extractData(root, false) : [],
			message: this.getMessage(o),
			raw: o
		});

		// blow up if no successProperty
		if (Ext.isEmpty(res.success)) {
			throw new Ext.data.JsonReader.Error('successProperty-response', this.meta.successProperty);
		}
		return res;
	}

});
