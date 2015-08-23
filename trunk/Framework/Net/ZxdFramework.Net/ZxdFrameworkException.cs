using System;

namespace ZxdFramework
{
    /// <summary>
    /// 框架的基本异常
    /// </summary>
    public class ZxdFrameworkException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZxdFrameworkException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ZxdFrameworkException(string message) : base(message)
        {
            
        }
    }
}