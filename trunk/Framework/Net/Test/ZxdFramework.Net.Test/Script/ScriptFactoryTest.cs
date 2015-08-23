using System;
using IronRuby;
using NUnit.Framework;
using System.Collections.Generic;

namespace ZxdFramework.Script
{
    public class ScriptFactoryTest : TestSupport
    {
        private static string _test = "sdfsadfasdfsd";
        public static string TestStr { get { return _test; } }

        [Test]
        public void Test()
        {
            var fatory = "ScriptFactory".GetInstance<ScriptFactory>();
            Log.Debug("0 ---- ");
            var runtime = fatory.CreateIronRubyRuntime(new List<Type> { typeof(ScriptFactoryTest) }, true, false);
            var ty = typeof (DateTime).Assembly.FullName;
            
            //runtime.LoadAssembly(typeof(object).Assembly);
            Log.Debug("1 ---- " + ty);
            var ruby = runtime.GetEngine("ruby");

            const string script = @"
#require 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
module Zxd
    V重视 = '中文';
    alias :V变量 :V重视
    module Json
        SSS = 2334;
    end
end

Zxd::V变量
";

            Log.Debug(ruby.Execute(script));


        }

        [Test]
        public void TestScript()
        {
            var test = "1 + 1 \n";
            var fatory = "ScriptFactory".GetInstance<ScriptFactory>();
            Log.Debug("0 ---- ");
            var runtime = fatory.CreateIronRubyRuntime(new List<Type> { typeof(ScriptFactoryTest) }, true, false);

            var eng = runtime.GetEngine("ruby");

            var result = eng.Execute(test);

            

            Log.Debug(result);

        }
    }
}