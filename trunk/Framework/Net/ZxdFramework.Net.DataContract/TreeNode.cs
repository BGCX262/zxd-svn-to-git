using System.Collections.Generic;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// ��������νṹ������, �ṩ�˾��󲿷ֵ����νṹ�ļ̳�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TreeNode<T> : Entity<string> 
        where T : TreeNode<T>
    {
        /// <summary>
        /// Gets or sets the name.
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
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public T Parent { set; get; }


        private ICollection<T> _children;
        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public ICollection<T> Children 
        {
            get { return _children; }

            set
            {
                if (value != null)
                {
                    foreach (var child in value)
                    {
                        child.Parent = (T) this;
                    }
                }
                _children = value;
            }
        }


        /// <summary>
        /// ��ȡ��ǰ�ڵ����������νṹ�е�����. ���� "." ��Ϊ�ָ�
        /// </summary>
        /// <returns></returns>
        public string GetFullName()
        {
            if (Parent == null) return Name;
            return Parent.Name + "." + Name;
        }

        /// <summary>
        /// ��ȡ��ǰ�ڵ����������νṹ�е�Id. ���� "." ��Ϊ�ָ�
        /// </summary>
        /// <returns></returns>
        public string GetFullId()
        {
            if (Parent == null) return Id;
            return Parent.Id + "." + Id;
        }
    }
}