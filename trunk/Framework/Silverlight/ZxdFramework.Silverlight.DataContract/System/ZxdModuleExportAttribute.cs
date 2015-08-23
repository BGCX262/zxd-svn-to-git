using System;
using Microsoft.Practices.Prism.MefExtensions.Modularity;

namespace ZxdFramework.DataContract.System
{
    /// <summary>
    /// ϵͳģ�鵼����ǩ, ���һ�������������ǩ. ��ô������������ʱ��ͻ��Զ��ļ��������.
    /// </summary>
    public class ZxdModuleExportAttribute : ModuleExportAttribute
    {
        public ZxdModuleExportAttribute(Type moduleType) : base(moduleType)
        {
        }

        public ZxdModuleExportAttribute(string moduleName, Type moduleType) : base(moduleName, moduleType)
        {
        }

        /// <summary>
        /// ������Ա
        /// </summary>
        /// <value>The author.</value>
        public string Author { set; get; }

        /// <summary>
        /// ģ���������Ϣ
        /// </summary>
        /// <value>The description.</value>
        public string Description { set; get; }


        /// <summary>
        /// ģ�����
        /// </summary>
        /// <value>The category.</value>
        public string Category { set; get; }

    }
}