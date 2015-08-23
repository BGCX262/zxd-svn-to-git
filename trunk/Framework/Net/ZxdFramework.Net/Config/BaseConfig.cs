using System;
using System.Collections.Generic;
using Spring.Context;
using Spring.Context.Support;
using System.Linq;
using ZxdFramework.Events;

namespace ZxdFramework.Config
{
    /// <summary>
    /// �����������ṩ��. 
    /// </summary>
    public class BaseConfig
    {

        public const string RootResource = @"assembly://ZxdFramework.Net/ZxdFramework/Config.Application-Context.config";

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseConfig"/> class.
        /// </summary>
        public BaseConfig() : this(new string[]{})
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseConfig"/> class.
        /// </summary>
        /// <param name="extConfigPath">��չ�������ļ�</param>
        public BaseConfig(string extConfigPath) : this(new []{ extConfigPath })
        {
        }


        public BaseConfig(IEnumerable<string> extConfigPaths)
        {
            var paths = new List<string>(extConfigPaths) {RootResource};
            _rootContext = new XmlApplicationContext(paths.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        protected static XmlApplicationContext _rootContext;

        /// <summary>
        /// ��ȡ��Ҫ������, ������ʵ����ģ�Ͳ��ܻ�ȡ����
        /// </summary>
        /// <value>The root context.</value>
        public static IApplicationContext RootContext 
        {
            get
            {
                if(_rootContext == null) throw new Exception("���������� BaseConfig û��ʵ����. ������ʵ��������������κ�һ������.");
                return _rootContext;
            }
        }

        /// <summary>
        ///  ��ȡ�¼��������
        /// </summary>
        /// <value>The event aggregator.</value>
        public static IEventAggregator EventAggregator
        {
            get { return (IEventAggregator)_rootContext.GetObject("EventAggregator", typeof (IEventAggregator)); }
        }
    }
}