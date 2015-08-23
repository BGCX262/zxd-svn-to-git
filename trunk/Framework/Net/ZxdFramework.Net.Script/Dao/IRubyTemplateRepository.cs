using System;
using Spring.Data.NHibernate.Generic;
using ZxdFramework.Dao;

namespace ZxdFramework.Script.Dao
{
    /// <summary>
    /// 定义脚本模板的数据操作层
    /// </summary>
    public interface IRubyTemplateRepository : IRepositoryBase<RubyTemplate, Guid>
    {
        
    }


    /// <summary>
    /// 脚本模板管理器
    /// </summary>
    public class RubyTemplateRepository : RepositoryBase<RubyTemplate, Guid>, IRubyTemplateRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RubyTemplateRepository"/> class.
        /// </summary>
        /// <param name="template">The template.</param>
        public RubyTemplateRepository(HibernateTemplate template) : base(template)
        {
        }
    }
}