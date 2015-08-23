using System;
using System.Linq;

namespace ZxdFramework.Events
{
    /// <summary>
    /// 后台事件模型
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload.</typeparam>
    public class PresentationEvent<TPayload> : EventBase
    {
        public SubscriptionToken Subscribe(Action<TPayload> action)
        {
            return Subscribe(action, false);
        }


        /// <summary>
        /// Subscribes a delegate to an event that will be published on the <see cref="ThreadOption.PublisherThread"/>.
        /// </summary>
        /// <param name="action">The delegate that gets executed when the event is published.</param>
        /// <param name="keepSubscriberReferenceAlive">When <see langword="true"/>, the <seealso cref="CompositePresentationEvent{TPayload}"/> keeps a reference to the subscriber so it does not get garbage collected.</param>
        /// <returns>A <see cref="SubscriptionToken"/> that uniquely identifies the added subscription.</returns>
        /// <remarks>
        /// If <paramref name="keepSubscriberReferenceAlive"/> is set to <see langword="false" />, <see cref="CompositePresentationEvent{TPayload}"/> will maintain a <seealso cref="WeakReference"/> to the Target of the supplied <paramref name="action"/> delegate.
        /// If not using a WeakReference (<paramref name="keepSubscriberReferenceAlive"/> is <see langword="true" />), the user must explicitly call Unsubscribe for the event when disposing the subscriber in order to avoid memory leaks or unexepcted behavior.
        /// <para/>
        /// The CompositePresentationEvent collection is thread-safe.
        /// </remarks>
        public SubscriptionToken Subscribe(Action<TPayload> action, bool keepSubscriberReferenceAlive)
        {
            return Subscribe(action, keepSubscriberReferenceAlive, null);
        }


        /// <summary>
        /// Subscribes a delegate to an event.
        /// </summary>
        /// <param name="action">The delegate that gets executed when the event is published.</param>
        /// <param name="threadOption">Specifies on which thread to receive the delegate callback.</param>
        /// <param name="keepSubscriberReferenceAlive">When <see langword="true"/>, the <seealso cref="CompositePresentationEvent{TPayload}"/> keeps a reference to the subscriber so it does not get garbage collected.</param>
        /// <param name="filter">Filter to evaluate if the subscriber should receive the event.</param>
        /// <returns>A <see cref="SubscriptionToken"/> that uniquely identifies the added subscription.</returns>
        /// <remarks>
        /// If <paramref name="keepSubscriberReferenceAlive"/> is set to <see langword="false" />, <see cref="CompositePresentationEvent{TPayload}"/> will maintain a <seealso cref="WeakReference"/> to the Target of the supplied <paramref name="action"/> delegate.
        /// If not using a WeakReference (<paramref name="keepSubscriberReferenceAlive"/> is <see langword="true" />), the user must explicitly call Unsubscribe for the event when disposing the subscriber in order to avoid memory leaks or unexepcted behavior.
        /// 
        /// The CompositePresentationEvent collection is thread-safe.
        /// </remarks>
        public virtual SubscriptionToken Subscribe(Action<TPayload> action, bool keepSubscriberReferenceAlive,
                                                   Predicate<TPayload> filter)
        {
            IDelegateReference actionReference = new DelegateReference(action, keepSubscriberReferenceAlive);
            IDelegateReference filterReference;
            if (filter != null)
            {
                filterReference = new DelegateReference(filter, keepSubscriberReferenceAlive);
            }
            else
            {
                filterReference = new DelegateReference(new Predicate<TPayload>(delegate { return true; }), true);
            }

            var subscription = new EventSubscription<TPayload>(actionReference, filterReference);


            return base.InternalSubscribe(subscription);
        }


        /// <summary>
        /// Publishes the 
        /// </summary>
        /// <param name="payload">Message to pass to the subscribers.</param>
        public virtual void Publish(TPayload payload)
        {
            base.InternalPublish(payload);
        }

        /// <summary>
        /// Removes the first subscriber matching <seealso cref="Action{TPayload}"/> from the subscribers' list.
        /// </summary>
        /// <param name="subscriber">The <see cref="Action{TPayload}"/> used when subscribing to the event.</param>
        public virtual void Unsubscribe(Action<TPayload> subscriber)
        {
            lock (Subscriptions)
            {
                IEventSubscription eventSubscription =
                    Subscriptions.Cast<EventSubscription<TPayload>>().FirstOrDefault(evt => evt.Action == subscriber);
                if (eventSubscription != null)
                {
                    Subscriptions.Remove(eventSubscription);
                }
            }
        }

        /// <summary>
        /// Returns <see langword="true"/> if there is a subscriber matching <seealso cref="Action{TPayload}"/>.
        /// </summary>
        /// <param name="subscriber">The <see cref="Action{TPayload}"/> used when subscribing to the event.</param>
        /// <returns><see langword="true"/> if there is an <seealso cref="Action{TPayload}"/> that matches; otherwise <see langword="false"/>.</returns>
        public virtual bool Contains(Action<TPayload> subscriber)
        {
            IEventSubscription eventSubscription;
            lock (Subscriptions)
            {
                eventSubscription =
                    Subscriptions.Cast<EventSubscription<TPayload>>().FirstOrDefault(evt => evt.Action == subscriber);
            }
            return eventSubscription != null;
        }
    }
}