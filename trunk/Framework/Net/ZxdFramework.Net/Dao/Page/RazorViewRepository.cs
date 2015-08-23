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
    /// 视图仓库接口
    ///</summary>
    public interface IRazorViewRepository : IRepository
    {
        /// <summary>
        /// 获取数据库中的视图模板
        /// </summary>
        /// <param name="controllerName">视图名称</param>
        /// <param name="category">视图类型</param>
        /// <returns></returns>
        RazorView GetRazorView(string controllerName, ViewCategory category);


        /// <summary>
        /// 获取数据库中的默认的视图模板
        /// </summary>
        /// <param name="controllerName">视图名称</param>
        /// <returns></returns>
        RazorView GetRazorView(string controllerName);
    }


    ///<summary>
    /// 视图仓库
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
        /// 获取数据库中的视图模板
        /// </summary>
        /// <param name="controllerName">视图名称</param>
        /// <param name="category">视图类型</param>
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
        /// 获取数据库中的默认的视图模板
        /// </summary>
        /// <param name="controllerName">视图名称</param>
        /// <returns></returns>
        public RazorView GetRazorView(string controllerName)
        {
            return GetRazorView(controllerName, ViewCategory.View);
        }
    }
}