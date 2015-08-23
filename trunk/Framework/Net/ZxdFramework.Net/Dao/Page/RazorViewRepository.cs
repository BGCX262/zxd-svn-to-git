using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using Spring.Data.NHibernate.Generic;
using Spring.Transaction.Interceptor;
using ZxdFramework.DataContract.Page;
using System.Linq;

namespace ZxdFramework.Dao.Page
{
    ///<summary>
    /// ��ͼ�ֿ�ӿ�
    ///</summary>
    public interface IRazorViewRepository : IRepository
    {
        /// <summary>
        /// ��ȡ���ݿ��е���ͼģ��
        /// </summary>
        /// <param name="controllerName">��ͼ����</param>
        /// <param name="category">��ͼ����</param>
        /// <returns></returns>
        RazorView GetRazorView(string controllerName, ViewCategory category);


        /// <summary>
        /// ��ȡ���ݿ��е�Ĭ�ϵ���ͼģ��
        /// </summary>
        /// <param name="controllerName">��ͼ����</param>
        /// <returns></returns>
        RazorView GetRazorView(string controllerName);
    }


    ///<summary>
    /// ��ͼ�ֿ�
    ///</summary>
    public class RazorViewRepository : RepositoryBase<RazorView, Guid>, IRazorViewRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RazorViewRepository"/> class.
        /// </summary>
        /// <param name="template">The template.</param>
        public RazorViewRepository(HibernateTemplate template) : base(template)
        {
        }

        /// <summary>
        /// ��ȡ���ݿ��е���ͼģ��
        /// </summary>
        /// <param name="controllerName">��ͼ����</param>
        /// <param name="category">��ͼ����</param>
        /// <returns></returns>
        [Transaction(ReadOnly = true)]
        public RazorView GetRazorView(string controllerName, ViewCategory category)
        {
            var data = Session.CreateCriteria<RazorView>()
                .Add(Expression.Eq("ControllerName", controllerName))
                .Add(Expression.Eq("Category", category))
                .List<RazorView>();

            return data == null || data.Count == 0 ? null : data.First();
        }

        /// <summary>
        /// ��ȡ���ݿ��е�Ĭ�ϵ���ͼģ��
        /// </summary>
        /// <param name="controllerName">��ͼ����</param>
        /// <returns></returns>
        public RazorView GetRazorView(string controllerName)
        {
            return GetRazorView(controllerName, ViewCategory.View);
        }
    }
}