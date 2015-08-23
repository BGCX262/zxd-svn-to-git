using System;
using Microsoft.Practices.Prism.Events;

namespace ZxdFramework.DataContract.Events
{
    /// <summary>
    /// 拦截系统中运行时的异常事件对象
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
        /// 获取当前的异常对象
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { set; get; }
    }
}