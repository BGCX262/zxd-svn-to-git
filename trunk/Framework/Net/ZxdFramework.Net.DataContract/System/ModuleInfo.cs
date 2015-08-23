using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ZxdFramework.DataContract.System
{
    /// <summary>
    /// Silverlight 客户端的模块模型
    /// </summary>
    public class ModuleInfo : Entity<Guid>
    {
        /// <summary>
        /// 所属模块的文件
        /// </summary>
        /// <value>The module file.</value>
        [JsonIgnore]
        public ModuleFile ModuleFile
        {
            set;
            get;
        }

        /// <summary>
        /// 模块初始化模式
        /// </summary>
        /// <value>The initialization mode.</value>
        public InitializationMode InitializationMode { get; set; }

        /// <summary>
        /// 模块引用路径
        /// </summary>
        /// <value>The ref.</value>
        public string Ref { get; set; }

        /// <summary>
        /// 设置获取模块名字
        /// </summary>
        /// <value>The name of the module.</value>
        public string ModuleName { get; set; }

        /// <summary>
        /// 模块的人口类型
        /// </summary>
        /// <value>The type of the module.</value>
        public string ModuleType { get; set; }

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

        /// <summary>
        /// 被依赖的模块集合
        /// </summary>
        /// <value>The depends on.</value>
        public ICollection<string> DependsOn { get; set; }


        public override int GetHashCode()
        {
            return base.GetHashCode() + 233455;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ModuleInfo)) return false;
            return base.Equals(obj);
        }

        /// <summary>
        /// 获取 Xaml 片段定义内容
        /// </summary>
        /// <returns></returns>
        public string GetXamlDefinition()
        {
            const string context = @"<Modularity:ModuleInfo Ref=""{0}"" ModuleName=""{1}"" ModuleType=""{2}"" InitializationMode=""{3}"">
<Modularity:ModuleInfo.DependsOn>
    {4}
</Modularity:ModuleInfo.DependsOn>
</Modularity:ModuleInfo>";

            var temp = DependsOn.Aggregate("", (current, depend) => current + (string.Format("<sys:String>{0}</sys:String>", depend) + "\n"));

            return string.Format(context, 
                Ref, 
                ModuleName, 
                ModuleType, 
                Enum.GetName(typeof(InitializationMode), InitializationMode),
                temp);
        }
    }

    /// <summary>
    /// Specifies on which stage the Module group will be initialized.
    /// </summary>
    public enum InitializationMode
    {
        /// <summary>
        /// The module will be initialized when it is available on application start-up.
        /// </summary>
        WhenAvailable,

        /// <summary>
        /// The module will be initialized when requested, and not automatically on application start-up.
        /// </summary>
        OnDemand
    }
}