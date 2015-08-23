using System;
using System.Collections.Generic;
using Microsoft.Scripting.Hosting;
using Newtonsoft.Json;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// 用户角色模型
    /// </summary>
    public abstract class BaseRole : Entity<Guid>, IRole
    {

        /// <summary>
        /// 获取角色名称, 这个名称是系统中唯一的
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 设置获取描述信息
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { set; get; }

        private ICollection<IRole> _children;
        public ICollection<IRole> Children
        {
            get { return _children; }
            set
            {
                if (value != null)
                {
                    foreach (var role in value)
                    {
                        role.Parent = this;
                    }
                }
                _children = value;
            }
        }

        public ICollection<string> ActionKeys { set; get; }

        [JsonIgnore]
        public IRole Parent { set; get; }

        /// <summary>
        /// 检查当前的角色是否具有包含的安全节点
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="scope">The scope.</param>
        /// <returns></returns>
        public abstract bool Contain(ISecurityAction action, ScriptEngine engine, ScriptScope scope);
        /// <summary>
        /// Contains the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="scope">The scope.</param>
        /// <returns></returns>
        public abstract bool Contain(string key, ScriptEngine engine, ScriptScope scope);


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
}