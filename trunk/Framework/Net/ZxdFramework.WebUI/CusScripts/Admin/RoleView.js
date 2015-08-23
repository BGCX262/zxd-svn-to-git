
Ext.onReady(function () {
    new Ext.Viewport({
        layout: 'border',
        items: [{
            id: 'leftNav',
            region: 'west',
            //xtype: 'box',
            //split: true,
            margins: '5 0 5 0',
            width: 120
        }, {
            region: 'center',
            frame: true,
            //floating: true,
            margins: '5 5 5 5',
            //unstyled: true,
            layout: 'absolute'

        }, {
            region: 'east',
            xtype: 'box',
            margins: '5 0 5 0',
            width: 5
        }, {
            xtype: 'box',
            region: 'south',
            html: '',
            height: 25,
            cls: 'footbar'
        }]
    });

});