using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using ZxdFramework.DataContract;
using ZxdFramework.DataContract.Events;
using ZxdFramework.Service.Events;
using ZxdFramework.Service.IM;
using ZxdFramework.Service.Services;
using IIMService = ZxdFramework.Service.Services.IIMService;

namespace ZxdFramework.Service
{
    /// <summary>
    /// ����ִ���˺ŷ�����¼�֧����
    /// </summary>
    public interface IAccountServiceProvider
    {

        /// <summary>
        /// ��ʼ��Ȩ������Ҫ����Ϣ
        /// </summary>
        void Init();

        /// <summary>
        /// ִ���û���¼���¼�
        /// </summary>
        /// <param name="event">The @event.</param>
        void OnLogggin(UserLoginEvent @event);

        /// <summary>
        /// ���˺ű仯ִ�еķ���
        /// </summary>
        /// <param name="event">The @event.</param>
        void OnAccountChanged(AccountChangedEvent @event);

        /// <summary>
        /// ���û�����һ��ִ��ģ����������
        /// </summary>
        /// <param name="event">The @event.</param>
        void OnExecuteCommand(ExecuteCommandEvent @event);
    }

    /// <summary>
    /// ʵ����֧���˺ŵķ�����
    /// </summary>
    [Export(typeof(IAccountServiceProvider))]
    public class AccountServiceProvider : IAccountServiceProvider
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountServiceProvider"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        [ImportingConstructor]
        public AccountServiceProvider(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe<AccountChangedEvent>(OnAccountChanged);
            eventAggregator.Subscribe<ExecuteCommandEvent>(OnExecuteCommand);
        }

        /// <summary>
        /// 
        /// </summary>
        [Import]
        public IAccountService AccountService;

        /// <summary>
        /// 
        /// </summary>
        [Import]
        public IIMService IMService;


        /// <summary>
        /// ��ʼ��Ȩ������Ҫ����Ϣ
        /// </summary>
        public void Init()
        {
            AccountService.GetSystemRootSecurityAction((a, b) =>
                                                           {
                                                               Globals.RootSecurityAction = a;
                                                           });
            AccountService.GetRootRole((a, b) =>
                                           {
                                               Globals.RootRole = a;
                                           });
        }

        /// <summary>
        /// ִ���û���¼���¼�
        /// </summary>
        /// <param name="event">The @event.</param>
        public void OnLogggin(UserLoginEvent @event)
        {
             AccountService.Login(@event); 
        }

        /// <summary>
        /// Called when [account changed].
        /// </summary>
        /// <param name="event">The @event.</param>
        public void OnAccountChanged(AccountChangedEvent @event)
        {
            //Globals.CurrentUser = @event.CurrentUser;
            IMService.Init(@event.CurrentUser, States.����);
        }

        /// <summary>
        /// ���û�����һ��ִ��ģ����������
        /// </summary>
        /// <param name="event">The @event.</param>
        public void OnExecuteCommand(ExecuteCommandEvent @event)
        {
            var target = @event.GetTargetInstance();
            if(target.CanExecute(@event.Param))
            {
                target.Execute(@event.Param);
            }
        }

    }
}