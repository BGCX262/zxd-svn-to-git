using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Browser;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using ZxdFramework.DataContract.Authorization;
using ZxdFramework.DataContract.Events;
using ZxdFramework.Json;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// 应用程序中的全局对象
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Inits this instance.
        /// </summary>
        public static void Init()
        {
            JsonConverter.GlobalSettings.Converters.Add(new JsonRoleConverter());    
        }

        private static Uri _serverRoot;
        /// <summary>
        /// 设置获取服务器的根目录
        /// </summary>
        /// <value>
        /// The server root.
        /// </value>
        public static Uri ServerRoot
        {
            set
            {
                _serverRoot = value;
                IsolatedStorageSettings.ApplicationSettings["ServerRoot"] = value;
            }
            get
            {
                if (_serverRoot != null) return _serverRoot;
                if (!Application.Current.IsRunningOutOfBrowser)
                {
                    ServerRoot = HtmlPage.Document.DocumentUri;
                }
                else
                {
                    _serverRoot = (Uri)IsolatedStorageSettings.ApplicationSettings["ServerRoot"];
                }
                return _serverRoot;
            }
        }

        /// <summary>
        /// 设置获取日志工厂
        /// </summary>
        /// <value>
        /// The logger facade.
        /// </value>
        public static ILoggerFacade LoggerFacade { set; get; }

        /// <summary>
        /// 设置获取事件管理对象
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        public static IEventAggregator EventAggregator {  set; get; }


        /// <summary>
        /// 设置获取MEF容器对象
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public static CompositionContainer Container { set; get; }

        /// <summary>
        /// 获取设置当前程序中登录的用户. 如果要改变当前对象. 程序需要抛出一个用户改变事件.
        /// <see cref="AccountChangedEvent"/>
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public static IUser CurrentUser { set; get; }


        /// <summary>
        /// 设置获取系统的角色树
        /// </summary>
        /// <value>The root role.</value>
        public static IRole RootRole { set; get; }

        /// <summary>
        /// 设置获取程序中的权限控制点, 当前的权限控制点中的对象只是用来主键匹配. 并不会包含其他而外信息. 比如说明信息, 图标等
        /// </summary>
        /// <value>The root security action.</value>
        public static ISecurityAction RootSecurityAction { set; get; }


        /// <summary>
        /// 设置获取系统中的数据最后一次更新的时间
        /// </summary>
        /// <value>The last update.</value>
        public static DateTime LastDataUpdate { 
            set { IsolatedStorageSettings.ApplicationSettings["LastUpdate"] = value; } 
            get { return (DateTime)IsolatedStorageSettings.ApplicationSettings["LastUpdate"]; }
        }

    }
}