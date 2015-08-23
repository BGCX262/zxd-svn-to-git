using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using ZxdFramework.Logging.Events;

namespace ZxdFramework.Logging
{

    /// <summary>
    /// �ṩ��־�¼�����ģ��
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
        /// ���û�ȡ��ǰ��־����
        /// <br/>
        /// Debug = 0, Exception = 1, Info = 2, Warn = 3
        /// <br/>
        /// ��־ֻ�ᷢ�����ڻ��ߵ��ڵ�ǰ���͵��¼�.
        /// </summary>
        /// <value>The current category.</value>
        public Category CurrentCategory { set; get; }
    }
}