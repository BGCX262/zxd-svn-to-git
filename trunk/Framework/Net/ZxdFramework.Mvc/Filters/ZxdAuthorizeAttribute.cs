using System.Web.Mvc;
using ZxdFramework.Mvc.Services;

namespace ZxdFramework.Mvc.Filters
{
    /// <summary>
    /// 框架的权限验证
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