using System;
using System.Collections.Generic;
using IronRuby;
using Microsoft.Scripting.Hosting;
using ZxdFramework.DataContract;

namespace ZxdFramework.Script
{
    /// <summary>
    /// ����ű��Ĺ���
    /// </summary>
    public interface IScriptFactory
    {
        /// <summary>
        /// ��ȡ�ű����л���
        /// </summary>
        ScriptRuntime Runtime { get; }

        /// <summary>
        /// ����Ĭ�ϵĽű����л���
        /// </summary>
        /// <returns></returns>
        ScriptRuntime CreateIronRubyRuntime();

        /// <summary>
        /// �����ű����л���
        /// </summary>
        /// <param name="runtimeAssembyTypes">The runtime assemby types.</param>
        /// <param name="interpretedMode">if set to <c>true</c> [interpreted mode].</param>
        /// <param name="debugMode">if set to <c>true</c> [debug mode].</param>
        /// <returns></returns>
        ScriptRuntime CreateIronRubyRuntime(IEnumerable<Type> runtimeAssembyTypes, bool interpretedMode, bool debugMode);
    }

    /// <summary>
    /// �ű�����
    /// </summary>
    public class ScriptFactory : IScriptFactory
    {
        private ScriptRuntime _runtime;

        /// <summary>
        /// ��ȡ�ű����л���, Ĭ�ϵ�û�м����Զ���ĳ���
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
        /// ����Ĭ�ϵĽű����л���
        /// </summary>
        /// <returns></returns>
        public ScriptRuntime CreateIronRubyRuntime()
        {
            return Ruby.CreateRuntime();
        }

        /// <summary>
        /// �����ű����л���
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