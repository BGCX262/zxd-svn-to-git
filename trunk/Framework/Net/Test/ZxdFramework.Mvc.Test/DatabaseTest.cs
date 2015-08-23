using NHibernate;
using NHibernate.Cfg;
using NHibernate.Impl;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Spring.Data.NHibernate;
using ZxdFramework.Authorization;
using ZxdFramework.Config;
using ZxdFramework.DataContract;
using ZxdFramework.Model;

namespace ZxdFramework.Mvc
{
    public class DatabaseTest : TestSupport
    {
        /// <summary>
        /// ��ʼ�����ݿ�
        /// </summary>
        [Test]
        public void TestCreate()
        {
            //NHibernate.Impl.SessionFactoryImpl
            var config = new Configuration();
            config.Configure("DatabaseCreate.cfg.xml");

            //config.AddAssembly(typeof (IModel).Assembly);
            //config.AddAssembly(typeof (User).Assembly);
            config.AddClass(typeof (User));
            
            Log.Debug("��ʼ�����");
            //config.AddAssembly(typeof())
            var schem = new SchemaExport(config);
            
            //schem.Drop(true, true);
            schem.Create(true, true);
        }
    }
}