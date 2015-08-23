using System;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

namespace ZxdFramework.Logging.Events
{
    /// <summary>
    /// 日志记录事件
    /// </summary>
    public class LoggingEvent : CompositePresentationEvent<LoggingEvent>
    {
        
        public LoggingEvent()
        {
            Time = DateTime.Now;
        }

        public LoggingEvent(string message, Category category, Priority priority) : this()
        {
            Message = message;
            Category = category;
            Priority = priority;
        }


        /// <summary>
        /// 获取记录开始时间
        /// </summary>
        /// <value>The time.</value>
        public DateTime Time { private set; get; }

        /// <summary>
        /// 获取日志类型
        /// </summary>
        /// <value>The category.</value>
        public Category Category { private set; get; }

        /// <summary>
        /// 获取日志等级
        /// </summary>
        /// <value>The priority.</value>
        public Priority Priority { private set; get; }

        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <value>The message.</value>
        public string Message { private set; get; }


        /// <summary>
        /// 日志消息格式<br/>
        /// 0 - 时间,
        /// 1 - 类型,
        /// 2 - 等级,
        /// 3 - 日志内容
        /// </summary>
        public static string MessageFormat = "{0},{1},{2}:{3}";

        /// <summary>
        /// 时间格式
        /// </summary>
        public static string TimeFormat = "HH:mm:ss";

        public override string ToString()
        {
            return string.Format(MessageFormat, Time.ToString(TimeFormat), Category.ToString(), Priority.ToString(), Message);
        }

    }
}