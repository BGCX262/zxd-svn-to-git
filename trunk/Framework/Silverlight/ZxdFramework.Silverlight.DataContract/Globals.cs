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
    /// Ӧ�ó����е�ȫ�ֶ���
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
        /// ���û�ȡ�������ĸ�Ŀ¼
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
        /// ���û�ȡ��־����
        /// </summary>
        /// <value>
        /// The logger facade.
        /// </value>
        public static ILoggerFacade LoggerFacade { set; get; }

        /// <summary>
        /// ���û�ȡ�¼��������
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        public static IEventAggregator EventAggregator {  set; get; }


        /// <summary>
        /// ���û�ȡMEF��������
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public static CompositionContainer Container { set; get; }

        /// <summary>
        /// ��ȡ���õ�ǰ�����е�¼���û�. ���Ҫ�ı䵱ǰ����. ������Ҫ�׳�һ���û��ı��¼�.
        /// <see cref="AccountChangedEvent"/>
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public static IUser CurrentUser { set; get; }


        /// <summary>
        /// ���û�ȡϵͳ�Ľ�ɫ��
        /// </summary>
        /// <value>The root role.</value>
        public static IRole RootRole { set; get; }

        /// <summary>
        /// ���û�ȡ�����е�Ȩ�޿��Ƶ�, ��ǰ��Ȩ�޿��Ƶ��еĶ���ֻ����������ƥ��. �������������������Ϣ. ����˵����Ϣ, ͼ���
        /// </summary>
        /// <value>The root security action.</value>
        public static ISecurityAction RootSecurityAction { set; get; }


        /// <summary>
        /// ���û�ȡϵͳ�е��������һ�θ��µ�ʱ��
        /// </summary>
        /// <value>The last update.</value>
        public static DateTime LastDataUpdate { 
            set { IsolatedStorageSettings.ApplicationSettings["LastUpdate"] = value; } 
            get { return (DateTime)IsolatedStorageSettings.ApplicationSettings["LastUpdate"]; }
        }

    }
}