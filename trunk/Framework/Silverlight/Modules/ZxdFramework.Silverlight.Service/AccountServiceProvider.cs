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
    /// 定义执行账号服务的事件支持类
    /// </summary>
    public interface IAccountServiceProvider
    {

        /// <summary>
        /// 初始化权限中需要的信息
        /// </summary>
        void Init();

        /// <summary>
        /// 执行用户登录的事件
        /// </summary>
        /// <param name="event">The @event.</param>
        void OnLogggin(UserLoginEvent @event);

        /// <summary>
        /// 当账号变化执行的方法
        /// </summary>
        /// <param name="event">The @event.</param>
        void OnAccountChanged(AccountChangedEvent @event);

        /// <summary>
        /// 当用户发出一个执行模块程序的命令
        /// </summary>
        /// <param name="event">The @event.</param>
        void OnExecuteCommand(ExecuteCommandEvent @event);
    }

    /// <summary>
    /// 实现了支持账号的服务类
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
        /// 初始化权限中需要的信息
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
        /// 执行用户登录的事件
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
            IMService.Init(@event.CurrentUser, States.在线);
        }

        /// <summary>
        /// 当用户发出一个执行模块程序的命令
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