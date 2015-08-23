using System;
using Microsoft.Practices.Prism.Events;
using Microsoft.Scripting;

namespace ZxdFramework.DataContract.Events
{
    /// <summary>
    /// ִ�������¼�. ����¼�������Ҫ���û����߽���ִ��һ������ִ�е��¼�. һ������ģ��֮�������ִ�г���
    /// </summary>
    public class ExecuteCommandEvent : CompositePresentationEvent<ExecuteCommandEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteCommandEvent"/> class.
        /// </summary>
        public ExecuteCommandEvent()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteCommandEvent"/> class.
        /// </summary>
        /// <param name="executeType">�̳���IExecuteCommand������</param>
        public ExecuteCommandEvent(Type executeType)
        {

            Target = executeType;
        }

        private Type _target;
        /// <summary>
        /// ִ�е�Ŀ������, ���õ�ִ�����ͱ���̳��� IExecuteCommand �ӿ�
        /// </summary>
        /// <value>The target.</value>
        public Type Target
        {
            get { return _target; }
            set
            {
                if (!value.IsSubclassOf(typeof(IExecuteCommand)))
                    throw new ArgumentTypeException("�����ִ�����Ͳ����Ǽ̳��� IExecuteCommand");
                _target = value;
            }
        }


        /// <summary>
        /// ִ�й�����Я���Ĳ���
        /// </summary>
        /// <value>The param.</value>
        public object Param { set; get; }

        /// <summary>
        /// Gets the target instance.
        /// </summary>
        /// <returns></returns>
        public IExecuteCommand GetTargetInstance()
        {
            return (IExecuteCommand)Activator.CreateInstance(Target);
        }
    }
}