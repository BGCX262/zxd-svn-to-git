using System.Web;
using Microsoft.Scripting.Hosting;

namespace ZxdFramework.Mvc.Engines.Ruby.Extensions {
    /// <summary>
    /// 脚本运行环境
    /// </summary>
    public static class ScriptRuntimeExtensions {
        const string APPKEY_SCRIPTRUNTIME = "__ScriptRuntime__";

        public static ScriptRuntime GetScriptRuntime(this HttpApplicationStateBase app) {
            return (ScriptRuntime)app[APPKEY_SCRIPTRUNTIME];
        }

        public static void SetScriptRuntime(this HttpApplicationState app, ScriptRuntime scriptRuntime) {
            app[APPKEY_SCRIPTRUNTIME] = scriptRuntime;
        }
    }
}