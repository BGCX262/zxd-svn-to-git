using NUnit.Framework;
using ZxdFramework.Events;
using ZxdFramework.Mvc.Config;

namespace ZxdFramework.Mvc.Events
{
    public class EventTest : TestSupport
    {
        [Test]
        public void TestE()
        {
            Log.Debug(MvcConfig.EventAggregator);
            MvcConfig.EventAggregator.Subscribe<MyEvent>(LogName);
            MvcConfig.EventAggregator.Publish(new MyEvent()
                                                  {
                                                      Name = "TEst OK"
                                                  });
            MvcConfig.EventAggregator.Unsubscribe<MyEvent>(LogName);
            MvcConfig.EventAggregator.Publish(new MyEvent()
            {
                Name = "TEst OK"
            });
            
        }


        public void LogName(MyEvent @event)
        {
            Log.Debug(@event.Name);
        }
    }


    public class MyEvent : PresentationEvent<MyEvent>
    {
        public string Name { set; get; }
    }
}