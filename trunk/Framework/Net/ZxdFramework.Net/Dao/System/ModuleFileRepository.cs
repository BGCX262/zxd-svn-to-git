using System;
using System.Linq;
using System.Text;
using Spring.Data.NHibernate.Generic;
using ZxdFramework.DataContract.System;

namespace ZxdFramework.Dao.System
{

    public interface IModuleFileRepository : IRepository
    {
        /// <summary>
        /// 获取模块 Xaml 的配置加载内容
        /// </summary>
        /// <returns></returns>
        string GetModuleXaml();
    }

    internal class ModuleFileRepository : RepositoryBase<ModuleFile, Guid>, IModuleFileRepository
    {
        public ModuleFileRepository(HibernateTemplate template) : base(template)
        {
        }

        /// <summary>
        /// 获取模块 Xaml 的配置加载内容
        /// </summary>
        /// <returns></returns>
        public string GetModuleXaml()
        {

            const string templeate = @"<Modularity:ModuleCatalog xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"" 
xmlns:sys=""clr-namespace:System;assembly=mscorlib"" 
xmlns:Modularity=""clr-namespace:Microsoft.Practices.Prism.Modularity;assembly=Microsoft.Practices.Prism"">
{0}
</Modularity:ModuleCatalog>";
            var context = new StringBuilder();
            foreach (var info in this.SelectMany(item => item.ModuleList))
            {
                context.Append(info.GetXamlDefinition());
            }
            return string.Format(templeate, context);
        }
    }
}