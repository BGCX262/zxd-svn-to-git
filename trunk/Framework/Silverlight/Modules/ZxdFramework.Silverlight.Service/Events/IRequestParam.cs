using System;
using System.Threading;

namespace ZxdFramework.Service.Events
{
    /// <summary>
    /// 定义请求对象
    /// </summary>
    public interface IRequestParam
    {
        /// <summary>
        /// 获取请求对象
        /// </summary>
        IJsonRequest Request { get; }

        /// <summary>
        /// 获取返回对象
        /// </summary>
        IJsonResponse Response { get; }

        /// <summary>
        /// 获取请求地址
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        string Uri { set; get; }

        /// <summary>
        /// 获取请求时间
        /// </summary>
        DateTime RequestTime { get; }

        /// <summary>
        /// 获取返回时间
        /// </summary>
        DateTime ResponseTime { get; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        Action<SentRequestEvent, IJsonResponse> Result { set; get; }

        /// <summary>
        /// 执行请求方法
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="requestSynchronizationContext">The request synchronization context.</param>
        void ServiceResponse(IJsonResponse response, SynchronizationContext requestSynchronizationContext);

        /// <summary>
        /// 获取请求的头部信息
        /// </summary>
        /// <returns></returns>
        string GetHeader();
    }
}