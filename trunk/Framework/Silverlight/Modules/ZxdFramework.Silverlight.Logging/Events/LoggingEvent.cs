using System;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

namespace ZxdFramework.Logging.Events
{
    /// <summary>
    /// ��־��¼�¼�
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
        /// ��ȡ��¼��ʼʱ��
        /// </summary>
        /// <value>The time.</value>
        public DateTime Time { private set; get; }

        /// <summary>
        /// ��ȡ��־����
        /// </summary>
        /// <value>The category.</value>
        public Category Category { private set; get; }

        /// <summary>
        /// ��ȡ��־�ȼ�
        /// </summary>
        /// <value>The priority.</value>
        public Priority Priority { private set; get; }

        /// <summary>
        /// ��ȡ��־��Ϣ
        /// </summary>
        /// <value>The message.</value>
        public string Message { private set; get; }


        /// <summary>
        /// ��־��Ϣ��ʽ<br/>
        /// 0 - ʱ��,
        /// 1 - ����,
        /// 2 - �ȼ�,
        /// 3 - ��־����
        /// </summary>
        public static string MessageFormat = "{0},{1},{2}:{3}";

        /// <summary>
        /// ʱ���ʽ
        /// </summary>
        public static string TimeFormat = "HH:mm:ss";

        public override string ToString()
        {
            return string.Format(MessageFormat, Time.ToString(TimeFormat), Category.ToString(), Priority.ToString(), Message);
        }

    }
}