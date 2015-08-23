
/**
 * 首页布局
 */

var root = null;
Ext.onReady(function () {
    Ext.QuickTips.init();

    var desk = new Ext.zxd.ZxdDesktop({
        id: 'center',
        region: 'center'
    });

    root = new Ext.Viewport({
        layout: 'border',
        items: [{
            height: 80,
            region: 'north',
            xtype: 'container',
            layout: 'column',
            items: [{
                xtype: 'logo',
                width: 250,
                height: 70
            }, {
                //xtype: 'navbuttonlist',
                xtype: 'container',
                columnWidth: 1,
                layout: 'vbox',
                height:80,
                items: [{
                    xtype: 'container',
                    height: 0
                }, {
                    xtype: 'navbuttonlist',
                    height: 80
                }]
            }],
            margins: '0 0 0 0'
        }, {
            id: 'leftNav',
            region: 'west',
            xtype: 'customerpanel',
            split: true,
            floating: true,
            shadow: true,
            margins: '20 0 5 0',
            width: 200,
            layout: 'fit',
            minWidth: 150,
            panel: {
                layout: 'accordion',

                defaults: {
                    // 应用到每个包含的panel
                    bodyStyle: 'padding:15px'
                },
                layoutConfig: {
                    // 这里是布局相关的配置项<
                    titleCollapse: false,
                    animate: true,
                    activeOnTop: true
                },
                items: [{
                    title: '我的收藏',
                    items: [{
                        xtype: 'button',
                        text: 'text',
                        handler: function () {
                            CreateWin();
                        }
                    }, {
                        xtype: 'button',
                        text: 'text2',
                        handler: function () {
                        }
                    }]
                }, {
                    title: '报表系统',
                    html: '<p>Panel content!</p>'
                }, {
                    title: '应用程序',
                    html: '<p>Panel content!</p>'
                }, {
                    title: '我的信息',
                    html: '<p>Panel content!</p>'
                }]

            }
        }, desk, {
            region: 'east',
            xtype: 'box',
            margins: '5 0 5 0',
            width: 5
        }, {
            xtype: 'container',
            region: 'south',
            layout: 'toolbar',
            height: 30,
            cls: 'footbar',
            items: [{ xtype: 'tbspacer', width: 5 }, {
                xtype: 'button',
                text: '导航',
                menu: {
                    width: 150,
                    items: [{ 
                    	text: '用户管理',
                    	handler:Ext.zxd.Commands.doEditMembership
                    }, {
                        text: '系统管理',
                        menu: [{ text: '工程设备' }, { text: 'sdfsdfsdf'}]
                    }, { text: '工程设置'}]
                }
            }, { xtype: 'tbfill' }, desk.getButPanel()]
        }]
    })
});


function CreateWin() {
   win = new Ext.zxd.ViewWindow({
        floating: true,
        plain:true,
        width:500,
        height:300,
        maximizable:true,
        minimizable:true,
        layout:'fit'
    });
    win.show();
    win.load({
        url: 'OpenWindow?name=公安处&id=20',
        scope: win, // optional scope for the callback
        discardUrl: false,
        isPage: true,
        text: 'Loading...',
        timeout: 30,
        scripts: true,
        callback: function () {
            win.add(_window_20);
            win.doLayout();
            delete _window_20;
        }
    });
}