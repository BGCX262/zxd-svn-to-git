using System;
using System.Collections.Generic;

namespace ZxdFramework.Events
{
    /// <summary>
    /// 事件管理类型
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, EventBase> events = new Dictionary<Type, EventBase>();

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <typeparam name="TEventType">The type of the event type.</typeparam>
        /// <returns></returns>
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            EventBase existingEvent = null;

            if (!this.events.TryGetValue(typeof(TEventType), out existingEvent))
            {
                var newEvent = new TEventType();
                this.events[typeof(TEventType)] = newEvent;

                return newEvent;
            }
            else
            {
                return (TEventType)existingEvent;
            }
        }

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <returns></returns>
        public EventBase GetEvent(Type eventType)
        {
            EventBase existingEvent = null;

            if (!this.events.TryGetValue(eventType, out existingEvent))
            {
                var newEvent = (EventBase)Activator.CreateInstance(eventType);
                this.events[eventType] = newEvent;

                return newEvent;
            }
            else
            {
                return (EventBase)existingEvent;
            }
        }


    }


    /// <summary>
    /// 事件管理的扩展
    /// </summary>
    public static class EventAggregatorExtensions
    {
        #region EventAggregator

        /// <summary>
        /// Publishes the specified aggregator.
        /// </summary>
        /// <typeparam name="TMessageType">The type of the message type.</typeparam>
        /// <param name="aggregator">The aggregator.</param>
        /// <param name="message">The message.</param>
        public static void Publish<TMessageType>(
            this IEventAggregator aggregator, TMessageType message)
              where TMessageType : PresentationEvent<TMessageType>, new()
        {
            aggregator.GetEvent<TMessageType>().Publish(message);
        }


        /// <summary>
        /// Publishes the specified aggregator.
        /// </summary>
        /// <param name="aggregator">The aggregator.</param>
        /// <param name="eventModel">The event model.</param>
        public static void Publish(this IEventAggregator aggregator, EventBase eventModel)
        {
            var data = aggregator.GetEvent(eventModel.GetType());
            var method = data.GetType().GetMethod("Publish");
            if (method != null)
            {
                method.Invoke(data, new object[]{eventModel});
            }
        }

        /// <summary>
        /// Subscribes the specified aggregator.
        /// </summary>
        /// <typeparam name="TMessageType">The type of the message type.</typeparam>
        /// <param name="aggregator">The aggregator.</param>
        /// <param name="action">The action.</param>
        public static void Subscribe<TMessageType>(
          this IEventAggregator aggregator, Action<TMessageType> action)
            where TMessageType : PresentationEvent<TMessageType>, new()
        {
            //aggregator.GetEvent<TMessageType>().Subscribe(action, ThreadOption.PublisherThread, false, Dummy);
            aggregator.GetEvent<TMessageType>().Subscribe(action);
        }

        //public static bool Dummy(object dummy) 
        //{
        //    return true;
        //}

        /// <summary>
        /// Unsubscribes the specified aggregator.
        /// </summary>
        /// <typeparam name="TMessageType">The type of the message type.</typeparam>
        /// <param name="aggregator">The aggregator.</param>
        /// <param name="action">The action.</param>
        public static void Unsubscribe<TMessageType>(
          this IEventAggregator aggregator, Action<TMessageType> action)
            where TMessageType : PresentationEvent<TMessageType>, new()
        {
            aggregator.GetEvent<TMessageType>().Unsubscribe(action);
        }
        #endregion
    }
}