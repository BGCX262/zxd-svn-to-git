using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZxdFramework.Authorization;
using ZxdFramework.Mvc.Controllers;
using System.Dynamic;
using ZxdFramework.WebUI.Controllers.Models;

namespace ZxdFramework.WebUI.Controllers
{
    public class ExtController : ZxdFrameworkController
    {

        public ActionResult Config()
        {
            Response.ContentType = "text/javascript";
            
            var model = new ExtScriptConfig
                            {
                                RootPath = Request.ApplicationPath.Length == 1 ? "" : Request.ApplicationPath,
                                CurrentUser = CurrentUser
                            };

            return View(model);
        }

    }
}
