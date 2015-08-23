using System;

namespace ZxdFramework.Events
{
    /// <summary>
    /// 定义事件处理接口
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// 获取事件对象
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