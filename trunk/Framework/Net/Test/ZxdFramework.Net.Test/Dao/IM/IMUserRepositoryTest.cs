using System.Collections.Generic;
using IronRuby.StandardLibrary.OpenSsl;
using NUnit.Framework;
using ZxdFramework.DataContract.IM;

namespace ZxdFramework.Dao.IM
{
    public class IMUserRepositoryTest : TestSupport
    {
        [Test]
        public void TestCreate()
        {
            var dao = "IIMDao".GetInstance<IMDao>();
            var user = new IMUser();
            user.TcpAddress = "sdfsdf";
            user.Groups = new List<IMUserGroup>()
                              {
                                  new IMUserGroup()
                                      {
                                          Type = IMGroupTypes.×Ô¼º,
                                          Icon = "sdfsdfsdf",
                                          Name = "¸ãÐ¦Ë¹µÙ·Ò¼à¿Ø",
                                          Members = new List<IMUserSetting>()
                                                        {
                                                            new IMUserSetting()
                                                                {
                                                                    Name = "sdfsdf"
                                                                }
                                                        }
                                      }
                              };
            //dao.Add(user);
        }

        [Test]
        public void TestGet()
        {
            var user = "IIMDao".GetInstance<IMDao>();

        }
        [Test]
        public void TestDelete()
        {
            var user = "IIMDao".GetInstance<IMDao>();

        }
    }
}