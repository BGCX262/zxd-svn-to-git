/**
 * 创建者：宗旭东
 *
 */

using System.Web.Mvc;
using NHibernate;
using Spring.Data.NHibernate.Support;

namespace ZxdFramework.Mvc.Filters
{
    /// <summary>
    /// 打开关闭Dao请求Session
    /// </summary>
    public class SessionViewAttribute : ActionFilterAttribute
    {
        private SessionScope _sessionScope;

        public override void  OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            _sessionScope = new SessionScope("MySessionFactory".GetInstance<ISessionFactory>(), true);
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _sessionScope.Close();
            base.OnResultExecuted(filterContext);
        }
    }
}