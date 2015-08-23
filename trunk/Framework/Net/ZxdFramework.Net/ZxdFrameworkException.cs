using System;

namespace ZxdFramework
{
    /// <summary>
    /// ��ܵĻ����쳣
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