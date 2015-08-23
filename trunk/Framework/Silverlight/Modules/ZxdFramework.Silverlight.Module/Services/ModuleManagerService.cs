using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using ZxdFramework.DataContract.System;
using ZxdFramework.Module.Events;
using ZxdFramework.Service.Events;

namespace ZxdFramework.Module.Services
{
    /// <summary>
    /// ������ģ�����ķ�����
    /// </summary>
    public interface IModuleManagerService
    {
        /// <summary>
        /// �ϴ�ģ����·�����ģ����Ϣ
        /// </summary>
        /// <param name="moduleFile">The module file.</param>
        /// <param name="completed">The completed.</param>
        void UploadModuleFile(ModuleFile moduleFile, ServiceCompleted<bool> completed);


        /// <summary>
        /// ����ģ��������Ϣ
        /// </summary>
        /// <param name="completed">The completed.</param>
        void DownloadModuleInfo(ServiceCompleted<AfterDownloadModuleInfoEvent> completed);
    }


    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IModuleManagerService))]
    public class ModuleManagerService : IModuleManagerService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IEventAggregator _eventAggregator;
        /// <summary>
        /// 
        /// </summary>
        private readonly ILoggerFacade _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleManagerService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="loggerFacade">The logger facade.</param>
        [ImportingConstructor]
        public ModuleManagerService(IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
        {
            _eventAggregator = eventAggregator;
            _logger = loggerFacade;
        }

        /// <summary>
        /// �ϴ�ģ����·�����ģ����Ϣ
        /// </summary>
        /// <param name="moduleFile">The module file.</param>
        /// <param name="completed">The completed.</param>
        public void UploadModuleFile(ModuleFile moduleFile, ServiceCompleted<bool> completed)
        {
            var @event = SentRequestEvent.CreateRequestEvent("Service/System.json/UploadModuleFile", moduleFile);
            @event.Result = (a, b) => completed.Invoke(b.GetPostData<bool>(), @event);
            _eventAggregator.Publish(@event);
        }

        /// <summary>
        /// ����ģ��������Ϣ
        /// </summary>
        /// <param name="completed">The completed.</param>
        public void DownloadModuleInfo(ServiceCompleted<AfterDownloadModuleInfoEvent> completed)
        {
            _logger.Log("��ʼ��������ģ��������Ϣ", Category.Debug, Priority.Medium);
            var @event = SentRequestEvent.CreateRequestEvent("Service/System.json/DownXamlModuleInfo");
            @event.Result = (a, b) =>
                                {
                                    var downloadEvent = new AfterDownloadModuleInfoEvent(b.GetPostData<string>());
                                    _eventAggregator.Publish(downloadEvent);
                                    completed(downloadEvent, @event);
                                    _logger.Log("���ģ������������Ϣ", Category.Debug, Priority.Medium);
                                };

            _eventAggregator.Publish(@event);
        }
    }
}