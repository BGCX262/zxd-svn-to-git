using System.Web.Mvc;
using System.Web.Routing;

namespace ZxdFramework.WebUI.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}.page/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                //,
                //new []{ "ZxdFramework.WebUI.Areas.Admin" }
            );
            //context.Routes.Add(new Route("Admin/{controller}.page/{action}/{id}", 
            //    new MvcRouteHandler())
            //    {
            //        Defaults = new RouteValueDictionary(new { controller = "Home", action = "Index", id = UrlParameter.Optional }),
            //        DataTokens = new RouteValueDictionary(new { area = "Admin", namespaces = new[] { "ZxdFramework.WebUI.Areas.Admin" } }) 
            //    });
        }
    }
}
