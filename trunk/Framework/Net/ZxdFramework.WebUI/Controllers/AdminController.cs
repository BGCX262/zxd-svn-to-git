using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZxdFramework.Authorization.Services;
using ZxdFramework.Dao;
using ZxdFramework.Mvc.Controllers;
using ZxdFramework.Mvc.Services;
using ZxdFramework.WebUI.Controllers.Models;

namespace ZxdFramework.WebUI.Controllers
{
    /// <summary>
    /// 后台管理控制器
    /// </summary>
    public class AdminController : ZxdFrameworkController
    {
        public AdminController(IAuthorityService authorityService)
        {
            AuthorityService = authorityService;
        }


        /// <summary>
        /// Gets or sets the authority service.
        /// </summary>
        /// <value>
        /// The authority service.
        /// </value>
        public IAuthorityService AuthorityService { protected set; get; }


        /// <summary>
        /// 后台登陆页面
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOn(string returnUri)
        {
            ViewData["returnUri"] = returnUri ?? "/Home.page/Index";
            return View();
        }

        [HttpPost]
        public object LogOn(LoginModel model)
        {
            dynamic result = new System.Dynamic.ExpandoObject();

            result.success = AuthorityService.Logon(model.UserName, model.Password, model.RememberMe);
            return result;
        }


        public object LogOut()
        {
            return AuthorityService.Logout();
        }

    }
}
