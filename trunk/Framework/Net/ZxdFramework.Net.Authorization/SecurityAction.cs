using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ZxdFramework.DataContract;

namespace ZxdFramework.Authorization
{
    /// <summary>
    /// 安全节点
    /// </summary>
    public class SecurityAction : ISecurityAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityAction"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public SecurityAction(string key)
        {
            Key = key;
        }

        /// <summary>
        /// 节点的键. 必须当前的键是这个层次中的唯一值
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { private set; get; }

        /// <summary>
        /// 节点名称
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { set; get; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { set; get; }


        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string Icon { set; get; }

        /// <summary>
        /// 只读属性, 获取所在整个树中的名称
        /// </summary>
        [JsonIgnore]
        public string FullName
        {
            get
            {
                if (Parent == null) return Name;
                return Parent.FullName + "." + Name;
            }
        }


        private ISet<ISecurityAction> _children;
        /// <summary>
        /// 设置获取子安全节点. 子节点可作为细化安全的节点. 父节点包含子节点的全部权限
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public ISet<ISecurityAction> Children 
        {
            get { return _children; }
            set
            {
                if (value != null)
                {
                    foreach (var action in value)
                    {
                        action.Parent = this;
                    }
                }
                _children = value;
            }
        }


        /// <summary>
        /// 父节点
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        [JsonIgnore]
        public ISecurityAction Parent { set; get; }

        /// <summary>
        /// 检查当前或者子节点是否具有整个主键
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Contain(string key)
        {
            return GetSecurityAction(key) != null;
        }

        /// <summary>
        /// 获取当前节点下的安全节点
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public ISecurityAction GetSecurityAction(string key)
        {
            return Key == key ? this : Children.FirstOrDefault(child => child.Contain(key));
        }

        public void AddChild(ISecurityAction action)
        {
            if (Children == null) Children = new HashSet<ISecurityAction>();

            if (!Children.Contains(action))
            {
                action.Parent = this;
                Children.Add(action);
            }
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode() * 23;
        }

        public override bool Equals(object obj)
        {
            var temp = obj as SecurityAction;

            return temp != null && string.Equals(Key, temp.Key);
        }
    }
}