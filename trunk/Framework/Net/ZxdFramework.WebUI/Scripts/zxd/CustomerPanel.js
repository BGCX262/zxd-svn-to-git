Ext.namespace("Ext.zxd");
/**
 * 用户的右侧导航的Panel
 * @class Ext.zxd.CustomerPanel
 * @extends Ext.Container
 */
Ext.zxd.CustomerPanel = Ext.extend(Ext.Container, {
    photoEl: undefined,
    userImg: undefined,
    constructor: function (config) {

        //var temp = config.items;

        if (!config.panel) {
            config.panel = {};
        }

        config.cls = 'zxd-user-panel';
        config.items = [{
            cls: 'zxd-user-contain',
            xtype: 'container',
            layout: 'fit',
            items: [config.panel]
        }];

        Ext.zxd.CustomerPanel.superclass.constructor.call(this, config);

    },
    /**
    * @param
    */
    initComponent: function () {
        Ext.zxd.CustomerPanel.superclass.initComponent.call(this);
    },
    /**
    * @private
    */
    afterRender: function () {
        Ext.zxd.CustomerPanel.superclass.afterRender.call(this);

        var t = new Ext.XTemplate('<div class="zxd-photo-div"><div class="RoundedCorner">',
        '<b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4"></b></b>',
        '<div class="user-photo-img">',
            '<img src="{0}" height="64" width="64"/>',
            '</div>',
            '<b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1"></b></b>',
            '</div></div>',
            '<div class="User-Photo-Info">',
            '<p class="User-Info-Name" align="left">{1}</p>',
            '<p class="User-Info-Job" align="left">项目经理</p>',
            '</div>'
        );
        this.photoEl = t.insertFirst(this.el, [currentUser.getPhotoUri(), currentUser.getName()]);
        //this.userImg = this.photoEl.child('img').first();
        var s = new Ext.Shadow({ mode: 'frame' });
        s.setZIndex(1);
        s.show(this.photoEl);
        //        var s = new Ext.Shadow({ mode: 'sides' });
        //        s.show(this.photoEl);
    },

    /**
    * 设置当前用户的头像
    * @public
    * @param {} url
    */
    setUserPhoto: function (url) {
        //this.userImg.dom.src = url;
    }
});

Ext.reg('customerpanel', Ext.zxd.CustomerPanel);