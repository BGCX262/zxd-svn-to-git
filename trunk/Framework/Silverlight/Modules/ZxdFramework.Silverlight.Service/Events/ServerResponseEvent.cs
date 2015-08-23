using System;
using System.Threading;
using Microsoft.Practices.Prism.Events;

namespace ZxdFramework.Service.Events
{

    /// <summary>
    /// ��������������¼�����ӿ�. ��������¼����ڷ�������������֮�󴥷�����. ÿ�εĴ���ͨ�� CurrentState ״̬���Ի�ȡ
    /// </summary>
    public interface IServerResponseEvent
    {
        /// <summary>
        /// ��ȡ��ǰ״̬������
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        ReponseExecuteState CurrentState { get; }

        /// <summary>
        /// ���û�ȡ�Ƿ���ִ�з����󴥷�����¼�
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is sent after event; otherwise, <c>false</c>.
        /// </value>
        bool IsSentAfterEvent { set; get; }
    }

    /// <summary>
    /// ������ִ�з����¼�����
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
        /// ��ȡ�������
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        public IRequestParam Request { protected set; get; }

        /// <summary>
        /// ��ȡ��ǰ״̬������
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        public ReponseExecuteState CurrentState {internal set; get; }

        /// <summary>
        /// ���û�ȡ�Ƿ���ִ�з����󴥷�����¼�
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is sent after event; otherwise, <c>false</c>.
        /// </value>
        public bool IsSentAfterEvent { set; get; }
    }


    /// <summary>
    /// ���巵�ض���ִ�е�״̬
    /// </summary>
    public enum ReponseExecuteState
    {
        /// <summary>
        /// ��ǰ�¼�������ִ�з���֮ǰ
        /// </summary>
        BeforeExecute,
        /// <summary>
        /// ��ǰ�¼�ִ���ڴ�������֮��
        /// </summary>
        AfterExecute
    }
}