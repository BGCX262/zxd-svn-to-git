using Microsoft.Practices.Prism.Commands;

namespace ZxdFramework.SystemManager.Commands
{



    /// <summary>
    /// 集合了系统设置模块的命令
    /// </summary>
    public static class SystemManagerCommands
    {
        /// <summary>
        /// 打开模块的设置
        /// </summary>
        public static CompositeCommand OpenModuleSettingCommand = new CompositeCommand();
    }


    /// <summary>
    /// 系统设置模块命令代理
    /// </summary>
    public class SystemManagerCommandsProxy
    {
        /// <summary>
        /// Gets the open module setting command.
        /// </summary>
        /// <value>The open module setting command.</value>
        public virtual CompositeCommand OpenModuleSettingCommand
        {
            get { return SystemManagerCommands.OpenModuleSettingCommand; }
        }
    }
}