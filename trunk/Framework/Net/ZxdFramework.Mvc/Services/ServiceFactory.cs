using ZxdFramework.Authorization.Services;

namespace ZxdFramework.Mvc.Services
{
    /// <summary>
    /// ���񹤳���
    /// </summary>
    public static class ServiceFactory
    {
        /// <summary>
        /// ��ȡȨ�޹���ķ�����
        /// </summary>
        public static IAuthorityService AuthorityService
        {
            get { return "AuthorityService".GetInstance<IAuthorityService>(); }
        }
    }
}