using System.Collections.Generic;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// 抽象的树形结构的类型, 提供了绝大部分的树形结构的继承
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
        /// 获取当前节点在整个树形结构中的名称. 并用 "." 作为分割
        /// </summary>
        /// <returns></returns>
        public string GetFullName()
        {
            if (Parent == null) return Name;
            return Parent.Name + "." + Name;
        }

        /// <summary>
        /// 获取当前节点在整个树形结构中的Id. 并用 "." 作为分割
        /// </summary>
        /// <returns></returns>
        public string GetFullId()
        {
            if (Parent == null) return Id;
            return Parent.Id + "." + Id;
        }
    }
}