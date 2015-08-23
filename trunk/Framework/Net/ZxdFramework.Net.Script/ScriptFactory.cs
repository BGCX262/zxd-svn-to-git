using System;
using System.Collections.Generic;
using IronRuby;
using Microsoft.Scripting.Hosting;
using ZxdFramework.DataContract;

namespace ZxdFramework.Script
{
    /// <summary>
    /// 定义脚本的工厂
    /// </summary>
    public interface IScriptFactory
    {
        /// <summary>
        /// 获取脚本运行环境
        /// </summary>
        ScriptRuntime Runtime { get; }

        /// <summary>
        /// 创建默认的脚本运行环境
        /// </summary>
        /// <returns></returns>
        ScriptRuntime CreateIronRubyRuntime();

        /// <summary>
        /// 创建脚本运行环境
        /// </summary>
        /// <param name="runtimeAssembyTypes">The runtime assemby types.</param>
        /// <param name="interpretedMode">if set to <c>true</c> [interpreted mode].</param>
        /// <param name="debugMode">if set to <c>true</c> [debug mode].</param>
        /// <returns></returns>
        ScriptRuntime CreateIronRubyRuntime(IEnumerable<Type> runtimeAssembyTypes, bool interpretedMode, bool debugMode);
    }

    /// <summary>
    /// 脚本工厂
    /// </summary>
    public class ScriptFactory : IScriptFactory
    {
        private ScriptRuntime _runtime;

        /// <summary>
        /// 获取脚本运行环境, 默认的没有加载自定义的程序集
        /// </summary>
        public ScriptRuntime Runtime
        {
            get
            {
                if (_runtime == null)
                {
                    _runtime = CreateIronRubyRuntime();
                }
                return _runtime;
            }
        }

        /// <summary>
        /// 创建默认的脚本运行环境
        /// </summary>
        /// <returns></returns>
        public ScriptRuntime CreateIronRubyRuntime()
        {
            return Ruby.CreateRuntime();
        }

        /// <summary>
        /// 创建脚本运行环境
        /// </summary>
        /// <param name="runtimeAssembyTypes">The runtime assemby types.</param>
        /// <param name="interpretedMode">if set to <c>true</c> [interpreted mode].</param>
        /// <param name="debugMode">if set to <c>true</c> [debug mode].</param>
        /// <returns></returns>
        public ScriptRuntime CreateIronRubyRuntime(IEnumerable<Type> runtimeAssembyTypes, bool interpretedMode, bool debugMode)
        {
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