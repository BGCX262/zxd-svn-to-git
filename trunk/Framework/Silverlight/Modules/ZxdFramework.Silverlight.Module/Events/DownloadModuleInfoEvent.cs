using System.ComponentModel.Composition;
using System.Windows.Markup;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using ZxdFramework.DataContract;
using Category = Microsoft.Practices.Prism.Logging.Category;

namespace ZxdFramework.Module.Events
{
    /// <summary>
    /// 请求下载模块的定义信息
    /// </summary>
    public class DownloadModuleInfoEvent : CompositePresentationEvent<DownloadModuleInfoEvent>
    {
        
    }

    /// <summary>
    /// 完成下载模块信息的事件
    /// </summary>
    public class AfterDownloadModuleInfoEvent : CompositePresentationEvent<AfterDownloadModuleInfoEvent>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AfterDownloadModuleInfoEvent"/> class.
        /// </summary>
        public AfterDownloadModuleInfoEvent()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AfterDownloadModuleInfoEvent"/> class.
        /// </summary>
        /// <param name="xamlContext">The xaml context.</param>
        public AfterDownloadModuleInfoEvent(string xamlContext)
        {
            XamlContext = xamlContext;
        }


        /// <summary>
        /// 获取 Xaml 的信息
        /// </summary>
        public string XamlContext { internal set; get; }


        /// <summary>
        /// 获取服务器返回的模块配置信息
        /// </summary>
        /// <returns></returns>
        public IModuleCatalog GetModuleCatalog()
        {

            Globals.LoggerFacade.Log("获取模块加载信息: " + XamlContext, Category.Debug, Priority.Medium);

            var catalog = (IModuleCatalog)XamlReader.Load(XamlContext);

            return catalog;
        }
    }
}