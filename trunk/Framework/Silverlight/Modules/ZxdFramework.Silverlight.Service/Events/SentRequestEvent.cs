using System;
using System.ComponentModel.Composition;
using System.Threading;
using Microsoft.Practices.Prism.Events;
using ZxdFramework.DataContract;

namespace ZxdFramework.Service.Events
{
    /// <summary>
    /// �����ִ����ɺ�ִ�еĴ�����
    /// </summary>
    /// <typeparam name="T">��������</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="param">The sent event.</param>
    public delegate void ServiceCompleted<in T>(T result, IRequestParam param);

    /// <summary>
    ///
    /// </summary>
    /// <param name="param">The param.</param>
    public delegate void ServiceCompleted(IRequestParam param);

    /// <summary>
    /// ���������ģ�Ͷ���
    /// </summary>
    public class SentRequestEvent : CompositePresentationEvent<SentRequestEvent>, IRequestParam
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SentRequestEvent"/> class.
        /// </summary>
        public SentRequestEvent()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SentRequestEvent"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="request">The request.</param>
        public SentRequestEvent(string uri, IJsonRequest request)
        {
            RequestTime = DateTime.Now;
            Request = request;
            Uri = uri;
        }

        /// <summary>
        /// ���û�ȡ�������
        /// </summary>
        /// <value>The request.</value>
        public IJsonRequest Request { set; get; }


        /// <summary>
        /// ��ȡӦ�����
        /// </summary>
        /// <value>The response.</value>
        public IJsonResponse Response { protected set; get; }


        /// <summary>
        /// ���û�ȡ������ɵ�ִ�з���
        /// </summary>
        /// <value>The result.</value>
        public Action<SentRequestEvent, IJsonResponse> Result { set; get; }
        //public ac Completed { set; get; }

        /// <summary>
        /// ���û�ȡ�����ַ
        /// </summary>
        /// <value>The URI.</value>
        public string Uri { set; get; }


        /// <summary>
        /// Gets or sets the request time.
        /// </summary>
        /// <value>
        /// The request time.
        /// </value>
        public DateTime RequestTime { protected set; get; }

        /// <summary>
        /// Gets or sets the response time.
        /// </summary>
        /// <value>
        /// The response time.
        /// </value>
        public DateTime ResponseTime { protected set; get; }

        /// <summary>
        /// �������ִ���¼�
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="requestSynchronizationContext">ִ�е��߳�</param>
        public void ServiceResponse(IJsonResponse response, SynchronizationContext requestSynchronizationContext)
        {
            ResponseTime = DateTime.Now;
            Response = response;

            var reponseEvent = new ServerResponseEvent(this)
                                   {
                                       CurrentState = ReponseExecuteState.BeforeExecute
                                   };
             Globals.EventAggregator.Publish(reponseEvent);
            
            if(Result != null)
                requestSynchronizationContext.Post(e => Result(this, response), null);

            if(reponseEvent.IsSentAfterEvent)
                Globals.EventAggregator.Publish(reponseEvent);
            
        }

        /// <summary>
        /// ��ȡ�ض������ݱ�ͷ
        /// </summary>
        /// <returns></returns>
        public string GetHeader()
        {
            return Request.ToString();
        }


        #region ��̬����
        /// <summary>
        /// Creates the request event.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public static SentRequestEvent CreateRequestEvent(string uri, object param)
        {
            return new SentRequestEvent(uri, JsonRequest.CreateJsonRequest(param));
        }

        /// <summary>
        /// Creates the request event.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="jsonData">The json data.</param>
        /// <returns></returns>
        public static SentRequestEvent CreateRequestEvent(string uri, string jsonData)
        {
            return new SentRequestEvent(uri, JsonRequest.CreateJsonRequest(jsonData));
        }

        /// <summary>
        /// Creates the request event.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static SentRequestEvent CreateRequestEvent(string uri)
        {
            return new SentRequestEvent(uri, JsonRequest.CreateJsonRequest(null));
        }
        #endregion
    }
}