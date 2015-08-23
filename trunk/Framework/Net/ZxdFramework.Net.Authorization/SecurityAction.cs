using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ZxdFramework.DataContract;

namespace ZxdFramework.Authorization
{
    /// <summary>
    /// ��ȫ�ڵ�
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
        /// �ڵ�ļ�. ���뵱ǰ�ļ����������е�Ψһֵ
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { private set; get; }

        /// <summary>
        /// �ڵ�����
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
        /// ֻ������, ��ȡ�����������е�����
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
        /// ���û�ȡ�Ӱ�ȫ�ڵ�. �ӽڵ����Ϊϸ����ȫ�Ľڵ�. ���ڵ�����ӽڵ��ȫ��Ȩ��
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
        /// ���ڵ�
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        [JsonIgnore]
        public ISecurityAction Parent { set; get; }

        /// <summary>
        /// ��鵱ǰ�����ӽڵ��Ƿ������������
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Contain(string key)
        {
            return GetSecurityAction(key) != null;
        }

        /// <summary>
        /// ��ȡ��ǰ�ڵ��µİ�ȫ�ڵ�
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