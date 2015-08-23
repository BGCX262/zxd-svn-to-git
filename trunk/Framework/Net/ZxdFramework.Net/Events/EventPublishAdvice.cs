using System;
using AopAlliance.Intercept;
using Common.Logging;

namespace ZxdFramework.Events
{
    /// <summary>
    /// 事件标签拦截器
    /// </summary>
    public class EventPublishAdvice : IMethodInterceptor
    {
        private ILog _log = LogManager.GetCurrentClassLogger();

        public EventPublishAdvice(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        /// <summary>
        /// Gets or sets the event aggregator.
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        public IEventAggregator EventAggregator { protected set; get; }

        public object Invoke(IMethodInvocation invocation)
        {
            var trs = (EventPublishAttribute)invocation.Method.GetCustomAttributes(typeof(EventPublishAttribute), false)[0];

            object returnValue = null;

            try
            {

                if (trs.Pointcut == PubulishPointcut.Before || trs.Pointcut == PubulishPointcut.Both)
                {
                    if(_log.IsDebugEnabled)_log.Debug("拦截到一个事件触发方法 前置:" + invocation.TargetType.FullName + ", " + invocation.Method.Name);
                    EventAggregator.Publish(GetEventModel(trs, invocation));
                }

                returnValue = invocation.Proceed();

                if (trs.Pointcut == PubulishPointcut.After || trs.Pointcut == PubulishPointcut.Both)
                {
                    if (_log.IsDebugEnabled) _log.Debug("拦截到一个事件触发方法 后置:" + invocation.TargetType.FullName + ", " + invocation.Method.Name);
                    EventAggregator.Publish(GetEventModel(trs, invocation));
                }

            }
            catch (Exception)
            {
                
                throw;
            }

            return returnValue;
        }

        private EventBase GetEventModel(EventPublishAttribute at, IMethodInvocation invocation)
        {
            var data = (EventBase)Activator.CreateInstance(at.EventType);

            foreach (var propertyInfo in at.EventType.GetProperties())
            {
                if (propertyInfo.CanWrite)
                {
                    foreach (var args in invocation.Arguments)
                    {
                        if (propertyInfo.PropertyType.IsInstanceOfType(args))
                        {
                            propertyInfo.SetValue(data, args, null);
                        }
                    }
                }
            }

            return data;
        }
        
    }
}