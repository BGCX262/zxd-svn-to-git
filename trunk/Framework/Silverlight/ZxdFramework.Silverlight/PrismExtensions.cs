using System;
using Microsoft.Practices.Prism.Events;

namespace ZxdFramework
{
    /// <summary>
    ///��ģ���ܵ���չ����
    /// </summary>
    public static class PrismExtensions
    {
        #region EventAggregator

        /// <summary>
        /// ����һ���¼�ģ��
        /// </summary>
        /// <typeparam name="TMessageType">ģ�͵�����</typeparam>
        /// <param name="aggregator">�¼�������</param>
        /// <param name="message">�¼�����</param>
        public static void Publish<TMessageType>(
            this IEventAggregator aggregator, TMessageType message)
              where TMessageType : CompositePresentationEvent<TMessageType>, new()
        {
            aggregator.GetEvent<TMessageType>().Publish(message);
        }



        /// <summary>
        /// ע��һ���¼�ִ�з���
        /// </summary>
        /// <typeparam name="TMessageType">�¼�����</typeparam>
        /// <param name="aggregator">The aggregator.</param>
        /// <param name="action">ִ�з���</param>
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
        /// ע��һ���¼�ִ�з���
        /// </summary>
        /// <typeparam name="TMessageType">�¼�����</typeparam>
        /// <param name="aggregator">The aggregator.</param>
        /// <param name="action">ִ�з���</param>
        public static void Unsubscribe<TMessageType>(
          this IEventAggregator aggregator, Action<TMessageType> action)
            where TMessageType : CompositePresentationEvent<TMessageType>, new()
        {
            aggregator.GetEvent<TMessageType>().Unsubscribe(action);
        }
        #endregion
    }
}