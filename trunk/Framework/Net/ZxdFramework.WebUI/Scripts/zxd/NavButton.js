
Ext.namespace("Ext.zxd");
/**
 * 导航按钮
 * @class Ext.zxd.NavButton
 * @extends Ext.BoxComponent
 */
Ext.zxd.NavButton = Ext.extend(Ext.BoxComponent, {
    iconUrl: '',
    text: '',
    Owner: null,
    cls: 'dock-item2',
    autoEl: {
            tag: 'a',
            href: '#'
        },

    onRender: function (ct, position) {

        if (!this.template) {
            if (!Ext.zxd.NavButton.navTemplate) {
                Ext.zxd.NavButton.navTemplate = new Ext.Template(
                        //'<a href="#" class="dock-item2" style="width:40px; left: {2}px;">',
                            '<span style="display: none;">{1}</span>',
                            '<img alt="{1}" src="{0}">'
                        //'</a>'
                    );
                Ext.zxd.NavButton.navTemplate.compile();
            }
            this.template = Ext.zxd.NavButton.navTemplate;

        }

        var nav, targs = this.getTemplateArgs();

        Ext.zxd.NavButton.superclass.onRender.call(this, ct, position);

        nav = this.template.append(this.el, targs, true);

        this.currentTopimg = nav.child('img');

        this.mon(this.el, {
            mouseover: this.onOver.createDelegate(this),
            mouseout: this.onOverOut.createDelegate(this)
        });

    },
    getTemplateArgs: function () {
        var index = this.Owner.items.indexOf(this);

        return [this.iconUrl, this.text, index * 40];
    },
    onOver: function () {

        //this.currentTopimg.setOpacity(0.0, { duration: 0.5, easing: 'easeOut' });
        //this.currentBackimg.setOpacity(1.0, { duration: 0.5, easing: 'easeNone' });
    },
    onOverOut: function () {
        //this.currentBackimg.setOpacity(0.0, { duration: 0.5, easing: 'easeNone' });
        //this.currentTopimg.setOpacity(1.0, { duration: 0.5, easing: 'easeOut' });
    }
});

Ext.reg('navbutton', Ext.zxd.NavButton);