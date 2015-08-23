using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading;
using System.Web;

namespace ZxdFramework.WebUI.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ConnService : IConnService
    {
        IConnClient _client;
        /// <summary>
        /// 
        /// </summary>
        string _orderName;
        /// <summary>
        /// 
        /// </summary>
        int _orderQuantity;
        /// <summary>
        /// 
        /// </summary>
        bool _processed = false;

        public void Registor(string name, int quantity)
        {
            //var ht = HttpContext.Current;
            // Grab the client callback channel.
            _client = OperationContext.Current.GetCallbackChannel<IConnClient>();

            
            _orderName = name;
            _orderQuantity = quantity;

            // Pretend service is processing and will call client back in 5 seconds.
            using (var timer = new Timer(new TimerCallback(CallClient), null, 5000, 5000))
            {
                Thread.Sleep(11000);
            }

        }

        private void CallClient(object o)
        {
            Order order = new Order();
            order.Payload = new List<string>();

            // Process the order.
            if (_processed)
            {
                // Pretend the service is fulfilling the order.
                while (_orderQuantity-- > 0)
                {
                    order.Payload.Add(_orderName + _orderQuantity);
                }

                order.Status = OrderStatus.Completed;

            }
            else
            {
                // Pretend the service is processing the payment.
                order.Status = OrderStatus.Processing;
                _processed = true;
            }

            // Call the client back.
            _client.Receive(order);

        }

    }


    public class Order
    {
        public OrderStatus Status { get; set; }
        public List<string> Payload { get; set; }
    }

    public enum OrderStatus
    {
        Processing,
        Completed
    }


}
