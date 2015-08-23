namespace ZxdFramework.Authorization
{
    /// <summary>
    /// 用户没有登录异常
    /// </summary>
    public class UnauthorizedException : ZxdAuthorizeExcpetion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
        /// </summary>
        public UnauthorizedException() : base(null)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public UnauthorizedException(string message) : base(message)
        {
        }
    }


    /// <summary>
    /// 安全异常
    /// </summary>
    public class SecurityException : ZxdAuthorizeExcpetion
    {

        public SecurityException() : base(null)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SecurityException(string message) : base(message)
        {
        }
    }
}