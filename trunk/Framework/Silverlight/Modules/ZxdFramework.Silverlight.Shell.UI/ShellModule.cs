using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using ProjectResources;
using ZxdFramework.DataContract.System;
using ZxdFramework.Shell.UI.Views;
using InitializationMode = Microsoft.Practices.Prism.Modularity.InitializationMode;

namespace ZxdFramework.Shell.UI
{
    /// <summary>
    /// 主体界面显示模块入口类
    /// </summary>
    [ZxdModuleExport("shell", typeof(ShellModule), 
        Author = "宗旭东", 
        Category = "System", 
        InitializationMode = InitializationMode.WhenAvailable,
        Description = "主体界面显示模块")]
    public class ShellModule : IModule
    {
        public ShellModule()
        {
            var storem =
                Application.GetResourceStream(new Uri("/ProjectResources;component/Themes/Default.xaml",
                                        UriKind.Relative));
            var str = new System.IO.StreamReader(storem.Stream);
            var dic = (ResourceDictionary)XamlReader.Load(str.ReadToEnd());
            Application.Current.Resources.MergedDictionaries.Add(dic);
            str.Close();
            
        }
        [Import("AppShellContent")] 
        public ContentControl ShellContent;

        [Import] 
        public Lazy<IShellViewModel> ViewModel;

        [Import]
        public IEventAggregator EventAggregator;

        [Import]
        public IRegionManager RegionManager;

        [Import]
        public ILoginViewModel LoginViewModel;

        [Import]
        public IContentViewModel ContentViewModel;

        [Import]
        public IAccountViewModel AccountViewModel;

        [Import]
        public IFootbarViewModel FootbarViewModel;

        [Import]
        public ILeftNavigateViewModel LeftNavigateViewModel;

        [Import]
        public INavigateMenuViewModel NavigateMenuViewModel;

        public void Initialize()
        {
            RegisterRegions();
            ShellContent.Content = ViewModel.Value.View;
        }

        /// <summary>
        /// 注册显示区域的控件
        /// </summary>
        public void RegisterRegions()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.LoginRegionName, () => LoginViewModel.View);
            RegionManager.RegisterViewWithRegion(RegionNames.ShellRegionName, () => ContentViewModel.View);
            RegionManager.RegisterViewWithRegion(RegionNames.AccountRegionName, () => AccountViewModel.View);
            RegionManager.RegisterViewWithRegion(RegionNames.FooterRegionName, () => FootbarViewModel.View);
            RegionManager.RegisterViewWithRegion(RegionNames.LeftNavigateRegionName, () => LeftNavigateViewModel.View);
            RegionManager.RegisterViewWithRegion(RegionNames.NavigateMenuRegionName, () => NavigateMenuViewModel.View);
            //RegionManager.RegisterViewWithRegion(RegionNames.LogoRegionName, () => LogoControl.Value );
            //RegionManager.RegisterViewWithRegion(RegionNames.AccountRegionName, () => AccountView.Value.View);
            //RegionManager.RegisterViewWithRegion(RegionNames.FooterRegionName, () => FootbarView.Value);
        }
    }
}