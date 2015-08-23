using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ZxdFramework.DataContract;
using ZxdFramework.Mvc.Config;
using ZxdFramework.WebUI.Config;

namespace ZxdFramework.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Json", // Route name
            //    "{controller}.json/{action}/{id}",
            //    new { controller = "Default", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //    );

            routes.MapRoute(
                "Page", // Route name
                "{controller}.page/{action}/{id}", // URL with parameters
                new { controller = "Default", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.Add(new Route("{controller}.json/{action}/{id}",
                new MvcRouteHandler())
                {
                    Defaults = new RouteValueDictionary(new { controller = "Home", action = "Index", id = UrlParameter.Optional })
                });
        }

        protected void Application_Start()
        {
            Globals.IsDebug = true;

            //测试工具
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            ControllerBuilder.Current.DefaultNamespaces.Add("ZxdFramework.WebUI");
            ControllerBuilder.Current.DefaultNamespaces.Add("ZxdFramework.Mvc");
            new AspMvcConfig();
            

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            AreaRegistration.RegisterAllAreas();

        }
    }
}