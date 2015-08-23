using System.Web.Mvc;
using ZxdFramework.Json;

namespace ZxdFramework.Mvc
{
    /// <summary>
    /// ��չ MVC �еĴ������
    /// </summary>
    public static class MvcHelper
    {
        /// <summary>
        /// ��ȡ����Ľű�������Ϣ
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