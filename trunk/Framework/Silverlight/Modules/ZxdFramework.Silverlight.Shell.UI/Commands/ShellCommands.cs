using Microsoft.Practices.Prism.Commands;

namespace ZxdFramework.Silverlight.Shell.UI.Commands
{
    /// <summary>
    /// 主程序命令代理类, 集成了主程序中对外发送的全部命令
    /// </summary>
    public class ShellCommands
    {
        /// <summary>
        /// 显示日志命令
        /// </summary>
        public static CompositeCommand ShowLoggerCommand = new CompositeCommand();
 
    }
}