using System;
using Spring.Context;
using ZxdFramework.Config;
using ZxdFramework.Events;

namespace ZxdFramework
{
    /// <summary>
    /// ����е���չ��
    /// </summary>
    public static class FrameworkExtensions
    {
        /// <summary>
        /// �Ӷ��������л�ȡһ�������ʵ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectName">Name of the object.</param>
        /// <returns></returns>
        public static T GetInstance<T>(this string objectName)
        {
            return (T) BaseConfig.RootContext.GetObject(objectName, typeof (T));
        }


        /// <summary>
        /// ��ȡ���ͺ�Ķ���
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