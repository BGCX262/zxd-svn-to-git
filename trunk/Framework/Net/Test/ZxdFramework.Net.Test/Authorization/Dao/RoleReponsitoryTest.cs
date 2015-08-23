using System.Collections.Generic;
using NUnit.Framework;
using ZxdFramework.DataContract;

namespace ZxdFramework.Authorization.Dao
{
    public class RoleReponsitoryTest : TestSupport
    {
        [Test]
        public void TestCreate()
        {
            var dao = "RoleReponsitory".GetInstance<IRoleReponsitory>();
            var role = new Role
                           {
                               AuthorityScript = "1 + 1",
                               Description = "",
                               Name = "²âÊÔ½ÇÉ«",
                               ActionKeys = new HashSet<string>() { "1", "2", "3" },
                               Children = new HashSet<IRole>()
                                              {
                                                  new Role
                                                      {
                                                          Name = "²âÊÔ×Ó½ÇÉ«",
                                                          AuthorityScript = "true",
                                                          ActionKeys = new HashSet<string>{"1.2", "1.3"}
                                                      }
                                              }
                           };


            var role2 = new Role
                            {
                                AuthorityScript = "0",
                                Name = "½Å±¾½ÇÉ«"
                            };
            
            
            Log.Debug("--------------------------------------------------------------------");
            dao.Add(role);
            dao.Add(role2);
            //dao.Test(role);
            Log.Debug("--------------------------------------------------------------------");
            //dao.Remove(role);
            Log.Debug("--------------------------------------------------------------------");
        }

        [Test]
        public void TestGet()
        {
            var dao = "RoleReponsitory".GetInstance<IRoleReponsitory>();
            var root = dao.GetRootChildren();
        }

        [Test]
        public void TestGetName()
        {
            var dao = "RoleReponsitory".GetInstance<IRoleReponsitory>();

            Assert.IsNotNull(dao["²âÊÔ½ÇÉ«"]);
        }

        [Test]
        public void TestDelete()
        {
            var dao = "RoleReponsitory".GetInstance<IRoleReponsitory>();

            dao.Remove((Role)dao.GetRootChildren()[0]);
        }

    }
}