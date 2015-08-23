using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZxdFramework.Authorization;
using ZxdFramework.Authorization.Services;
using ZxdFramework.Dao;
using ZxdFramework.DataContract;
using ZxdFramework.Json;
using ZxdFramework.Mvc.Controllers;
using ZxdFramework.Mvc.Services;
using ZxdFramework.WebUI.Controllers.Models;

namespace ZxdFramework.WebUI.Controllers
{
    /// <summary>
    /// 后台管理控制器
    /// </summary>
    public class AccountController : ZxdFrameworkController
    {
        public AccountController(IAuthorityService authorityService, IAuthorizationFactory authorizationFactory)
        {
            AuthorityService = authorityService;
            AuthorizationFactory = authorizationFactory;
        }


        /// <summary>
        /// Gets or sets the authority service.
        /// </summary>
        /// <value>
        /// The authority service.
        /// </value>
        public IAuthorityService AuthorityService { protected set; get; }


        public IAuthorizationFactory AuthorizationFactory { protected set; get; }


        /// <summary>
        /// 后台登陆页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LogOn(string returnUrl)
        {
            ViewData["returnUri"] = returnUrl ?? FormsAuthentication.DefaultUrl;
            return View();
        }

        [HttpPost]
        public object LogOn(LoginModel model)
        {
            return AuthorityService.Logon(model.UserName, model.Password, model.RememberMe);
            //return result;
        }

        public object LogOut()
        {
            return AuthorityService.Logout();
        }

        /// <summary>
        /// 获取根角色
        /// </summary>
        /// <returns></returns>
        public IRole GetRootRole()
        {
            return AuthorityService.GetRootRole();
        }

        /// <summary>
        /// 获取根角色对象. 忽略角色属性中的描述信息
        /// </summary>
        public void GetSystemRootRole()
        {
            JsonResponse.JsonData = new Serializer(AuthorityService.GetRootRole())
                .Ignore("Description")
                .Serialize();
        }

        /// <summary>
        /// 获取系统中使用的节点名称. 忽略了其他属性
        /// </summary>
        public void GetSystemRootSercurityAction()
        {
            JsonResponse.JsonData = new Serializer(AuthorityService.GetRootSecurityAction())
                .Ignore("Icon")
                .Ignore("Name")
                .Ignore("Description")
                .ToString();
        }

        /// <summary>
        /// 获取权限节点的根目录
        /// </summary>
        /// <returns></returns>
        public ISecurityAction GetRootSercurityAction()
        {
            //return AuthorityService.GetRootSecurityAction();
            return AuthorizationFactory.RootAction;
        }

        /// <summary>
        /// 获取当前登录的用户对象
        /// </summary>
        /// <returns></returns>
        public IUser GetCurrentUser()
        {
            return CurrentUser;
        }

        /// <summary>
        /// 获取用户的头像信息
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult GetPhoto(Guid id)
        {
            return File(AuthorityService.GetUserPhoto(id).Data, "image/png");
        }

    }
}
