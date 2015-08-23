using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using ZxdFramework.Authorization;
using ZxdFramework.DataContract;
using ZxdFramework.DataContract.Authorization;
using ZxdFramework.Service.Events;

namespace ZxdFramework.Service.Services
{
    /// <summary>
    /// �˻�������
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Logins the specified login event.
        /// </summary>
        /// <param name="loginEvent">The login event.</param>
        void Login(UserLoginEvent loginEvent);

        /// <summary>
        /// ��ȡ��ǰ���û�����
        /// </summary>
        /// <param name="serviceCompleted">The service completed.</param>
        void GetCurrentUser(ServiceCompleted<IUser> serviceCompleted);


        /// <summary>
        /// ��ȡ��������ɫ������
        /// </summary>
        /// <param name="serviceCompleted">The service completed.</param>
        void GetRootRole(ServiceCompleted<IRole> serviceCompleted);

        /// <summary>
        /// ��ȡ�����Ȩ�����ýڵ�, ֻ�ṩ��ϵͳ��Ҫʹ�õ���Ϣ. ����Ҫ��ȡ������Ϣ
        /// </summary>
        /// <param name="serviceCompleted">The service completed.</param>
        void GetSystemRootSecurityAction(ServiceCompleted<ISecurityAction> serviceCompleted);
    }

    ///<summary>
    ///</summary>
    [Export(typeof(IAccountService))]
    public class AccountService : IAccountService
    {
        /// <summary>
        /// 
        /// </summary>
        [Import]
        public IEventAggregator EventAggregator;

        public void Login(UserLoginEvent loginEvent)
        {

            var d = new Dictionary<string, object>
                        {
                            {"Username", loginEvent.LoginName},
                            {"Password", loginEvent.LoginPassword}
                        };

            var @event = SentRequestEvent.CreateRequestEvent("Account.json/LogOn", d);
            @event.Result = (a, b) =>
                                {
                                    var r = b.GetPostData<int>();
                                    if(r == 1)
                                    {
                                        //��ȡ��¼����
                                        GetCurrentUser(delegate(IUser user, IRequestParam param)
                                                           {
                                                               loginEvent.LoginUser = user;
                                                               loginEvent.Callback(r, param);
                                                           });
                                    }
                                    else
                                    {
                                        loginEvent.Callback(r, a);
                                    }
                                };
            EventAggregator.Publish(@event);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="serviceCompleted">The service completed.</param>
        public void GetCurrentUser(ServiceCompleted<IUser> serviceCompleted)
        {
            var @event = SentRequestEvent.CreateRequestEvent("Account.json/GetCurrentUser", null);
            @event.Result = (a, b) => serviceCompleted(b.GetPostData<User>(), a);
            EventAggregator.Publish(@event);
        }

        //public void GetUserSetting(ServiceCompleted<Users>)

        public void GetRootRole(ServiceCompleted<IRole> serviceCompleted)
        {
            var @event = SentRequestEvent.CreateRequestEvent("Account.json/GetSystemRootRole", null);
            @event.Result = (a, b) => serviceCompleted(b.GetPostData<Role>(), a);
            EventAggregator.Publish(@event);
        }

        public void GetSystemRootSecurityAction(ServiceCompleted<ISecurityAction> serviceCompleted)
        {
            var @event = SentRequestEvent.CreateRequestEvent("Account.json/GetSystemRootSercurityAction", null);
            @event.Result = (a, b) => serviceCompleted(b.GetPostData<SecurityAction>(), a);
            EventAggregator.Publish(@event);
        }
    }
}