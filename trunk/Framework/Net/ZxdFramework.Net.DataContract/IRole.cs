using System.Collections;
using System.Collections.Generic;
using Microsoft.Scripting.Hosting;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// �����ɫ
    /// </summary>
    public interface IRole : IModel
    {
        /// <summary>
        /// ��ȡ��ɫ����, ���������ϵͳ��Ψһ��
        /// </summary>
        string Name { get; }

        /// <summary>
        /// ���û�ȡ������Ϣ
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { set; get; }


        /// <summary>
        /// ��ȡ���ý�ɫ�ӽڵ�
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        ICollection<IRole> Children { set; get; }

        /// <summary>
        /// ��ǰ��ɫ�°����Ľڵ�
        /// </summary>
        /// <value>
        /// The actions.
        /// </value>
        ICollection<string> ActionKeys { set; get; }

        /// <summary>
        /// ��ȡ��ɫ�ĸ��ڵ�
        /// </summary>
        IRole Parent { set; get; }

        /// <summary>
        /// ��鵱ǰ�Ľ�ɫ�Ƿ���а����İ�ȫ�ڵ�
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
        /// ����ɫ�Ƿ������ȫ�ڵ�, ����Խ�ɫ�Ľű����м��
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        bool Contain(string key);
    }
}