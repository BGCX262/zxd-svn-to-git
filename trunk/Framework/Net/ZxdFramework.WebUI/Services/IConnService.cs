using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ZxdFramework.WebUI.Services
{
   
    [ServiceContract(CallbackContract = typeof(IConnClient))]
    public interface IConnService
    {
        [OperationContract]
        void Registor(string name, int quantity);
    }
}
