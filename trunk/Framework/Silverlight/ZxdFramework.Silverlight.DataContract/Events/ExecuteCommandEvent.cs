using System;
using Microsoft.Practices.Prism.Events;
using Microsoft.Scripting;

namespace ZxdFramework.DataContract.Events
{
    /// <summary>
    /// 执行命令事件. 这个事件对象主要有用户或者界面执行一个请求执行的事件. 一般用来模块之间的启用执行程序
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
        /// <param name="executeType">继承了IExecuteCommand的类型</param>
        public ExecuteCommandEvent(Type executeType)
        {

            Target = executeType;
        }

        private Type _target;
        /// <summary>
        /// 执行的目标类型, 设置的执行类型必须继承与 IExecuteCommand 接口
        /// </summary>
        /// <value>The target.</value>
        public Type Target
        {
            get { return _target; }
            set
            {
                if (!value.IsSubclassOf(typeof(IExecuteCommand)))
                    throw new ArgumentTypeException("传入的执行类型并不是继承与 IExecuteCommand");
                _target = value;
            }
        }


        /// <summary>
        /// 执行过程中携带的参数
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