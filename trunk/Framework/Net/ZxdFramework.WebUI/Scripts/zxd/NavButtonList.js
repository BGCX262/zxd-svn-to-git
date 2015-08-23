
Ext.namespace("Ext.zxd");

Ext.zxd.NavButtonList = Ext.extend(Ext.Container, {
    cls: 'dock',
    id: 'dock',
    constructor: function (config) {
        config.items = [{
            xtype: 'container',
            cls: 'dock-container2',
            //layout: 'hbox',
            defaults: { width: 60, cls: 'dock-item2', xtype: 'navbutton', Owner: this },
            items: [{
                iconUrl: '/Styles/lighttheme/Images/calendar.png',
                text: '工程师'
            }, {
                iconUrl: '/Styles/lighttheme/Images/email.png',
                text: '工程师'
            }, {
                iconUrl: '/Styles/lighttheme/Images/history.png',
                text: '工程师'
            }, {
                iconUrl: '/Styles/lighttheme/Images/home.png',
                text: '工程师'
            }, {
                iconUrl: '/Styles/lighttheme/Images/link.png',
                text: '工程师'
            }, {
                iconUrl: '/Styles/lighttheme/Images/music.png',
                text: '工程师'
            }, {
                iconUrl: '/Styles/lighttheme/Images/portfolio.png',
                text: '工程师'
            }, {
                iconUrl: '/Styles/lighttheme/Images/rss.png',
                text: '工程师'
            }, {
                iconUrl: '/Styles/lighttheme/Images/video.png',
                text: '工程师'
            }]
        }];
        Ext.zxd.NavButtonList.superclass.constructor.call(this, config);
        this.on('afterrender', this.doAfterRender);
    },
    doAfterRender: function (ob) {
        $('#dock').Fisheye(
				{
				    maxWidth: 20,
				    items: 'a',
				    itemsText: 'span',
				    container: '.dock-container2',
				    itemWidth: 50,
				    proximity: 70,
				    alignment: 'left',
				    valign: 'bottom',
				    halign: 'center'
				}
			)
    }
});

Ext.reg('navbuttonlist', Ext.zxd.NavButtonList);