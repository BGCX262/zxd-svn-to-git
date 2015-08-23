

using System.Collections.Generic;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// 定义安全节点
    /// </summary>
    public interface ISecurityAction
    {
        /// <summary>
        /// 节点主键
        /// </summary>
        string Key { get; }

        /// <summary>
        /// 节点名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 只读属性, 获取所在整个树中的名称
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// 描述信息
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { set; get; }


        /// <summary>
        /// 定义图标
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        string Icon { set; get; }

        /// <summary>
        /// 子节点
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        ISet<ISecurityAction> Children { set; get; }

        /// <summary>
        /// 父节点
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        ISecurityAction Parent { set; get; }


        /// <summary>
        /// 检查当前或者子节点是否具有整个主键
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        bool Contain(string key);


        /// <summary>
        /// 获取当前节点下的安全节点
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        ISecurityAction GetSecurityAction(string key);

        /// <summary>
        /// 增加一个子节点
        /// </summary>
        /// <param name="action">The action.</param>
        void AddChild(ISecurityAction action);
    }
}