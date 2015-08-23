using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using ZxdFramework.Mvc.Config;

namespace ZxdFramework.Mvc.Controllers
{
    /// <summary>
    /// 提取 Spring.net 对象容器中对应的控制器
    /// </summary>
    public class SpringControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// Creates the specified controller by using the specified request context.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>The controller.</returns>
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            if(string.IsNullOrEmpty(controllerName)) throw new ArgumentException("controllerName");
            var springControllerName = (GetArea(requestContext) + controllerName + "Controller").ToLower();
            try
            {
                if (MvcConfig.ControllerContext.ContainsLocalObject(springControllerName))
                {
                    return (IController)MvcConfig.ControllerContext.GetObject(springControllerName);    
                }
                else
                {
                    return base.CreateController(requestContext, controllerName);
                }
            }
            catch(Exception e)
            {
                throw new InvalidOperationException("获取控制器對象失败 : " + controllerName + "\b" + e.Message);
            }
        }

        //public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        //{
        //    return SessionStateBehavior.Default;
        //}

        //public void ReleaseController(IController controller)
        //{
        //    var disposable = controller as IDisposable;
        //    if(disposable != null)
        //    {
        //        disposable.Dispose();
        //    }
        //}

        private string GetArea(RequestContext context)
        {
            if (context.RouteData.DataTokens.ContainsKey("area"))
                return context.RouteData.DataTokens["area"].ToString();
            return string.Empty;
        }
    }
}