using System.ComponentModel.Composition;
using ZxdFramework.DataContract;
using ZxdFramework.Service.IM;

namespace ZxdFramework.Service.Services
{
    /// <summary>
    /// 即时通讯服务接口
    /// </summary>
    public interface IIMService
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IIMService"/> is inited.
        /// </summary>
        /// <value><c>true</c> if inited; otherwise, <c>false</c>.</value>
        bool Inited { get; }


        /// <summary>
        /// 获取当前即时通讯对象, 只有当前对象初始化后才能获取到用户对象
        /// </summary>
        /// <value>The IM user.</value>
        IMUser IMUser { get; }

        /// <summary>
        /// 初始化服务数据, 如果用户重新登录. 也必须重新调用当前方法
        /// </summary>
        /// <param name="user">登录人员对象</param>
        /// <param name="state">初始时的状态</param>
        void Init(IUser user, States state);
    }


    /// <summary>
    /// 即时通讯服务
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
            //Server.JoinAsync(user.Id, States.在线);
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