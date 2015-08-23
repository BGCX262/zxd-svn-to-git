using System.Web.Mvc;
using ZxdFramework.Mvc.Controllers;
using ZxdFramework.Service;

namespace ZxdFramework.Mvc.Filters
{
    /// <summary>
    /// ����ִ��Json���������׳����쳣. <br/>
    /// Ĭ�ϻ���������Jsonִ���׳����쳣. ������ǰ̨��׼���쳣�ṹ��ǰ̨.
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
                    //����Json�����쳣
                    var response = new JsonResponse {ResultType = ResultType.Error};
                    controller.JsonResponse = response;
                    filterContext.Result = new JsonResult(response);
                }
            }

        }
    }
}