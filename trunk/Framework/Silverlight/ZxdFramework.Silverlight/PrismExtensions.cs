using System;
using Microsoft.Practices.Prism.Events;

namespace ZxdFramework
{
    /// <summary>
    ///　模块框架的扩展方法
    /// </summary>
    public static class PrismExtensions
    {
        #region EventAggregator

        /// <summary>
        /// 触发一个事件模型
        /// </summary>
        /// <typeparam name="TMessageType">模型的类型</typeparam>
        /// <param name="aggregator">事件触发器</param>
        /// <param name="message">事件对象</param>
        public static void Publish<TMessageType>(
            this IEventAggregator aggregator, TMessageType message)
              where TMessageType : CompositePresentationEvent<TMessageType>, new()
        {
            aggregator.GetEvent<TMessageType>().Publish(message);
        }



        /// <summary>
        /// 注册一个事件执行方法
        /// </summary>
        /// <typeparam name="TMessageType">事件类型</typeparam>
        /// <param name="aggregator">The aggregator.</param>
        /// <param name="action">执行方法</param>
        public static void Subscribe<TMessageType>(
          this IEventAggregator aggregator, Action<TMessageType> action)
            where TMessageType : CompositePresentationEvent<TMessageType>, new()
        {
            aggregator.GetEvent<TMessageType>().Subscribe(action);
        }


        /// <summary>
        /// Subscribes the specified aggregator.
        /// </summary>
        /// <typeparam name="TMessageType">The type of the message type.</typeparam>
        /// <param name="aggregator">The aggregator.</param>
        /// <param name="action">The action.</param>
        /// <param name="option">The option.</param>
        public static void Subscribe<TMessageType>(
          this IEventAggregator aggregator, Action<TMessageType> action, ThreadOption option)
            where TMessageType : CompositePresentationEvent<TMessageType>, new()
        {
            aggregator.GetEvent<TMessageType>().Subscribe(action, option);
        }

        /// <summary>
        /// 注销一个事件执行方法
        /// </summary>
        /// <typeparam name="TMessageType">事件类型</typeparam>
        /// <param name="aggregator">The aggregator.</param>
        /// <param name="action">执行方法</param>
        public static void Unsubscribe<TMessageType>(
          this IEventAggregator aggregator, Action<TMessageType> action)
            where TMessageType : CompositePresentationEvent<TMessageType>, new()
        {
            aggregator.GetEvent<TMessageType>().Unsubscribe(action);
        }
        #endregion
    }
}