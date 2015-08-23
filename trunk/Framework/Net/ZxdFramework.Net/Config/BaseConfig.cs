using System;
using System.Collections.Generic;
using Spring.Context;
using Spring.Context.Support;
using System.Linq;
using ZxdFramework.Events;

namespace ZxdFramework.Config
{
    /// <summary>
    /// 基本的配置提供类. 
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
        /// <param name="extConfigPath">扩展的配置文件</param>
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
        /// 获取主要的容器, 必须先实例化模型才能获取容器
        /// </summary>
        /// <value>The root context.</value>
        public static IApplicationContext RootContext 
        {
            get
            {
                if(_rootContext == null) throw new Exception("基本配置类 BaseConfig 没有实例化. 必须先实例化配置类或者任何一个子类.");
                return _rootContext;
            }
        }

        /// <summary>
        ///  获取事件管理对象
        /// </summary>
        /// <value>The event aggregator.</value>
        public static IEventAggregator EventAggregator
        {
            get { return (IEventAggregator)_rootContext.GetObject("EventAggregator", typeof (IEventAggregator)); }
        }
    }
}