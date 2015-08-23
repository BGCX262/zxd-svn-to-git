using System;

namespace ZxdFramework.Events
{
    /// <summary>
    /// �����¼�����ӿ�
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// ��ȡ�¼�����
        /// </summary>
        /// <typeparam name="TEventType">The type of the event type.</typeparam>
        /// <returns></returns>
        TEventType GetEvent<TEventType>() where TEventType : EventBase, new();

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <returns></returns>
        EventBase GetEvent(Type eventType);
    }
}