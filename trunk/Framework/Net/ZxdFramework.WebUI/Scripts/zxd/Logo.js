/**
* 标志组件
*/

Ext.namespace("Ext.zxd");
/**
 * 项目中的Logo
 * @class Ext.zxd.Logo
 * @extends Ext.BoxComponent
 */
Ext.zxd.Logo = Ext.extend(Ext.BoxComponent, {
    cls: 'logo',
    constructor: function (config) {
        Ext.zxd.Logo.superclass.constructor.call(this, config);
    },
    onRender: function (ct, p) {
        Ext.zxd.Logo.superclass.onRender.call(this, ct, p);
    }
});

Ext.reg('logo', Ext.zxd.Logo);