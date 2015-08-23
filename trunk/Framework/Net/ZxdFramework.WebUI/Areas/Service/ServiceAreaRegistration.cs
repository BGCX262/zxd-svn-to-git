using System.Web.Mvc;

namespace ZxdFramework.WebUI.Areas.Service
{
    public class ServiceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Service";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Service_default",
                "Service/{controller}.json/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
