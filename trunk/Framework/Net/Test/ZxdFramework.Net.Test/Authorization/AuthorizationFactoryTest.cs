using NUnit.Framework;
using ZxdFramework.Authorization.Dao;
using ZxdFramework.Json;

namespace ZxdFramework.Authorization
{
    public class AuthorizationFactoryTest : TestSupport
    {
        [Test]
        public void TestGet()
        {
            var factory = AuthorizationFactory.Instance;

            factory.AddSecurityAction(new AuthResource());
            var data = new Serializer(factory.RootAction)
            .Ignore("Icon")
            .Ignore("Name")
            .Ignore("Description")
            .ToString();
            Log.Debug(data);
        }

        [Test]
        public void TestCheck()
        {
            var factory = AuthorizationFactory.Instance;
            var accountDao = "AccountRepository".GetInstance<IAccountRepository>();
            var user = accountDao.GetUser("zongxudong");
            Log.Debug(user);
            factory.CheckSecurityAction(user, factory.RootAction, true);
        }

    }


    public class AuthResource : SecurityActionResource
    {
        [SecurityAction("Ȩ��ģ��", Description = "mms�෢")]
        public const string Auth = "auth";

        [SecurityAction("Ȩ������", Description = "sadfasfasfsadfsa")]
        public const string AuthAdd = "auth.add";
    }
}