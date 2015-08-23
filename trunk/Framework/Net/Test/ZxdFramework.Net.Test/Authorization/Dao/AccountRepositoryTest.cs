using NUnit.Framework;
using ZxdFramework.Json;

namespace ZxdFramework.Authorization.Dao
{
    public class AccountRepositoryTest : TestSupport
    {
        [Test]
        public void TestCreate()
        {
            var user = new User
                           {
                               Name = "×ÚÐñ¶«",
                               LoginName = "zong",
                               Password = "zong",
                               RoleNames = new [] { "²âÊÔ½ÇÉ«" }
                           };
            var dao = "AccountRepository".GetInstance<IAccountRepository>();
            dao.CreateUser(user);
        }

        [Test]
        public void TestGet()
        {
            var dao = "AccountRepository".GetInstance<IAccountRepository>();
            var user = dao.GetUser("Guest");

            user.RoleNames = null;

            Log.Debug(JsonConverter.Serialize(user));
            Assert.IsNotNull(user);
            Log.Debug(user.Name);
        }

        [Test]
        public void TestGetPhoto()
        {
            var dao = "AccountRepository".GetInstance<IAccountRepository>();
            var ph = dao.GetUserPhoto("Guest");

            Log.Debug(ph);
        }

        [Test]
        public void TestUserSetting()
        {
            var dao = "AccountRepository".GetInstance<IAccountRepository>();
            var user = dao.GetUser("Guest");

            var setting = new UserSetting()
                              {
                                  Key = "Key",
                                  OwnerId = user.Id,
                                  Value = "111111111111111111111111"
                              };

            dao.SaveOrUpdateSetting(setting);
        }

        [Test]
        public void TestGetSetting()
        {
            var dao = "AccountRepository".GetInstance<IAccountRepository>();
            var user = dao.GetUser("administrator");

            var data = dao.GetUserSettings(user.Id);

            Log.Debug(data);
        }
    }
}