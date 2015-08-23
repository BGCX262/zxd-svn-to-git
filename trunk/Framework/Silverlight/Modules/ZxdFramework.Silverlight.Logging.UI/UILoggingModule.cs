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
    /// ��־��ͼģ��
    /// </summary>
    [ZxdModuleExport("UiLogginModule", typeof(UILoggingModule),
        Author = "����",
        Category = "System",
        InitializationMode = InitializationMode.WhenAvailable,
        Description = "��־����ģ��. �ṩ����־��Ϣ����ʾ�ȹ���",
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