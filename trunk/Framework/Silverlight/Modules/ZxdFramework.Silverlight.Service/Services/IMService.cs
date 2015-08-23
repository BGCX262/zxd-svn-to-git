using System.ComponentModel.Composition;
using ZxdFramework.DataContract;
using ZxdFramework.Service.IM;

namespace ZxdFramework.Service.Services
{
    /// <summary>
    /// ��ʱͨѶ����ӿ�
    /// </summary>
    public interface IIMService
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IIMService"/> is inited.
        /// </summary>
        /// <value><c>true</c> if inited; otherwise, <c>false</c>.</value>
        bool Inited { get; }


        /// <summary>
        /// ��ȡ��ǰ��ʱͨѶ����, ֻ�е�ǰ�����ʼ������ܻ�ȡ���û�����
        /// </summary>
        /// <value>The IM user.</value>
        IMUser IMUser { get; }

        /// <summary>
        /// ��ʼ����������, ����û����µ�¼. Ҳ�������µ��õ�ǰ����
        /// </summary>
        /// <param name="user">��¼��Ա����</param>
        /// <param name="state">��ʼʱ��״̬</param>
        void Init(IUser user, States state);
    }


    /// <summary>
    /// ��ʱͨѶ����
    /// </summary>
    [Export(typeof (IIMService))]
    public class IMService : IIMService
    {
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public IMServiceClient Server { protected set; get; }

        #region IIMService Members

        public bool Inited { protected set; get; }

        public IMUser IMUser { protected set; get; }

        public void Init(IUser user, States state)
        {
            if (!Inited)
            {
                Server = new IMServiceClient();
                Server.ReceiveCommandReceived += Server_ReceiveCommandReceived;
                Server.JoinCompleted += client_JoinCompleted;
            }

            Inited = true;
            //Server.JoinAsync(user.Id, States.����);
        }

        #endregion

        private void client_JoinCompleted(object sender, JoinCompletedEventArgs e)
        {
            IMUser = e.Result;
        }

        private void Server_ReceiveCommandReceived(object sender, ReceiveCommandReceivedEventArgs e)
        {

        }
    }
}