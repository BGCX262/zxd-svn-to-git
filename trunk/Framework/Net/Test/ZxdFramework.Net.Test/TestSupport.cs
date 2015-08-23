using Common.Logging;
using NHibernate;
using Spring.Data.NHibernate.Support;
using NUnit.Framework;
using ZxdFramework.Config;
using ZxdFramework.WebUI.Config;

namespace ZxdFramework
{
    [TestFixture]
    public class TestSupport
    {
        protected SessionScope Scope;
        public ILog Log { private set; get; }

        /// <summary>
        /// 测试之前的初始化必须的对象
        /// </summary>
        [SetUp]
        public virtual void Init()
        {
            
            Log = LogManager.GetCurrentClassLogger();

            Log.Debug("初始化必须的对象 ----->");
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            //HibernatingRhinos.NHibernate.Profiler.Appender.NHibernateProfiler.Initialize();

            var config = new AspMvcConfig();
            //MvcConfig.RootContext.
           Scope = new SessionScope("MySessionFactory".GetInstance<ISessionFactory>(), true);
            

            Log.Debug("测试开始 ----->");
        }



        [TearDown]
        public virtual void ClearUp()
        {
            //"MySessionFactory".GetInstance<ISessionFactory>().GetCurrentSession().Clear();
            Log.Debug("测试结束 <-----");
            Scope.Close();
        }
    }
}
