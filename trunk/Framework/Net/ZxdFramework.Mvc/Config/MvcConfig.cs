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
    /// �ṩ�� Asp.net MVC ��ܵĻ������ü�������
    /// </summary>
    public class MvcConfig : BaseConfig
    {
        /// <summary>
        /// MVC �������õ�ַ
        /// </summary>
        public const string MvcRootResource = @"assembly://ZxdFramework.Mvc/ZxdFramework.Mvc/Config.Application-Context.config";
        /// <summary>
        /// ������ �������õ�ַ
        /// </summary>
        public const string ControllerResource = @"assembly://ZxdFramework.Mvc/ZxdFramework.Mvc/Config.Controller-Context.config";

        /// <summary>
        /// �ڲ��������������ļ��������, ���г�ʼ��
        /// </summary>
        public MvcConfig() : this(new string[]{}, null)
        {

        }

        /// <summary>
        /// ���븽�ӵ��������ļ�, Ȼ����г�ʼ��
        /// </summary>
        /// <param name="extRootResource">�����������ļ�</param>
        public MvcConfig(string extRootResource) : this(new []{extRootResource}, null)
        {
            
        }

        /// <summary>
        /// ����һ���������ļ���һ�������������ļ�
        /// </summary>
        /// <param name="extRootResource">�������ļ�</param>
        /// <param name="extControllerResources">�����������ļ�</param>
        public MvcConfig(string extRootResource, string extControllerResources)
            : this(new[] { extRootResource }, new []{ extControllerResources })
        {
            
        }

        /// <summary>
        /// ���Ӷ���������ļ��Ͷ�������������ļ�
        /// </summary>
        /// <param name="extRootResources">�������ļ�����</param>
        /// <param name="extControllerResources">�����������ļ�����</param>
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
            //ע������
            ControllerBuilder.Current.SetControllerFactory("SpringControllerFactory".GetInstance<IControllerFactory>());
            //Ȩ�޹���
            GlobalFilters.Filters.Add(AuthorizationFactory.Instance);
        }

        private static IApplicationContext _controllerContext;
        /// <summary>
        /// ��ȡ�������Ķ�������. <br/>
        /// �������Ķ�����������RootContext�µ�һ��������. 
        /// </summary>
        /// <value>The root context.</value>
        public static IApplicationContext ControllerContext
        {
            get
            {
                if (_controllerContext == null) throw new Exception("���������� BaseConfig û��ʵ����. ������ʵ��������������κ�һ������.");
                return _controllerContext;
            }
        }

    }
}