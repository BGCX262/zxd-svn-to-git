using Microsoft.Scripting.Hosting;

namespace ZxdFramework.Net.Script
{
    /// <summary>
    /// 定义当前的类型是否支持脚本运行
    /// </summary>
    public interface IScriptRunable
    {
        /// <summary>
        /// 执行当前对象中定义的脚本
        /// </summary>
        /// <param name="engine">脚本运行引擎</param>
        /// <param name="scope">运行环境</param>
        /// <returns>
        /// 返回脚执行的结果
        /// </returns>
        object ScriptRun(ScriptEngine engine, ScriptScope scope);

    }
}