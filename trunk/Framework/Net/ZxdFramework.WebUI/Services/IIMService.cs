using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ZxdFramework.DataContract.IM;

namespace ZxdFramework.WebUI.Services
{
    /// <summary>
    /// 即时通讯协议接口
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IIMClient))]
    public interface IIMService
    {
        /// <summary>
        /// 加入即时通讯
        /// </summary>
        /// <param name="userid">用户编码</param>
        /// <param name="state">The state.</param>
        [OperationContract]
        IMUser Join(Guid userid, States state);

        /// <summary>
        /// 用户离线
        /// </summary>
        /// <param name="userid">The userid.</param>
        [OperationContract]
        void OffLine(Guid userid);


        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">The message.</param>
        [OperationContract]
        void SentMessage(Message message);

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="command">The command.</param>
        [OperationContract]
        void SentCommand(IMCommand command);


        /// <summary>
        /// 改变用户设置
        /// </summary>
        /// <param name="setting">The setting.</param>
        [OperationContract]
        void ChangeSetting(IMUserSetting setting);

        
    }
}
