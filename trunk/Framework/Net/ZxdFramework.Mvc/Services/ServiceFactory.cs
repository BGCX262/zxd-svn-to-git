using ZxdFramework.Authorization.Services;

namespace ZxdFramework.Mvc.Services
{
    /// <summary>
    /// 服务工厂类
    /// </summary>
    public static class ServiceFactory
    {
        /// <summary>
        /// 获取权限管理的服务类
        /// </summary>
        public static IAuthorityService AuthorityService
        {
            get { return "AuthorityService".GetInstance<IAuthorityService>(); }
        }
    }
}