using System;

namespace ZxdFramework.Events
{
    /// <summary>
    /// 事件通知标签
    /// </summary>
    public class EventPublishAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventPublishAttribute"/> class.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        public EventPublishAttribute(Type eventType)
        {
            EventType = eventType;
            Pointcut = PubulishPointcut.After;
            IsPropertySet = true;
            //this.GetType().GetConstructor()
            IsConstructorSet = true;
        }

        /// <summary>
        /// 事件类型
        /// </summary>
        /// <value>
        /// The type of the event.
        /// </value>
        public Type EventType { protected set; get; }


        /// <summary>
        /// 事件触发点. 默认之前触发
        /// </summary>
        /// <value>
        /// The pointcut.
        /// </value>
        public PubulishPointcut Pointcut { set; get; }

        /// <summary>
        /// 是否进行属性设置
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is property set; otherwise, <c>false</c>.
        /// </value>
        public bool IsPropertySet { set; get; }


        /// <summary>
        /// 是否进行构造器设置
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is constructor set; otherwise, <c>false</c>.
        /// </value>
        public bool IsConstructorSet { set; get; }
    }


    /// <summary>
    /// 通知的切入点
    /// </summary>
    public enum PubulishPointcut
    {
        /// <summary>
        /// 之前发表
        /// </summary>
        Before,
        /// <summary>
        /// 执行完成后发表
        /// </summary>
        After,
        /// <summary>
        /// 前后各发表一次
        /// </summary>
        Both
    }
}