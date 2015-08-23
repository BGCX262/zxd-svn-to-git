using System.Web.Mvc;
using ZxdFramework.Mvc.Controllers;
using ZxdFramework.Service;

namespace ZxdFramework.Mvc.Filters
{
    /// <summary>
    /// 处理执行Json请求框架中抛出的异常. <br/>
    /// 默认会拦截所以Json执行抛出的异常. 并返回前台标准的异常结构到前台.
    /// </summary>
    public class JsonErrorAttribute : FilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// Called when [exception].
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnException(ExceptionContext filterContext)
        {
            if(!JsonRequest.CheckJsonRequest(filterContext.HttpContext.Request)) return;

            filterContext.ExceptionHandled = true;

            if (filterContext.Controller is ZxdFrameworkController)
            {
                var controller = (ZxdFrameworkController) filterContext.Controller;
                if (controller.JsonRequest != null)
                {
                    //处理Json请求异常
                    var response = new JsonResponse {ResultType = ResultType.Error};
                    controller.JsonResponse = response;
                    filterContext.Result = new JsonResult(response);
                }
            }

        }
    }
}