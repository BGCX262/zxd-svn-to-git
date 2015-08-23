using NUnit.Framework;
using ZxdFramework.Script.Dao;

namespace ZxdFramework.Script
{
    public class RubyTemplateTest : TestSupport
    {
        [Test]
        public void TestGet()
        {
            var item = "RubyTemplateRespository".GetInstance<IRubyTemplateRepository>();
            foreach (var VARIABLE in item)
            {
                
            }
        }

        [Test]
        public void TestDelete()
        {
            var item = "RubyTemplateRespository".GetInstance<IRubyTemplateRepository>();
            foreach (var VARIABLE in item)
            {
                item.Remove(VARIABLE);
            }
        }

        [Test]
        public void TestSave()
        {
            var tem = new RubyTemplate();
            
            tem.ModuleName = "Zxd.Script";
            tem.AliasName = "重云";
            tem.ScriptName = "Zxd";
            tem.Content = "1 + 1";
            tem.Requires.Add("sdfsadfasf");
            tem.Requires.Add("sdfaaaaaa");
            tem.Footnode.Add("----------------");
            tem.Footnode.Add("--------------------");
            tem.Roles.Add("role");
            tem.Users.Add("user");
            var item = "RubyTemplateRespository".GetInstance<IRubyTemplateRepository>();
            item.Add(tem);
        }


        [Test]
        public void TestTemplate()
        {
            var tem = new RubyTemplate();
            tem.ModuleName = "Zxd.Script";
            tem.AliasName = "重云";
            tem.ScriptName = "Zxd";
            //tem.Content = "1 + 1";

            //tem.ToScript();
        }
    }
}