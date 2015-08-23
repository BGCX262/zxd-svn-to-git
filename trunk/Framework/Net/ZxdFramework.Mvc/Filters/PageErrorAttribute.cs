using System;
using System.Web.Mvc;
using ZxdFramework.Authorization;
using ZxdFramework.Service;

namespace ZxdFramework.Mvc.Filters
{
    /// <summary>
    /// ҳ������ִ���쳣����
    /// </summary>
    public class PageErrorAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (JsonRequest.CheckJsonRequest(filterContext.HttpContext.Request)) return;

            if (filterContext.Exception is UnauthorizedException)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                filterContext.ExceptionHandled = true;
            }
            else if(filterContext.Exception is SecurityException)
            {
                filterContext.ExceptionHandled = true;
            }

        }
    }
}