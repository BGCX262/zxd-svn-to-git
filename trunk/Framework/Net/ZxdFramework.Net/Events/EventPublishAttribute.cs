using System;

namespace ZxdFramework.Events
{
    /// <summary>
    /// �¼�֪ͨ��ǩ
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
        /// �¼�����
        /// </summary>
        /// <value>
        /// The type of the event.
        /// </value>
        public Type EventType { protected set; get; }


        /// <summary>
        /// �¼�������. Ĭ��֮ǰ����
        /// </summary>
        /// <value>
        /// The pointcut.
        /// </value>
        public PubulishPointcut Pointcut { set; get; }

        /// <summary>
        /// �Ƿ������������
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is property set; otherwise, <c>false</c>.
        /// </value>
        public bool IsPropertySet { set; get; }


        /// <summary>
        /// �Ƿ���й���������
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is constructor set; otherwise, <c>false</c>.
        /// </value>
        public bool IsConstructorSet { set; get; }
    }


    /// <summary>
    /// ֪ͨ�������
    /// </summary>
    public enum PubulishPointcut
    {
        /// <summary>
        /// ֮ǰ����
        /// </summary>
        Before,
        /// <summary>
        /// ִ����ɺ󷢱�
        /// </summary>
        After,
        /// <summary>
        /// ǰ�������һ��
        /// </summary>
        Both
    }
}