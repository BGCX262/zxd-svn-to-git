using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Resources;
using System.Xml;
using ZxdFramework.DataContract.System;

namespace ZxdFramework.Module
{
    /// <summary>
    /// 模块管理类, 提供了模块的读取等方法
    /// </summary>
    public class ModuleManager
    {
        /// <summary>
        /// 从文件中获取模块对象
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public static ModuleFile GetModuleFile(FileInfo file)
        {
            return new ModuleFile(file);
        }
    }
}