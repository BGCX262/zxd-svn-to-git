using System;
using System.ComponentModel.Composition;
using C1.Silverlight;
using Microsoft.Practices.Prism.Modularity;
using ZxdFramework.Commands;
using ZxdFramework.DataContract.System;
using ZxdFramework.Silverlight.Shell.UI.Commands;
using InitializationMode = Microsoft.Practices.Prism.Modularity.InitializationMode;

namespace ZxdFramework.Logging.UI
{
    /// <summary>
    /// 日志视图模块
    /// </summary>
    [ZxdModuleExport("UiLogginModule", typeof(UILoggingModule),
        Author = "宗旭东",
        Category = "System",
        InitializationMode = InitializationMode.WhenAvailable,
        Description = "日志窗口模块. 提供对日志信息的显示等功能",
        DependsOnModuleNames = new[] { "shell" })]
    public class UILoggingModule : IModule
    {

        [Import]
        public Lazy<ILoggingWindowViewModel> LoggingWindowViewModel;

        public void Initialize()
        {
            ShellCommands.ShowLoggerCommand.RegisterCommand(LoggingWindowViewModel.Value.ShowCommand);
        }

    }
}