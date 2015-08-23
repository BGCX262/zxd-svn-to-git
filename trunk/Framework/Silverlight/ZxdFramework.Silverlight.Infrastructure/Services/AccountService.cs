using System;
using System.ComponentModel.Composition;
using ZxdFramework.DataContract;
using ZxdFramework.Service.Events;

namespace ZxdFramework.Infrastructure.Services
{
    /// <summary>
    /// �˻�������
    /// </summary>
    public interface IAccountService
    {
        void Loggin(string username, string password, ServiceCompleted<IUser> completed);
    }

    [Export(typeof(IAccountService))]
    public class AccountService : IAccountService
    {
        public void Loggin(string username, string password, ServiceCompleted<IUser> completed)
        {
            
        }
    }
}