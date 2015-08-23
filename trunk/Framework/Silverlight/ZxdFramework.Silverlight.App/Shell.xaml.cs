using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using ZxdFramework.DataContract;
using ZxdFramework.Module.Services;
using Category = Microsoft.Practices.Prism.Logging.Category;

namespace ZxdFramework.App
{
    /// <summary>
    /// 最底层的容器. 当前类型已被抛出
    /// </summary>
    [Export]
    public partial class Shell : UserControl
    {

        /// <summary>
        /// 
        /// </summary>
        [Import] public ILoggerFacade Logger;

        /// <summary>
        /// 
        /// </summary>
        [Import(AllowRecomposition = false)] 
        public IModuleManager ModuleManager;

        /// <summary>
        /// 
        /// </summary>
        [Import] public IModuleManagerService ModuleManagerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        public Shell()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取底层容器的内容. 当前的内容会被导出一个 "AppShellContent" 
        /// </summary>
        /// <value>
        /// The content of the shell.
        /// </value>
        [Export("AppShellContent")]
        public ContentControl ShellContent
        {
            get { return shellContent; }
        }

        /// <summary>
        /// Handles the KeyDown event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.U && Keyboard.Modifiers == ModifierKeys.Control)
            {
                Logger.Log("sadfasdfafdasfd", Category.Exception, Priority.None);
            }
        }


        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //加载模块
            ModuleManager.LoadModule("ZxdFramework.Module.ModuleManagerModule");
        }
    }
}