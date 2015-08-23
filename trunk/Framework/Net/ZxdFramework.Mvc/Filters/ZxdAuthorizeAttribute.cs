using System.Web.Mvc;
using ZxdFramework.Mvc.Services;

namespace ZxdFramework.Mvc.Filters
{
    /// <summary>
    /// ��ܵ�Ȩ����֤
    /// </summary>
    public class ZxdAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = ServiceFactory.AuthorityService.CurrentUser;

            if (user == null)
            {
                user = ServiceFactory.AuthorityService.CreateGuest();
                ServiceFactory.AuthorityService.CurrentUser = user;
            }


        }
    }
}