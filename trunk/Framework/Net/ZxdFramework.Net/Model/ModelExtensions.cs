using System;
using Spring.Data.NHibernate;
using ZxdFramework.Config;
using ZxdFramework.Dao;
using ZxdFramework.DataContract;
using ZxdFramework.Json;
using HibernateTemplate = Spring.Data.NHibernate.Generic.HibernateTemplate;

namespace ZxdFramework.Model
{
    /// <summary>
    /// 模型对象的扩展方法
    /// </summary>
    public static class ModelExtensions
    {

        /// <summary>
        /// 获取模型序列化对象
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static ISerializer GetSerializer(this object model)
        {
            return new Serializer(model);
        }

        /// <summary>
        /// 将当前的对象直接保存到数据库
        /// </summary>
        /// <param name="model">The model.</param>
        public static void Save(this IModelDao model)
        {
            DaoSupport.GetInstance().Save(model);
        }

        /// <summary>
        /// 将当前的对象更新到存储中
        /// </summary>
        /// <param name="model">The model.</param>
        public static void Update(this IModelDao model)
        {
            DaoSupport.GetInstance().Update(model);
        }

        /// <summary>
        /// 删除存储中的这个对象
        /// </summary>
        /// <param name="model">The model.</param>
        public static void Delete(this IModelDao model)
        {
            DaoSupport.GetInstance().Delete(model);
        }

        /// <summary>
        /// 保存或者更新当前对象
        /// </summary>
        /// <param name="model">The model.</param>
        public static void SaveOrUpdate(this IModelDao model)
        {
            DaoSupport.GetInstance().SaveOrUpdate(model);
        }

        /// <summary>
        /// 快速保存当前对象. 忽略缓存等高级功能
        /// </summary>
        /// <param name="model">The model.</param>
        public static void FastSave(this IModelDao model)
        {
            DaoSupport.GetInstance().FastSave(model);
        }

        /// <summary>
        /// 获取存储中的当前类型的对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="id">主键</param>
        public static object Get(this Type type, object id)
        {
            return DaoSupport.GetInstance().Get(type, id);
        }
    }
}