using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using C1.Silverlight;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using ProjectResources;
using ZxdFramework.Commands;
using ZxdFramework.DataContract.System;
using ZxdFramework.SystemManager.Commands;
using ZxdFramework.SystemManager.Views;
using InitializationMode = Microsoft.Practices.Prism.Modularity.InitializationMode;

namespace ZxdFramework.SystemManager
{
    /// <summary>
    /// ϵͳ����ģ����˿���. ��Ҫ�û�ϵͳ��Ҫ���õ�����.
    /// </summary>
    [ZxdModuleExport("SystemManager", typeof(SystemManagerModule),
        Author = "����",
        Category = "System.Setting",
        InitializationMode = InitializationMode.WhenAvailable,
        DependsOnModuleNames = new []{"shell"},
        Description = "��Ҫ����ϵͳ���õ�ģ��")]
    public class SystemManagerModule : IModule
    {
        [Import]
        public IEventAggregator EventAggregator;

        [Import]
        public IRegionManager RegionManager;

        [Import]
        public Lazy<IModuleSettingWindowModel> ModuleSettingWindowModel;

        [Import]
        public Lazy<ITestViewModel> TestViewModel;



        public DelegateCommand OpenTest2WindowCommand;





        public void Initialize()
        {

            OpenTest2WindowCommand = new DelegateCommand(DoOpenTest2Window);

            SystemManagerCommands.OpenModuleSettingCommand.RegisterCommand(new DelegateCommand(DoOpenModuleSetting, CanOpenModuleSetting));

            var region = RegionManager.Regions[RegionNames.TestRegionName];
            region.Add(new Button()
                           {
                               Command = SystemManagerCommands.OpenModuleSettingCommand,
                               Content = "Test"
                           });   


            //region = RegionManager.Regions[RegionNames.Test2RegionName];
            //region.Add(new Button()
            //               {
            //                   Width = 100,
            //                   Height = 100,
            //                   Command = OpenTest2WindowCommand,
            //                   Content = "Teest"
            //               });


            RegionManager.RegisterViewWithRegion(RegionNames.Test2RegionName, () =>
                                                                                  
                                                                                      new Button()
                                                                                                 {
                                                                                                     Width = 100,
                                                                                                     Height = 100,
                                                                                                     Command =
                                                                                                         OpenTest2WindowCommand,
                                                                                                     Content = "Teest"
                                                                                                 }
                                                                                  );
        }

        public bool CanOpenModuleSetting()
        {
            return true;
        }

        public void DoOpenModuleSetting()
        {
            var win = ModuleSettingWindowModel.Value.View as C1Window;
            win.Show();
            win.CenterOnScreen();
        }


        public void DoOpenTest2Window()
        {
            TestViewModel.Value.View.Show();
        }
    }
}