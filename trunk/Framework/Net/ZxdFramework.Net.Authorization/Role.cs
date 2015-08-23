using System;
using Microsoft.Scripting.Hosting;
using ZxdFramework.DataContract;
using ZxdFramework.Net.Script;

namespace ZxdFramework.Authorization
{
    /// <summary>
    /// 用户角色, 并支持权限检查前的脚本运算
    /// </summary>
    public class Role : BaseRole, IScriptRunable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role()
        {
            IsComplied = false;
        }

        private string _authorityScript;
        /// <summary>
        /// 设置获取权限检查脚本
        /// </summary>
        /// <value>
        /// The security script.
        /// </value>
        public string AuthorityScript
        {
            get { return _authorityScript; }
            set
            {
                if (value != AuthorityScript) IsComplied = false;
                _authorityScript = value;
            }
        }



        /// <summary>
        /// 更新时间
        /// </summary>
        /// <value>
        /// The last update.
        /// </value>
        public DateTime LastUpdate { set; get; }


        protected bool IsComplied { set; get; }
        protected CompiledCode CompiledCode { set; get; }
        /// <summary>
        /// 执行权限检查前的脚本
        /// </summary>
        /// <param name="engine">脚本运行引擎</param>
        /// <param name="scope">运行环境</param>
        /// <returns>
        /// 返回脚执行的结果
        /// </returns>
        public object ScriptRun(ScriptEngine engine, ScriptScope scope)
        {
            if (string.IsNullOrWhiteSpace(AuthorityScript)) return false;

            if (!IsComplied)
            {
                CompiledCode = engine.CreateScriptSourceFromString(AuthorityScript).Compile();
                IsComplied = true;
            }
            
            
            var result = CompiledCode.Execute(scope);
            return result is bool ? (bool) result : false;
        }

        public override bool Contain(ISecurityAction action, ScriptEngine engine,  ScriptScope scope)
        {
            return Contain(action.Key, engine, scope);
        }

        public override bool Contain(string key, ScriptEngine engine, ScriptScope scope)
        {
            var result = base.Contain(key);
           return (bool)ScriptRun(engine, scope) ? true : result;
        }


        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode()*12;
        }

        public override bool Equals(object obj)
        {
            var temp = obj as IRole;
            if (temp == null) return false;
            return string.Equals(temp.Name, Name);

        }
    }
}