using System.Web.Mvc;
using ZxdFramework.Json;

namespace ZxdFramework.Mvc
{
    /// <summary>
    /// 扩展 MVC 中的代码帮助
    /// </summary>
    public static class MvcHelper
    {
        /// <summary>
        /// 获取对象的脚本描述信息
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static MvcHtmlString GetObjectScript(this HtmlHelper htmlHelper, object obj)
        {
            return new MvcHtmlString(JsonConverter.Serialize(obj));
        }
    }
}