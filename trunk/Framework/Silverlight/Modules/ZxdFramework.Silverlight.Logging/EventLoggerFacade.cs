using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using ZxdFramework.Logging.Events;

namespace ZxdFramework.Logging
{

    /// <summary>
    /// 提供日志事件触发模型
    /// </summary>
    [Export("loggerFacade", typeof(ILoggerFacade)), PartCreationPolicy(CreationPolicy.Shared)]
    public class EventLoggerFacade: ILoggerFacade
    {
        [Import]
        public Lazy<IEventAggregator> EventAggregator;

        public EventLoggerFacade()
        {
            CurrentCategory = Category.Warn;
        }


        public void Log(string message, Category category, Priority priority)
        {
            if(category >= CurrentCategory)
            {
                EventAggregator.Value.Publish(new LoggingEvent(message, category, priority));
            }
        }

        /// <summary>
        /// 设置获取当前日志类型
        /// <br/>
        /// Debug = 0, Exception = 1, Info = 2, Warn = 3
        /// <br/>
        /// 日志只会发出大于或者等于当前类型的事件.
        /// </summary>
        /// <value>The current category.</value>
        public Category CurrentCategory { set; get; }
    }
}