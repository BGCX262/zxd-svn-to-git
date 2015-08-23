using System;
using Microsoft.Practices.Prism.Events;

namespace ZxdFramework.DataContract.Events
{
    /// <summary>
    /// ����ϵͳ������ʱ���쳣�¼�����
    /// </summary>
    public class ApplicationErrorEvent : CompositePresentationEvent<ApplicationErrorEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationErrorEvent"/> class.
        /// </summary>
        public ApplicationErrorEvent()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationErrorEvent"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public ApplicationErrorEvent(Exception exception)
        {
            Exception = exception;
        }

        /// <summary>
        /// ��ȡ��ǰ���쳣����
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { set; get; }
    }
}