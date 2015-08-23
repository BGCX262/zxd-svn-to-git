using System;
using System.Collections.Generic;
using Microsoft.Scripting.Hosting;
using Newtonsoft.Json;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// �û���ɫģ��
    /// </summary>
    public abstract class BaseRole : Entity<Guid>, IRole
    {

        /// <summary>
        /// ��ȡ��ɫ����, ���������ϵͳ��Ψһ��
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// ���û�ȡ������Ϣ
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
        /// ��鵱ǰ�Ľ�ɫ�Ƿ���а����İ�ȫ�ڵ�
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