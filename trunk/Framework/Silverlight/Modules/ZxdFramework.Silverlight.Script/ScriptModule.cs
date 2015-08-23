using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using ZxdFramework.DataContract.System;
using ZxdFramework.Service;

namespace ZxdFramework.Script
{
    [ZxdModuleExport(typeof(ScriptModule))]
    public class ScriptModule : IModule
    {
        private readonly IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public ScriptModule(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            //_eventAggregator.Subscribe<SentRequestEvent>(_serviceProvider.OnSentRequest);
        }


    }
}