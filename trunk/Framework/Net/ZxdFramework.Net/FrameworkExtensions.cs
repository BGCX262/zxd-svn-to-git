using System;
using Spring.Context;
using ZxdFramework.Config;
using ZxdFramework.Events;

namespace ZxdFramework
{
    /// <summary>
    /// 框架中的扩展类
    /// </summary>
    public static class FrameworkExtensions
    {
        /// <summary>
        /// 从对象容器中获取一个对象的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectName">Name of the object.</param>
        /// <returns></returns>
        public static T GetInstance<T>(this string objectName)
        {
            return (T) BaseConfig.RootContext.GetObject(objectName, typeof (T));
        }


        /// <summary>
        /// 获取类型后的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static T GetTypedObject<T>(this IApplicationContext context, string name)
        {
            return (T)context.GetObject(name);
        }

    }
}