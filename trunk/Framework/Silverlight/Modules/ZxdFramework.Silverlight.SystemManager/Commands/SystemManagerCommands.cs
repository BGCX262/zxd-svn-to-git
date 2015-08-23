using Microsoft.Practices.Prism.Commands;

namespace ZxdFramework.SystemManager.Commands
{



    /// <summary>
    /// ������ϵͳ����ģ�������
    /// </summary>
    public static class SystemManagerCommands
    {
        /// <summary>
        /// ��ģ�������
        /// </summary>
        public static CompositeCommand OpenModuleSettingCommand = new CompositeCommand();
    }


    /// <summary>
    /// ϵͳ����ģ���������
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