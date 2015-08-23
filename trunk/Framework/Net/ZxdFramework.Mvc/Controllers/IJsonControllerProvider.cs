using ZxdFramework.Service;

namespace ZxdFramework.Mvc.Controllers
{
    /// <summary>
    /// 定义支持 Json请求的控制器
    /// </summary>
    public interface IJsonControllerProvider
    {
        /// <summary>
        /// 获取Json请求的数据对象. 如果当前的请求不是Json那么为 Null
        /// </summary>
        /// <value>
        /// The json request.
        /// </value>
        IJsonRequest JsonRequest { set; get; }

        /// <summary>
        /// 返回一个Json对象
        /// </summary>
        /// <value>
        /// The json response.
        /// </value>
        IJsonResponse JsonResponse { set; get; }

    }
}