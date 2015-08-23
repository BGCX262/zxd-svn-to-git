/**
 * 创建者：宗旭东
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Spring.Data.NHibernate.Generic;
using Spring.Data.NHibernate.Generic.Support;
using Spring.Stereotype;
using Spring.Transaction.Interceptor;
using ZxdFramework.DataContract;
using ZxdFramework.Model;

namespace ZxdFramework.Dao
{

    /// <summary>
    /// 基本的Dao支持, 如果一个模型不需要负责的操作就可以直接实例化这个类并传入模型类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId">The type of the id.</typeparam>
    [Repository]
    public class RepositoryBase<T, TId> : HibernateDaoSupport, IRepositoryBase<T, TId> 
        where T : Entity<TId>
        where TId : IComparable
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase&lt;T, TId&gt;"/> class.
        /// </summary>
        /// <param name="template">The template.</param>
        public RepositoryBase(HibernateTemplate template)
        {
            HibernateTemplate = template;
        }

        /// <summary>
        /// 枚举数据库中的数据集. 最大只能枚举1000条数据
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Session.Query<T>().ToList().Take(1000).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 获取数据库或者Session缓存中的对象
        /// </summary>
        /// <value></value>
        public virtual T this[TId id]
        {
            get { return Get(id); }
        }

        object IRepository.this[object id]
        {
            get { return this[(TId)id]; }
        }

        [Transaction]
        public virtual void Add(IModel item)
        {
            Add((T)item);
        }

        public void ClearSession()
        {
            Session.Clear();
        }

        public object Get(object id)
        {
            return Session.Get<T>(id);
        }

        public T Get(TId id)
        {
           return Session.Get<T>(id);
        }

        [Transaction]
        public virtual void Add(T item)
        {
            //HibernateTemplate.Save(item);
            HibernateTemplate.SessionFactory.GetCurrentSession().Save(item);
        }

        [Transaction]
        public virtual void Remove(T item)
        {
            HibernateTemplate.Delete(item);
        }

        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// 	<c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        [Transaction(ReadOnly = true)]
        public virtual bool Contains(T item)
        {
            return Session.Query<T>().Contains(item);
        }

        /// <summary>
        /// 获取数据库中的记录总数
        /// </summary>
        /// <value>The count.</value>
        public virtual int Count
        {
            get { return Session.Query<T>().Count(); }
        }


    }
}