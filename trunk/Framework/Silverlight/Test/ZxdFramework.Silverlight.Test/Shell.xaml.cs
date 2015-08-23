using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using ZxdFramework.DataContract.System;
using ZxdFramework.Module.Services;
using ZxdFramework.Service.Events;

namespace ZxdFramework.Test
{
    [Export]
    public partial class Shell : UserControl, IPartImportsSatisfiedNotification
    {
        public Shell()
        {
            InitializeComponent(); 
        }

        public string Names { set; get; }

        [Import(AllowRecomposition = false)]
        public IModuleManager ModuleManager { set; get; }

        [Import]
        public IModuleCatalog ModuleCatalog { set; get; }

        [Import]
        public IEventAggregator EventAggregator { set; get; }

        [Import]
        public IModuleManagerService ModuleService { set; get; }

        #region IPartImportsSatisfiedNotification Members

        public void OnImportsSatisfied()
        {
        }

        #endregion

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //var request = new SentRequestEvent("System/GetModuleInfo", JsonRequest.CreateJsonRequest(12354));
            var ofd = new OpenFileDialog {Filter = "模块文件(*.xap)|*.xap"};

            if (ofd.ShowDialog() == true)
            {
                ModuleFile data = Module.ModuleManager.GetModuleFile(ofd.File);
                ModuleService.UploadModuleFile(data, Completed);
                //container.
            }
            //EventAggregator.Publish(request);
        }

        private void Completed(bool result, IRequestParam param)
        {
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //加载模块
            ModuleManager.LoadModule("ZxdFramework.Module.ModuleManagerModule");
        }
    }
}