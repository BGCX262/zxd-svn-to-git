using System.ServiceModel;

namespace ZxdFramework.WebUI.Services
{
    [ServiceContract]
    public interface IConnClient
    {
        [OperationContract(IsOneWay = true)]
        void Receive(Order msg);
    }
}