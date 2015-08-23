using System;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// ����ϵͳ���û��Ļ�����Ϣ
    /// </summary>
    public interface IUser : IModel
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        Guid Id { get; }

        /// <summary>
        /// ��ȡ�û�����
        /// </summary>
        string Name { get; }

        /// <summary>
        /// ��ȡ�û�������
        /// </summary>
        /// <value>
        /// The name of the login.
        /// </value>
        string LoginName { get; }


        /// <summary>
        /// ��¼����
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Gets the login time.
        /// </summary>
        DateTime? LoginTime { get; }

        /// <summary>
        /// ���û�ȡ�û���״̬<br/>
        /// ��ǰ��״ֵ̬������ϵͳ�ж��û��Ŀ���.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        UserStates State { set; get; }

        /// <summary>
        /// ��ǰ�û��Ľ�ɫ����
        /// </summary>
        string[] RoleNames { set; get; }


        /// <summary>
        /// ���û�ȡ�û��ı�� ID, �������û��Ƿ��ظ���¼�ı��. ÿ��
        /// </summary>
        /// <value>
        /// The token id.
        /// </value>
        string TokenId { set; get; }


        /// <summary>
        /// ���û�ȡ��¼�Ĵ���
        /// </summary>
        /// <value>The count.</value>
        int Count { set; get; }

        /// <summary>
        /// ���û�ȡ���һ�ε�¼��IP
        /// </summary>
        /// <value>The last ip.</value>
        string LastIp { set; get; }

        /// <summary>
        /// ���û�ȡ�û�����
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        Category Category { set; get; }
    }


    /// <summary>
    /// �����û���ǰ��״̬
    /// </summary>
    public enum UserStates
    {
        /// <summary>
        /// �����
        /// </summary>
        Active,
        /// <summary>
        /// ����
        /// </summary>
        Locked,
        /// <summary>
        /// ������
        /// </summary>
        Problem
    }

}