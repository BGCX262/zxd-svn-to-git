 /// <reference path="../ext-all-debug-w-comments.js" />
Ext.namespace("Ext.zxd");

/**
 * 用户执行命令
 * @class Ext.zxd.Command
 * @extends Ext.util.Observable
 */
Ext.zxd.Command = Ext.extend(Ext.util.Observable, {
	/**
	 * 执行代码
	 * @type Number
	 */
	code:0,
	/**
	 * 发出执行命令的控件
	 * @type Object
	 */
	owner:null,
	/**
	 * 执行完成后调用的方法
	 * @type Function
	 */
	callBack:Ext.emptyFn,
	/**
	 * 执行的方法
	 * @type Function
	 */
	excute:Ext.emptyFn,
	
    constructor: function (config) {
    	
    	Ext.apply(this, config);
    	
        Ext.zxd.Command.superclass.constructor.call(this, config)
        this.addEvents(
        	'executed',
            'beforeExecuted'
        );
    }
});
/**
 * 静态的, 标准执行命令
 * @type 
 */
Ext.zxd.Commands = {
	/**
	 * 执行打开用户管理窗口
	 * @param {} bt
	 */
	doEditMembership:function(bt) {
		var com = new Ext.zxd.Command({
			owner:bt,
			excute:function (com, user) {
		        var winid = '20';
		        var winName = 'editMembership';
		        var win = Ext.WindowMgr.get('_window_' + winid);
		        if (win) {
		            Ext.WindowMgr.bringToFront(winid);
		        }
		        else {
		            win = new Ext.zxd.ViewWindow({ id: '_window_' + winid, animateTarget:com.owner.el });
		            win.show();
		            win.load({
		                url: 'OpenWindow',
		                params: { name: 'editMembership', id: winid },
		                isPage: true,
		                text: '加载程序...',
		                timeout: 30,
		                scripts: true,
		                callback: function () {
		                    eval('win.add(_window_' + winid + ')');
		                    win.doLayout();
		                    eval('delete _window_' + winid);
		                }
		            });
		        }
		    }
		});
		currentUser.doCommand(com);
	}
};


/**
 * 用户对象
 * @class Ext.zxd.User
 * @extends Ext.util.Observable
 */
Ext.zxd.User = Ext.extend(Ext.util.Observable, {
    constructor: function (config) {
        this.addEvents(
	        /*
	         * 
	         */
        	'command',
            'createWindow',
            'quit'
        );
		
        // Call our superclass constructor to complete construction process.
        Ext.zxd.User.superclass.constructor.call(this, config)
    },
    /**
    * 获取用户头像地址
    * @return {String}
    */
    getPhotoUri: function () {
        return Ext.getUri('Account', 'GetPhoto') + '?Id=' + this.Id;
    },
    /**
     * 获取用户显示姓名
     * @return {String}
     */
    getName: function () {
        return this.Name;
    },
    /**
     * 执行界面发出的命令
     * @param {Ext.zxd.Command} com
     */
    doCommand:function(com){
    	com.excute(com, this);
    	com.callBack(com, this);
    	this.fireEvent('command', this, com);
    }
});


var currentUser = new Ext.zxd.User();


Ext.onReady(function () {
    if (_tempUser) {
        Ext.apply(currentUser, _tempUser);
        delete _tempUser;
    }
});
