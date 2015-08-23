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
    /// ģ�������, �ṩ��ģ��Ķ�ȡ�ȷ���
    /// </summary>
    public class ModuleManager
    {
        /// <summary>
        /// ���ļ��л�ȡģ�����
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public static ModuleFile GetModuleFile(FileInfo file)
        {
            return new ModuleFile(file);
        }
    }
}