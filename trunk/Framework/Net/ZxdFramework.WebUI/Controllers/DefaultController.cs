using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZxdFramework.Mvc.Controllers;
using ZxdFramework.Mvc.Filters;

namespace ZxdFramework.WebUI.Controllers
{
    public class DefaultController : ZxdFrameworkController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

    }
}
