using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.Text;
using ZxdFramework.Dao.IM;
using ZxdFramework.DataContract.IM;
using Message = ZxdFramework.DataContract.IM.Message;

namespace ZxdFramework.WebUI.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class IMService : IIMService
    {
        public IMService()
        {
            Dao = "IMDao".GetInstance<IIMDao>();
        }


        public IIMDao Dao { protected set; get; }


        public IMUser Join(Guid userid, States state)
        {
            var context = OperationContext.Current;
            //获取传进的消息属性
            var properties = context.IncomingMessageProperties;
            //获取消息发送的远程终结点IP和端口
            var endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            var user = Dao.Join(userid, state);
            user.Client = context.GetCallbackChannel<IIMClient>();
            user.TcpAddress = endpoint.Address;
            return user;
        }

        public void OffLine(Guid userid)
        {
            Dao.OffLine(userid);
        }

        public void SentMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void SentCommand(IMCommand command)
        {
            throw new NotImplementedException();
        }

        public void ChangeSetting(IMUserSetting setting)
        {
            throw new NotImplementedException();
        }
    }
}
