using System.ServiceModel;

namespace ZxdFramework.DataContract.IM
{
    /// <summary>
    /// 即时通讯客户端
    /// </summary>
    [ServiceContract]
    public interface IIMClient
    {
        /// <summary>
        /// 接收信息
        /// </summary>
        /// <param name="message">The message.</param>
        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(Message message);

        /// <summary>
        /// 接收用户发送的命令
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void ReceiveCommand(IMCommand command);

        /// <summary>
        /// 用户当前状态改变
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void UserChanged(IMUserSetting user);
    }
}