using System;
using System.Web.Mvc;
using ZxdFramework.DataContract;
using ZxdFramework.Mvc.Filters;
using ZxdFramework.Mvc.Services;
using ZxdFramework.Service;

namespace ZxdFramework.Mvc.Controllers
{
    /// <summary>
    /// 框架内的控制器的基本类型
    /// </summary>
    [SessionView, JsonError, PageError]
    public abstract class ZxdFrameworkController : Controller, IJsonControllerProvider
    {
        protected ZxdFrameworkController()
        {
            JsonResponse = new JsonResponse();
        }

        /// <summary>
        /// 获取Json请求的数据对象. 如果当前的请求不是Json那么为 Null
        /// </summary>
        /// <value>The json request.</value>
        public IJsonRequest JsonRequest { set; get; }

        public IJsonResponse JsonResponse { set; get; }


        /// <summary>
        /// 获取控制器的执行对象
        /// </summary>
        /// <returns>An action invoker.</returns>
        protected override IActionInvoker CreateActionInvoker()
        {
            return new ZxdControllerActionInvoker();
        }

        /// <summary>
        /// 获取当前的用户
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public IUser CurrentUser { set; get; }
    }

}