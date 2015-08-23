using System.Collections;
using System.Collections.Generic;
using Microsoft.Scripting.Hosting;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// 定义角色
    /// </summary>
    public interface IRole : IModel
    {
        /// <summary>
        /// 获取角色名称, 这个名称是系统中唯一的
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 设置获取描述信息
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { set; get; }


        /// <summary>
        /// 获取设置角色子节点
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        ICollection<IRole> Children { set; get; }

        /// <summary>
        /// 当前角色下包含的节点
        /// </summary>
        /// <value>
        /// The actions.
        /// </value>
        ICollection<string> ActionKeys { set; get; }

        /// <summary>
        /// 获取角色的父节点
        /// </summary>
        IRole Parent { set; get; }

        /// <summary>
        /// 检查当前的角色是否具有包含的安全节点
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="scope">The scope.</param>
        /// <returns></returns>
        bool Contain(ISecurityAction action, ScriptEngine engine, ScriptScope scope);

        /// <summary>
        /// Contains the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="scope">The scope.</param>
        /// <returns></returns>
        bool Contain(string key, ScriptEngine engine, ScriptScope scope);

        /// <summary>
        /// 检查角色是否包含安全节点, 不会对角色的脚本进行检查
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        bool Contain(string key);
    }
}