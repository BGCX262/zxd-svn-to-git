using System.ComponentModel.Composition;
using System.IO.IsolatedStorage;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;
using ZxdFramework.Service.Events;

namespace ZxdFramework.Service
{
    /// <summary>
    /// �������ģ��, �ṩ������ʹ�õ���������ģ��
    /// </summary>
    [ModuleExport("ZxdFramework.Service.ServiceModule",
        typeof (ServiceModule),
        InitializationMode = InitializationMode.WhenAvailable)]
    public class ServiceModule : IModule
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRequestServiceProvider _serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        [Import] public IAccountServiceProvider AccountServiceProvider;

        private IServiceLocator _serviceLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceModule"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="serviceLocator">The service locator.</param>
        [ImportingConstructor]
        public ServiceModule(IRequestServiceProvider serviceProvider, IEventAggregator eventAggregator,
                             IServiceLocator serviceLocator)
        {
            _serviceProvider = serviceProvider;
            _eventAggregator = eventAggregator;
            //serviceLocator.GetInstance()
            _serviceLocator = serviceLocator;
        }

        #region IModule Members

        public void Initialize()
        {
            InitStore();
            Registor();
            //ע��ִ��������¼�
            _eventAggregator.Subscribe<SentRequestEvent>(_serviceProvider.OnSentRequest);
            _eventAggregator.Subscribe<UserLoginEvent>(AccountServiceProvider.OnLogggin);
            AccountServiceProvider.Init();
            //IsolatedStorageFile.
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private static void InitStore()
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            if (store.AvailableFreeSpace < 20*1024*1024)
            {
                store.IncreaseQuotaTo(20*1024*1024);
            }
        }

        private static void Registor()
        {
        }
    }
}