/*!
 * Ext JS Library 3.3.1
 * Copyright(c) 2006-2010 Sencha Inc.
 * licensing@sencha.com
 * http://www.sencha.com/license
 */
Ext.onReady(function(){

	Ext.QuickTips.init();

	var xg = Ext.grid;

	//var reader = new Ext.data.JsonReader({
	var reader = new Ext.ux.JsonReader({
	    idProperty: 'ProjectId',
		fields: [
			{name: 'ProjectId', type: 'int'},
			{name: 'Project', type: 'string'},
			{name: 'taskId', type: 'int'},
			{name: 'Description', type: 'string'},
			{name: 'estimate', type: 'float'},
			{name: 'rate', type: 'float'},
			{name: 'cost', type: 'float'},
			{name: 'due', type: 'date', dateFormat:'m/d/Y'}
		],
		// additional configuration for remote
		root:'Data'
	});

//	// define a custom summary function
//	Ext.ux.grid.GroupSummary.Calculations['totalCost'] = function(v, record, field){
//		return v + (record.data.estimate * record.data.rate);
//	};

//	// utilize custom extension for Hybrid Summary
//	var summary = new Ext.ux.grid.HybridSummary();

	var grid = new xg.EditorGridPanel({
		ds: new Ext.data.Store({
			reader: reader,
			// use remote data
			proxy : new Ext.data.HttpProxy({
				url: 'Data',
				method: 'POST'
			})
		}),

		columns: [
			{
				id: 'description',
				header: 'Task',
				width: 80,
				sortable: true,
				dataIndex: 'Description',
				summaryType: 'count',
				hideable: false,
				editor: new Ext.form.TextField({
				   allowBlank: false
				})
			},{
				header: 'Project',
				width: 20,
				sortable: true,
				dataIndex: 'Project'
			}
		],


		frame: true,
		width: 800,
		height: 450,
		clicksToEdit: 1,
		collapsible: true,
		animCollapse: false,
		trackMouseOver: false,
		//enableColumnMove: false,
		title: 'Sponsored Projects',
		iconCls: 'icon-grid',
		renderTo: document.body
	});

	grid.on('afteredit', function(){
		var groupValue = 'Ext Forms: Field Anchoring';
		summary.showSummaryMsg(groupValue, 'Updating Summary...');
		setTimeout(function(){ // simulate server call
			// HybridSummary class implements updateSummaryData
			summary.updateSummaryData(groupValue,
				// create data object based on configured dataIndex
				{description: 22, estimate: 888, rate: 888, due: new Date(), cost: 8});
		}, 2000);
	});

	// load the remote data
	grid.store.load();

});

