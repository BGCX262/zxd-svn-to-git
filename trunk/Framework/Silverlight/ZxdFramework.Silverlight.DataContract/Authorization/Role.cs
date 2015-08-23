using System;
using System.Collections.Generic;
using Microsoft.Scripting.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZxdFramework.Net.Script;

namespace ZxdFramework.DataContract.Authorization
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : ViewModel, IRole, IScriptRunable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role()
        {
            IsComplied = false;
        }


        //public string Name { set; get; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    Notify(() => this.Name);
                }
            }
        }



        //public string Description { set; get; }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    Notify(() => this.Description);
                }
            }
        }



        private ICollection<IRole> _children;
        
        public ICollection<IRole> Children
        {
            get { return _children; }
            set
            {
                if (value == _children) return;
                if (value != null)
                {
                    foreach (var role in value)
                    {
                        role.Parent = this;
                    }
                }
                _children = value;
                Notify(() => Children);
            }
        }

        //public ICollection<string> ActionKeys { set; get; }
        private ICollection<string> _actionKeys;
        public ICollection<string> ActionKeys
        {
            get { return _actionKeys; }
            set
            {
                if (_actionKeys != value)
                {
                    _actionKeys = value;
                    Notify(() => this.ActionKeys);
                }
            }
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
                if(value != _authorityScript)
                {
                    IsComplied = false;
                    _authorityScript = value;
                    Notify(() => AuthorityScript);
                }
                
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

        [JsonIgnore]
        public IRole Parent { set; get; }

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
            return result is bool ? (bool)result : false;
        }

        public bool Contain(ISecurityAction action, ScriptEngine engine, ScriptScope scope)
        {
            return Contain(action.Key, engine, scope);
        }

        public bool Contain(string key, ScriptEngine engine, ScriptScope scope)
        {
            var result = Contain(key);
            return (bool)ScriptRun(engine, scope) ? true : result;
        }


        public bool Contain(string key)
        {
            var result = false;
            if (ActionKeys != null)
            {
                result = ActionKeys.Contains(key);
                if (!result)
                {
                    foreach (var role in Children)
                    {
                        result = role.Contain(key);
                        if (result) break;
                    }

                }
            }
            return result;
        }
    }


    /// <summary>
    /// 转换系统中需要使用的 Role 角色对象
    /// </summary>
    public class JsonRoleConverter : CustomCreationConverter<IRole>
    {
        public override IRole Create(Type objectType)
        {
            return new Role();
        }
    }
}