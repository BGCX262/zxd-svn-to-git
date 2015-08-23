Ext.namespace("Ext.zxd");

Ext.zxd.ZxdDesktop = Ext.extend(Ext.Panel, {
    frame: true,
    floating: true,
    //unstyled: true,
    activeItem: 0,
    deskBtns: undefined,

    constructor: function (config) {

        this.deskBtns = new Ext.ButtonGroup({
            cls: 'x-panel',
            hideBorders: true,
            defaults: {
                enableToggle: true,
                toggleGroup: 'deskbtn',
                width: 30,
                handler: (function (bt) {
                    //                    this.deskBtns.items.each(function (at) {
                    //                        if (bt !== at && at.pressed) at.toggle(false);
                    //                    });

                    this.setActiveItem(bt.index);

                }).createDelegate(this),
                listeners: {
                    afterrender: function (bt) {
                        this.el.on('contextmenu', this.rightMenu);
                    }
                },
                rightMenu: function (e, t, o) {
                    var cmp = Ext.getCmp(this.id);
                    var treeMenu = new Ext.menu.Menu({
                        items: ['<b class="menu-title">桌面设置 : ' + cmp.text + '</b>', '-',{
                            text: '删除'
                        }, {
                            text: cmp.text
                        }]
                    });

                    var coords = this.getBox();
                    treeMenu.showAt([coords.x, coords.y - 90]);
                    e.stopEvent();
                }
            },
            items: [{
                index: 0,
                text: '1',
                pressed: true
            }, {
                index: 1,
                text: '2'
            }, {
                index: 2,
                text: '3'
            }, {
                index: 3,
                text: '4'
            }]
        });

        config.margins = '5 5 5 5';
        config.defaults = {
            cls: 'zxd-desk'
        };
        config.items = [{
            id: 'card-0',
            html: '<h1>Welcome to the Wizard!</h1><p>Step 1 of 3</p>'
        }, {
            id: 'card-1',
            html: '<p>Step 2 of 3</p>'
        }, {
            id: 'card-2',
            html: '<h1>Congratulations!</h1><p>Step 3 of 3 - Complete</p>'
        }, {
            id: 'card-3',
            html: '<h1>Congratulations!</h1><p>Step 4 of 4 - Complete</p>'
        }];
        Ext.zxd.ZxdDesktop.superclass.constructor.call(this, config);

    },
    layout: 'adcard',
    //切换到下个容器
    nextPanel: function () {
        var count = this.items.getCount()
        this.activeItem = this.activeItem >= count - 1 ? 0 : this.activeItem + 1;
        this.getLayout().setActiveItem(this.activeItem);
    },
    //设置当前的容器
    setActiveItem: function (index) {
        this.getLayout().setActiveItem(index);
        this.activeItem = index;
    },
    //获取当前的容器
    getActivePanel: function () {
        return this.getLayout().activeItem;
    },
    getButPanel: function () {
        return this.deskBtns;
    }
});

Ext.reg('zxddesktop', Ext.zxd.ZxdDesktop);

Ext.layout.AdCardLayout = Ext.extend(Ext.layout.CardLayout, {
    type: 'adcard',
    task: new Ext.util.DelayedTask(),
    setActiveItem: function (item) {
        var ai = this.activeItem,
            ct = this.container;
        item = ct.getComponent(item);

        // Is this a valid, different card?
        if (item && ai != item) {

            // Changing cards, hide the current one
            if (ai) {
                ai.el.switchOff({
                    useDisplay: false,
                    remove: false,
                    duration: 0.5
                });
                //                ai.el.ghost({
                //                    duration: 1,
                //                    useDisplay:false,
                //                    callback: function () {
                //                        ai.hide();
                //                        if (ai.hidden !== true) {
                //                            return false;
                //                        }
                //                        ai.fireEvent('deactivate', ai);
                //                    }
                //                });

                //                ai.hide();
                //                if (ai.hidden !== true) {
                //                    return false;
                //                }

                (function () {
                    ai.hide();
                    if (ai.hidden !== true) {
                        return false;
                    }
                    ai.fireEvent('deactivate', ai);
                }).defer(500, this);

            }



            //                            item.show();

            //                            this.layout();

            //                            if (layout) {
            //                                item.doLayout();
            //                            }
            //                            item.fireEvent('activate', item);

            var layout = null;
            this.task.delay(1000, function () {
                layout = item.doLayout && (this.layoutOnCardChange || !item.rendered);

                // Change activeItem reference
                this.activeItem = item;

                // The container is about to get a recursive layout, remove any deferLayout reference
                // because it will trigger a redundant layout.
                delete item.deferLayout;
                item.show();
                item.el.slideIn('r');

                //                item.show();

                //                this.layout();

                //                if (layout) {
                //                    item.doLayout();
                //                }
                //                item.fireEvent('activate', item);
//                this.layout();

//                if (layout) {
//                    item.doLayout();
//                }
//                item.fireEvent('activate', item);
            }, this);

            (function () {


                this.layout();

                if (layout) {
                    item.doLayout();
                }
                item.fireEvent('activate', item);
            }).defer(2000, this);

            // Show the new component

        }
    }
});

Ext.Container.LAYOUTS['adcard'] = Ext.layout.AdCardLayout;