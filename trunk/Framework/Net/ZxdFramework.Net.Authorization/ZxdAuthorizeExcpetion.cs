namespace ZxdFramework.Authorization
{
    /// <summary>
    /// 权限验证异常
    /// </summary>
    public class ZxdAuthorizeExcpetion : ZxdFrameworkException
    {

        public ZxdAuthorizeExcpetion(string message) : base(message)
        {
        }
    }
}