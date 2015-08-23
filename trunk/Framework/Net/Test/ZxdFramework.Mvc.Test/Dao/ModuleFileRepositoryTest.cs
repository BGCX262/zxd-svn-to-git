using NUnit.Framework;
using ZxdFramework.Dao.System;

namespace ZxdFramework.Mvc.Dao
{
    public class ModuleFileRepositoryTest : TestSupport
    {
        [Test]
        public void TestGet()
        {
            var dao = "ModuleFileRepository".GetInstance<IModuleFileRepository>();
            var data = dao.GetModuleXaml();
            Log.Debug(data);
        }
    }
}