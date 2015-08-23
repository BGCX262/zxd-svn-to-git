using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using System.Net.Browser;
using System.Threading;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using ZxdFramework.DataContract;
using ZxdFramework.Service.Events;
using Category = Microsoft.Practices.Prism.Logging.Category;

namespace ZxdFramework.Service
{

    /// <summary>
    /// 定义服务类
    /// </summary>
    public interface IRequestServiceProvider
    {
        /// <summary>
        /// Called when [sent request].
        /// </summary>
        /// <param name="param">The param.</param>
        void OnSentRequest(IRequestParam param);

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        IEventAggregator EventAggregator { get; }

        ///// <summary>
        ///// 设置获取服务器的访问根目录
        ///// </summary>
        ///// <value>
        ///// The server root.
        ///// </value>
        //Uri ServerRoot { set; get; }

        /// <summary>
        /// 获取服务器的 Cookies
        /// </summary>
        CookieContainer Cookies { get; }
    }


    /// <summary>
    /// 请求服务提供类. 负责执行向服务器发送HTTP请求
    /// </summary>
    [Export(typeof(IRequestServiceProvider)), PartCreationPolicy(CreationPolicy.Shared)]
    public class RequestServiceProvider : IRequestServiceProvider
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestServiceProvider"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="logger">The logger.</param>
        [ImportingConstructor]
        public RequestServiceProvider(IEventAggregator eventAggregator, ILoggerFacade logger)
        {
            EventAggregator = eventAggregator;
            Logger = logger;
            WebRequest.RegisterPrefix("http://", WebRequestCreator.ClientHttp);
            WebRequest.RegisterPrefix("https://", WebRequestCreator.ClientHttp);

            Cookies = new CookieContainer();
        }

        /// <summary>
        /// Gets or sets the event aggregator.
        /// </summary>
        /// <value>The event aggregator.</value>
        public IEventAggregator EventAggregator { private set; get; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILoggerFacade Logger { private set; get; }

        //public Uri ServerRoot { set; get; }

        /// <summary>
        /// cookies 容器
        /// </summary>
        /// <value>The cookies.</value>
        public CookieContainer Cookies { private set; get; }

        /// <summary>
        /// Begins the specified @event.
        /// </summary>
        /// <param name="param">The @event.</param>
        public void OnSentRequest(IRequestParam param)
        {
            Logger.Log("开始请求服务器, 请求地址为: " + param.Uri, Category.Debug, Priority.Medium);
            var request = (HttpWebRequest)WebRequest.Create(new Uri(Globals.ServerRoot, param.Uri));
            
            request.CookieContainer = Cookies;
            request.Method = "POST";
            var ar = request.BeginGetRequestStream(RequestCallback, new RequestState(request, param)
                                                                        {
                                                                            Synchronization = SynchronizationContext.Current
                                                                        });
            //ar.AsyncWaitHandle.WaitOne(new TimeSpan(0, 1, 0));
            
        }

        private void RequestCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                var requestState = (RequestState) asynchronousResult.AsyncState;
                requestState.Request.Headers[JsonRequest.JsonRequestHeader] = requestState.RequestParam.GetHeader();
                var stream = requestState.Request.EndGetRequestStream(asynchronousResult);
                using(var writer = new StreamWriter(stream))
                {
                    writer.Write(requestState.RequestParam.Request.JsonData);
                }
                requestState.Request.BeginGetResponse(ResponseCallback, requestState);

            }
            catch (Exception e)
            {
                Logger.Log(e.Message, Category.Exception, Priority.High);
            }
            
        }

        private void ResponseCallback(IAsyncResult asyncResult)
        {
            try
            {
                var requestState = (RequestState) asyncResult.AsyncState;
                var response = (HttpWebResponse)requestState.Request.EndGetResponse(asyncResult);
                //response.Cookies
                Cookies.Add(requestState.Request.RequestUri, response.Cookies);
                requestState.RequestParam.ServiceResponse(JsonResponse.GetJsonResponse(response), requestState.Synchronization);
                Logger.Log("请求执行完成", Category.Debug, Priority.Medium);
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, Category.Exception, Priority.High);
            }
        }


        /// <summary>
        /// 请求状态临时对象
        /// </summary>
        public class RequestState
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="RequestState"/> class.
            /// </summary>
            /// <param name="request">The request.</param>
            /// <param name="jsonRequest">The json request.</param>
            public RequestState(HttpWebRequest request, IRequestParam jsonRequest)
            {
                Request = request;
                RequestParam = jsonRequest;
            }

            /// <summary>
            /// Gets or sets the request.
            /// </summary>
            /// <value>The request.</value>
            public HttpWebRequest Request { private set; get; }

            /// <summary>
            /// Gets or sets the request param.
            /// </summary>
            /// <value>The request param.</value>
            public IRequestParam RequestParam { private set; get; }

            /// <summary>
            /// Gets or sets the synchronization.
            /// </summary>
            /// <value>The synchronization.</value>
            public SynchronizationContext Synchronization { set; get; }
        }
    }
}