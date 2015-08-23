using NUnit.Framework;
using ZxdFramework.Authorization.Dao;

namespace ZxdFramework.Authorization
{
    public class RoleTest : TestSupport
    {
        [Test]
        public void TestCreate()
        {
            var role = new Role
                           {
                               Name = "系统管理员",
                               Description = "sadfsadfasdfasdf"
                           };

            var dao = "RoleReponsitory".GetInstance<IRoleReponsitory>();
            dao.Add(role);
        }

        [Test]
        public void TestGet()
        {
            var dao = "RoleReponsitory".GetInstance<IRoleReponsitory>();
            Log.Debug(dao.GetRoot().Name);
        }
    }
}