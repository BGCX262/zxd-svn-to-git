using System;
using System.Collections.Generic;
using NHibernate;
using Spring.Transaction.Interceptor;
using ZxdFramework.DataContract;

namespace ZxdFramework.Dao
{
    /// <summary>
    /// 定义了 Dao 层的基本操作方法, 并且这这些执行的方法都携带了容器管理的事务
    /// </summary>
    public interface IDaoSupport
    {
        /// <summary>
        /// 保存一个模型对象
        /// </summary>
        /// <param name="model">The model.</param>
        void Save(IModel model);

        /// <summary>
        /// 快速保存一个对象. 效率最高. 但是不能连级保存关联对象. 也没有连及事务
        /// </summary>
        /// <param name="model">The model.</param>
        void FastSave(IModel model);

        /// <summary>
        /// 保存或者更新一个模型对象
        /// </summary>
        /// <param name="model">The model.</param>
        void SaveOrUpdate(IModel model);
        /// <summary>
        /// 更新一个模型对象
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(IModel model);
        /// <summary>
        /// 删除一个模型对象
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(IModel model);
        /// <summary>
        /// 获取 一个模型对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        T Get<T>(object id);

        /// <summary>
        /// 获取数据库的全部的对象模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<T> GetAll<T>();

        /// <summary>
        /// 获取数据库中的对象
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        object Get(Type type, object id);
    }


    /// <summary>
    /// 支持最基本的Dao功能
    /// </summary>
    public class DaoSupport : IDaoSupport
    {

        protected DaoSupport(ISessionFactory factory)
        {
            SessionFactory = factory;
        }

        /// <summary>
        /// 获取 Session
        /// </summary>
        public ISessionFactory SessionFactory
        {
            //get { return "MySessionFactory".GetInstance<ISessionFactory>(); }
            protected set; get; }

        /// <summary>
        /// 获取Session
        /// </summary>
        public ISession Session { get { return SessionFactory.GetCurrentSession(); } }

        /// <summary>
        /// 获取容器中的基本对象, 由于分层的完整性. 这个绕过数据层的操作类. 只能是在内部操作
        /// </summary>
        /// <returns></returns>
        internal static IDaoSupport GetInstance()
        {
            return "DaoSupport".GetInstance<IDaoSupport>();
        }

        /// <summary>
        /// Saves the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        [Transaction]
        public virtual void Save(IModel model)
        {
            var session = SessionFactory.GetCurrentSession();
            session.Save(model);
            //session.Flush();
        }

        /// <summary>
        /// 快速保存一个对象. 效率最高. 但是不能连级保存关联对象. 也没有连及事务
        /// </summary>
        /// <param name="model">The model.</param>
        public void FastSave(IModel model)
        {
            var session = SessionFactory.OpenStatelessSession();
            var tx = session.BeginTransaction();
            try
            {
                session.Insert(model);
                tx.Commit();
            }
            catch (Exception)
            {
                tx.Rollback();
                throw;
            }
            finally
            {
                session.Close();    
            }
        }

        [Transaction]
        public virtual void SaveOrUpdate(IModel model)
        {
            var session = SessionFactory.GetCurrentSession();
            session.SaveOrUpdate(model);
            //session.Flush();
        }


        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        [Transaction]
        public virtual void Update(IModel model)
        {
            Session.Update(model);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        [Transaction]
        public virtual void Delete(IModel model)
        {
            Session.Delete(model);
        }

        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [Transaction(ReadOnly = true)]
        public virtual T Get<T>(object id)
        {
            return SessionFactory.GetCurrentSession().Get<T>(id);
        }

        /// <summary>
        /// Gets the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [Transaction(ReadOnly = true)]
        public virtual object Get(Type type, object id)
        {
            return Session.Get(type, id);
        }

        /// <summary>
        /// 获取数据库的全部的对象模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Transaction(ReadOnly = true)]
        public IList<T> GetAll<T>()
        {
            return Session.CreateCriteria(typeof (T)).List<T>();
        }

    }
}