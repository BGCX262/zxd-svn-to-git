using System;
using System.Collections.Generic;
using IronRuby;
using Microsoft.Scripting.Hosting;

namespace ZxdFramework.Net.Script {
    ///<summary>
    ///</summary>
    public static class Bootstrap {
        ///<summary>
        ///</summary>
        ///<param name="runtimeAssembyTypes"></param>
        ///<param name="interpretedMode"></param>
        ///<param name="debugMode"></param>
        ///<returns></returns>
        public static ScriptRuntime CreateIronRubyRuntime(IEnumerable<Type> runtimeAssembyTypes, bool interpretedMode, bool debugMode) {
            var rubySetup = Ruby.CreateRubySetup();
            rubySetup.Options["InterpretedMode"] = interpretedMode;

            var runtimeSetup = new ScriptRuntimeSetup();
            runtimeSetup.LanguageSetups.Add(rubySetup);
            runtimeSetup.DebugMode = debugMode;

            var scriptRuntime = Ruby.CreateRuntime(runtimeSetup);

            foreach (var type in runtimeAssembyTypes)
            {
                scriptRuntime.LoadAssembly(type.Assembly);
            }
            return scriptRuntime;
        }

    }
}
