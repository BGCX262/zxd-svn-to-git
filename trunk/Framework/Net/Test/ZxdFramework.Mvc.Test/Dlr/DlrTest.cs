using System;
using System.Collections.Generic;
using IronPython.Hosting;
using NUnit.Framework;
using Spring.Context;
using ZxdFramework.Mvc.Config;

namespace ZxdFramework.Mvc.Dlr
{
    public class DlrTest : TestSupport
    {
        [Test]
        public void Test1()
        {
            

           //var runtime = Python.CreateRuntimeSetup(new Dictionary<string, object>());
            var engine = Python.CreateEngine(AppDomain.CurrentDomain);
            Log.Debug(engine.LanguageVersion);
            var data = engine.Execute("1 + 1");
            Log.Debug(data);
        }
    }

    internal class MyClass : IApplicationEventListener
    {
        public void HandleApplicationEvent(object sender, ApplicationEventArgs e)
        {
            
        }
    }
}