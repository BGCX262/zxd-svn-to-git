using System;
using System.Threading;
using Microsoft.Practices.Prism.Events;

namespace ZxdFramework.Service.Events
{

    /// <summary>
    /// 定义服务器返回事件对象接口. 并且这个事件会在服务器返回数据之后触发两次. 每次的触发通过 CurrentState 状态属性获取
    /// </summary>
    public interface IServerResponseEvent
    {
        /// <summary>
        /// 获取当前状态的属性
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        ReponseExecuteState CurrentState { get; }

        /// <summary>
        /// 设置获取是否在执行方法后触发这个事件
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is sent after event; otherwise, <c>false</c>.
        /// </value>
        bool IsSentAfterEvent { set; get; }
    }

    /// <summary>
    /// 服务器执行返回事件对象
    /// </summary>
    public class ServerResponseEvent : CompositePresentationEvent<ServerResponseEvent>, IServerResponseEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerResponseEvent"/> class.
        /// </summary>
        public ServerResponseEvent()
        {
            IsSentAfterEvent = true;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ServerResponseEvent"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        public ServerResponseEvent(IRequestParam request)
        {
            Request = request;
        }


        /// <summary>
        /// 获取请求对象
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        public IRequestParam Request { protected set; get; }

        /// <summary>
        /// 获取当前状态的属性
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        public ReponseExecuteState CurrentState {internal set; get; }

        /// <summary>
        /// 设置获取是否在执行方法后触发这个事件
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is sent after event; otherwise, <c>false</c>.
        /// </value>
        public bool IsSentAfterEvent { set; get; }
    }


    /// <summary>
    /// 定义返回对象执行的状态
    /// </summary>
    public enum ReponseExecuteState
    {
        /// <summary>
        /// 当前事件触发在执行方法之前
        /// </summary>
        BeforeExecute,
        /// <summary>
        /// 当前事件执行在触发方法之后
        /// </summary>
        AfterExecute
    }
}