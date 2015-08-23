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
    /// ��������ģ��Ķ�����Ϣ
    /// </summary>
    public class DownloadModuleInfoEvent : CompositePresentationEvent<DownloadModuleInfoEvent>
    {
        
    }

    /// <summary>
    /// �������ģ����Ϣ���¼�
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
        /// ��ȡ Xaml ����Ϣ
        /// </summary>
        public string XamlContext { internal set; get; }


        /// <summary>
        /// ��ȡ���������ص�ģ��������Ϣ
        /// </summary>
        /// <returns></returns>
        public IModuleCatalog GetModuleCatalog()
        {

            Globals.LoggerFacade.Log("��ȡģ�������Ϣ: " + XamlContext, Category.Debug, Priority.Medium);

            var catalog = (IModuleCatalog)XamlReader.Load(XamlContext);

            return catalog;
        }
    }
}