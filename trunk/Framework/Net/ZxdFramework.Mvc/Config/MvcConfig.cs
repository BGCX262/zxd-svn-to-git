using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Scripting.Hosting;
using Spring.Context;
using Spring.Context.Support;
using ZxdFramework.Authorization;
using ZxdFramework.Config;
using ZxdFramework.Mvc.Engines.Razor;
using ZxdFramework.Mvc.Engines.Ruby;
using ZxdFramework.Mvc.Filters;
using ZxdFramework.Net.Script;

namespace ZxdFramework.Mvc.Config
{
    /// <summary>
    /// 提供了 Asp.net MVC 框架的基本配置加载类型
    /// </summary>
    public class MvcConfig : BaseConfig
    {
        /// <summary>
        /// MVC 容器配置地址
        /// </summary>
        public const string MvcRootResource = @"assembly://ZxdFramework.Mvc/ZxdFramework.Mvc/Config.Application-Context.config";
        /// <summary>
        /// 控制器 容器配置地址
        /// </summary>
        public const string ControllerResource = @"assembly://ZxdFramework.Mvc/ZxdFramework.Mvc/Config.Controller-Context.config";

        /// <summary>
        /// 在不附加其他配置文件的情况下, 进行初始化
        /// </summary>
        public MvcConfig() : this(new string[]{}, null)
        {

        }

        /// <summary>
        /// 传入附加的主配置文件, 然后进行初始化
        /// </summary>
        /// <param name="extRootResource">附加主配置文件</param>
        public MvcConfig(string extRootResource) : this(new []{extRootResource}, null)
        {
            
        }

        /// <summary>
        /// 附加一个主配置文件和一个控制器配置文件
        /// </summary>
        /// <param name="extRootResource">主配置文件</param>
        /// <param name="extControllerResources">控制器配置文件</param>
        public MvcConfig(string extRootResource, string extControllerResources)
            : this(new[] { extRootResource }, new []{ extControllerResources })
        {
            
        }

        /// <summary>
        /// 附加多个主配置文件和多个控制器配置文件
        /// </summary>
        /// <param name="extRootResources">主配置文件集合</param>
        /// <param name="extControllerResources">控制器配置文件集合</param>
        public MvcConfig(IEnumerable<string> extRootResources, IEnumerable<string> extControllerResources)
            : base(new List<string>(extRootResources)
                {
                    MvcRootResource
                })
        {
            

            var res = new List<string> {ControllerResource};
            if (extControllerResources != null) res.AddRange(extControllerResources);


            var assmemblyTypes = new[] { 
                    typeof(object), 
                    typeof(Uri), 
                    typeof(Controller)};

            var scriptRuntime = Bootstrap.CreateIronRubyRuntime(assmemblyTypes, true, true);
            var rubyDatabaseViewEngine = new RubyViewEngine(scriptRuntime, null)
                                             {
                                                 ViewLocationCache = DefaultViewLocationCache.Null
                                             };
            ViewEngines.Engines.Insert(0, rubyDatabaseViewEngine);


            //var data = ViewEngines.Engines;
            //data.Insert(0, new DbRazorEngine());
            //data.Add(new DbRazorEngine());
            //ViewEngines.Engines.Add()
            _controllerContext = new XmlApplicationContext(_rootContext, res.ToArray());
            //var factory = "SpringControllerFactory".GetInstance<IControllerFactory>();
            //注册容器
            ControllerBuilder.Current.SetControllerFactory("SpringControllerFactory".GetInstance<IControllerFactory>());
            //权限过滤
            GlobalFilters.Filters.Add(AuthorizationFactory.Instance);
        }

        private static IApplicationContext _controllerContext;
        /// <summary>
        /// 获取控制器的对象容器. <br/>
        /// 控制器的对象容器属于RootContext下的一个子容器. 
        /// </summary>
        /// <value>The root context.</value>
        public static IApplicationContext ControllerContext
        {
            get
            {
                if (_controllerContext == null) throw new Exception("基本配置类 BaseConfig 没有实例化. 必须先实例化配置类或者任何一个子类.");
                return _controllerContext;
            }
        }

    }
}