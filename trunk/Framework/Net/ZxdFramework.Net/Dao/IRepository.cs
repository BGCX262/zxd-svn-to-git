using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Spring.Data.NHibernate.Generic;
using Spring.Data.NHibernate.Generic.Support;
using ZxdFramework.DataContract;
using ZxdFramework.Model;

namespace ZxdFramework.Dao
{

    /// <summary>
    /// 定义数据仓库接口. 提供用于当前仓库支持枚举循环的操作
    /// </summary>
    public interface IRepository : IEnumerable
    {
        /// <summary>
        /// 使用对象主键从数据库中获取这个对象 <see cref="T"/> with the specified id.
        /// </summary>
        object this[object id] { get; }

        /// <summary>
        /// 增加一個對象到數據庫中
        /// </summary>
        /// <param name="item">The item.</param>
        void Add(IModel item);

        /// <summary>
        /// 对当前的Session 清除
        /// </summary>
        void ClearSession();

        /// <summary>
        /// 获取仓库里面的对象
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        object Get(object id);
    }

    /// <summary>
    /// 数据仓库实现接口.
    /// 实现了这个接口的类型, 就可以提供操作集合类的方式控制模型对象
    /// </summary>
    /// <typeparam name="T">数据模型的类型</typeparam>
    /// <typeparam name="TId">主键类型</typeparam>
    public interface IRepositoryBase<T, TId> : IRepository, IEnumerable<T>
        where T : Entity<TId> 
        where TId : IComparable
    {

        /// <summary>
        /// 使用对象主键从数据库中获取这个对象 <see cref="T"/> with the specified id.
        /// </summary>
        /// <value>返回获取到的对象</value>
        T this[TId id] { get; }


        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        T Get(TId id);

        /// <summary>
        /// 提供向数据库集合中增加一个对象数据
        /// </summary>
        /// <param name="item">The item.</param>
        void Add(T item);


        /// <summary>
        /// 在数据库集合中删除一个对象
        /// </summary>
        /// <param name="item">The item.</param>
        void Remove(T item);


        /// <summary>
        /// 当前的数据库是否包含了这个对象
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// 	<c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        bool Contains(T item);


        /// <summary>
        /// 返回数据库中有多少条数据
        /// </summary>
        /// <value>The count.</value>
        int Count { get; }

    }
}