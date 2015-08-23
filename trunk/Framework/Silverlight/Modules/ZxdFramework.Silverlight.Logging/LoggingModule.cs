using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;
using ZxdFramework.DataContract.Events;

namespace ZxdFramework.Logging
{
    /// <summary>
    /// 
    /// </summary>
    [ModuleExport(typeof(LoggingModule))]
    public class LoggingModule : IModule
    {
        private readonly IEventAggregator _eventAggregator;

        private IServiceLocator _serviceLocator;

        /// <summary>
        /// 
        /// </summary>
        [Import]
        public ILoggerFacade Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingModule"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="serviceLocator">The service locator.</param>
        [ImportingConstructor]
        public LoggingModule(IEventAggregator eventAggregator, IServiceLocator serviceLocator)
        {
            _eventAggregator = eventAggregator;
            //serviceLocator.GetInstance()
            _serviceLocator = serviceLocator;
        }

        public void Initialize()
        {
            _eventAggregator.Subscribe<ApplicationErrorEvent>(LoggingError);
        }


        /// <summary>
        /// 处理系统异常
        /// </summary>
        /// <param name="event">The @event.</param>
        public void LoggingError(ApplicationErrorEvent @event)
        {
            Logger.Log(@event.Exception.Message, Category.Exception, Priority.High);
        }
    }
}