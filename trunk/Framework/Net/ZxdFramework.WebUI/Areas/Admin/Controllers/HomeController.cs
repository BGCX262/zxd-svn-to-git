using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZxdFramework.Authorization;
using ZxdFramework.Mvc.Controllers;

namespace ZxdFramework.WebUI.Areas.Admin.Controllers
{
    public class HomeController : ZxdFrameworkController
    {
        //
        // GET: /Admin/Home/
        /// <summary>
        /// 后台管理主页
        /// </summary>
        /// <returns></returns>
        [Security]
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 打开一个窗口视图
        /// </summary>
        /// <param name="name">视图名称</param>
        /// <param name="id">视图主键</param>
        /// <returns></returns>
        public ActionResult OpenWindow(string name, string id)
        {
            ViewData["name"] = name;
            ViewData["id"] = id;
            ViewData["var"] = "_window_" + id;
            return View("EditMembership");
        }

    }
}
