using System;
using Microsoft.Practices.Prism.MefExtensions.Modularity;

namespace ZxdFramework.DataContract.System
{
    /// <summary>
    /// 系统模块导出标签, 如果一个类标记了这个标签. 那么程序在启动的时候就会自动的加载这个类.
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
        /// 开发人员
        /// </summary>
        /// <value>The author.</value>
        public string Author { set; get; }

        /// <summary>
        /// 模块的描述信息
        /// </summary>
        /// <value>The description.</value>
        public string Description { set; get; }


        /// <summary>
        /// 模块类别
        /// </summary>
        /// <value>The category.</value>
        public string Category { set; get; }

    }
}