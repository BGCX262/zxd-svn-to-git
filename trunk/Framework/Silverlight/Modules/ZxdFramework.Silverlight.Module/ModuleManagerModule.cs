using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using ZxdFramework.Module.Events;
using ZxdFramework.Module.Services;
using ZxdFramework.Service.Events;

namespace ZxdFramework.Module
{
    /// <summary>
    /// 模块管理模块
    /// </summary>
    [ModuleExport("ZxdFramework.Module.ModuleManagerModule", 
        typeof(ModuleManagerModule), 
        InitializationMode = InitializationMode.OnDemand,
        DependsOnModuleNames = new [] { "ZxdFramework.Service.ServiceModule" })]
    public class ModuleManagerModule : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleManagerModule"/> class.
        /// </summary>
        /// <param name="managerService">The manager service.</param>
        /// <param name="moduleCatalog">The module catalog.</param>
        [ImportingConstructor]
        public ModuleManagerModule(IModuleManagerService managerService, IModuleCatalog moduleCatalog)
        {
            ModuleManagerService = managerService;
            ModuleCatalog = moduleCatalog;
        }

        /// <summary>
        /// Gets or sets the module manager service.
        /// </summary>
        /// <value>The module manager service.</value>
        public IModuleManagerService ModuleManagerService { protected set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Import(AllowRecomposition = false)]
        public IModuleManager ModuleManager;

        /// <summary>
        /// Gets or sets the module catalog.
        /// </summary>
        /// <value>The module catalog.</value>
        public IModuleCatalog ModuleCatalog { protected set; get; }

        public void Initialize()
        {
            ModuleManagerService.DownloadModuleInfo(DownloadModuleInfo);
        }

        private void DownloadModuleInfo(AfterDownloadModuleInfoEvent moduleInfoEvent, IRequestParam requestParam)
        {
            var obj = moduleInfoEvent.GetModuleCatalog();
            foreach (var moduleInfo in obj.Modules)
            {
                ModuleCatalog.AddModule(moduleInfo);
            }
            ModuleManager.Run();
        }
    }
}
