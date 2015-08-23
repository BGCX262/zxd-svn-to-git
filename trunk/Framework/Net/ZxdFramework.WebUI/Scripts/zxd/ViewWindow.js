Ext.namespace("Ext.zxd");

Ext.WindowMgr.zseed = 100000;
/**
 * 打开的窗口
 * @class Ext.zxd.ViewWindow
 * @extends Ext.Window
 */
Ext.zxd.ViewWindow = Ext.extend(Ext.Window, {
    maximizable: true,
    minimizable: true,
    floating: true,
	plain:true,
	width:500,
	height:300,
	maximizable:true,
	minimizable:true,
	layout:'fit',
    initComponent: function () {
        Ext.zxd.ViewWindow.superclass.initComponent.call(this);
        this.mon(this, {
            afterrender: function (ob) {
                this.el.on('mousemove', function (e, t, o) {
                    e.stopEvent();
                });
            },
            maximize: function (ob) {
            }
        });
    },
    fitContainer: function () {
        var vs = this.container.getViewSize(false);
        this.setSize(vs.width, vs.height - 32);
    }
});

Ext.reg('viewwindow', Ext.zxd.ViewWindow);