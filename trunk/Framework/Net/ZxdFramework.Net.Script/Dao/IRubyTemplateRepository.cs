using System;
using Spring.Data.NHibernate.Generic;
using ZxdFramework.Dao;

namespace ZxdFramework.Script.Dao
{
    /// <summary>
    /// ����ű�ģ������ݲ�����
    /// </summary>
    public interface IRubyTemplateRepository : IRepositoryBase<RubyTemplate, Guid>
    {
        
    }


    /// <summary>
    /// �ű�ģ�������
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