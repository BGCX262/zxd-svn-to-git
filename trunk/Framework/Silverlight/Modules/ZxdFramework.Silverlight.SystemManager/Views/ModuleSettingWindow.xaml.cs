using System.ComponentModel.Composition;
using C1.Silverlight;

namespace ZxdFramework.SystemManager.Views
{
    /// <summary>
    /// 定义模块设置窗口
    /// </summary>
    public interface IModuleSettingWindow
    {
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        IModuleSettingWindowModel Model { set; get; }

        /// <summary>
        /// 显示当前窗口
        /// </summary>
        void Show();

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        void Close();
    }


    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IModuleSettingWindow))]
    public partial class ModuleSettingWindow : C1Window, IModuleSettingWindow
    {
        public ModuleSettingWindow()
        {
            InitializeComponent();
        }

        public IModuleSettingWindowModel Model
        {
            get { return DataContext as IModuleSettingWindowModel; }
            set { DataContext = value; }
        }
    }
}

