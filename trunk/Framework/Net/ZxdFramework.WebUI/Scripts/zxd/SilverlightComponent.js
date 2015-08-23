/// <reference path="http://localhost/ext-2.2/vswd-ext_2.2.js" />
/// <reference path="Silverlight.js" />

/**
 * Silverlight Component for ExtJS 2.x, 3.x;
 * @class Ext.SilverlightComponent
 * @extends Ext.BoxComponent
 * @constructor
 * @xtype silverlight
 */
Ext.SilverlightComponent = Ext.extend(Ext.BoxComponent, {
	/**
	 * @cfg {String} minRuntimeVersion 
	 * Indicates the earliest Silverlight version that is required
	 * in order to run a Silverlight-based application.
	 */
	minRuntimeVersion: '2.0.30908.0',
	/**
	 * @cfg {String} backgroundColor
	 * The background color of the rectangular region that displays XAML content.
	 */
	backgroundColor: '#FFFFFFFF',
	/**
	 * @cfg {String} url
	 * the Uniform Resource Identifier (URI) of either the initial XAML file (JavaScript API)
	 * or XAP package (managed API) that specifies or contains the content to render.
	 */
	url: undefined,
	/**
	 * @cfg {String} initParams
	 * User-defined initialization parameters.
	 */
	initParams: undefined,
	/**
	 * @cfg {Boolean} windowless
	 * A value that determines whether the Silverlight plug-in displays as a windowless plug-in.
	 * (Applies to Windows versions of Silverlight only.)
	 */
	windowless: true,
	/**
	 * @cfg {Boolean} allowHtmlPopupWindow
	 * A value that indicates whether the hosted content in the Silverlight plug-in can use
	 * the HtmlPage.PopupWindow method to display a new browser window.
	 */
	allowHtmlPopupWindow: false,
	/**
	 * @cfg {Boolean} enableHtmlAccess
	 * A value that indicates whether the hosted content in the Silverlight plug-in
	 * and in the associated run-time code has access to the browser Document Object Model (DOM).
	 */
	enableHtmlAccess: true,
	/**
	 * @cfg {String} xapId
	 * The id attribute value of the generated object element.
	 */
	xapId: undefined,
	/**
	 * @cfg {String} xapWidth
	 * The width attribute value of the generated object element.
	 */
	xapWidth: '100%',
	/**
	 * @cfg {String} xapHeight
	 * The height attribute value of the generated object element.
	 */
	xapHeight: '100%',
	initComponent: function() {
		Ext.SilverlightComponent.superclass.initComponent.call(this);
		this.addEvents('initialize');
	},
	_testXap: function() {
		if (document.getElementById(this.getXapId()).isLoaded == true) {
			this.initXap();
		}
		else {
			this._testXap.defer(50, this);
		}
	},
	onRender: function() {
		Ext.SilverlightComponent.superclass.onRender.apply(this, arguments);
		var xapId = this.getXapId();
		Silverlight.createObject(
			this.url, // xap file url
			this.el.dom, // parent element id
			xapId,
			{
				width: this.xapWidth,
				height: this.xapHeight,
				background: this.backgroundColor,
				minRuntimeVersion: this.minRuntimeVersion,
				windowless: this.windowless === true ? 'true' : 'false',
				allowHtmlPopupWindow: this.allowHtmlPopupWindow,
				enableHtmlAccess: this.enableHtmlAccess
			}, // propties
			{	// onload事件经常不起作用，奇怪。
				//onLoad: onXapLoad
				//onError: onXapError
			}, // events
			this.initParams, // initParams
			this.getId()
		);
		this._testXap();
	},
	getId: function() {
		return this.id || (this.id = 'extslcmp' + (++Ext.Component.AUTO_ID));
	},
	getXapId: function() {
		return this.xapId || (this.xapId = 'extxap' + (++Ext.Component.AUTO_ID));
	},
	initXap: function() {
		this.xap = document.getElementById(this.getXapId());
		this.onXapReady(!!this.isInitialized);
		this.isInitialized = true;
		this.fireEvent('initialize', this);
	},
	onXapReady: Ext.emptyFn
});
Ext.reg('silverlight', Ext.SilverlightComponent);