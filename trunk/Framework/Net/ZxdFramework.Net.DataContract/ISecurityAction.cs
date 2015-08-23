

using System.Collections.Generic;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// ���尲ȫ�ڵ�
    /// </summary>
    public interface ISecurityAction
    {
        /// <summary>
        /// �ڵ�����
        /// </summary>
        string Key { get; }

        /// <summary>
        /// �ڵ�����
        /// </summary>
        string Name { get; }

        /// <summary>
        /// ֻ������, ��ȡ�����������е�����
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { set; get; }


        /// <summary>
        /// ����ͼ��
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        string Icon { set; get; }

        /// <summary>
        /// �ӽڵ�
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        ISet<ISecurityAction> Children { set; get; }

        /// <summary>
        /// ���ڵ�
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        ISecurityAction Parent { set; get; }


        /// <summary>
        /// ��鵱ǰ�����ӽڵ��Ƿ������������
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        bool Contain(string key);


        /// <summary>
        /// ��ȡ��ǰ�ڵ��µİ�ȫ�ڵ�
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        ISecurityAction GetSecurityAction(string key);

        /// <summary>
        /// ����һ���ӽڵ�
        /// </summary>
        /// <param name="action">The action.</param>
        void AddChild(ISecurityAction action);
    }
}