using System;
using ZxdFramework.Authorization.Dao;
using ZxdFramework.DataContract;

namespace ZxdFramework.Authorization.Services
{
    /// <summary>
    /// ����Ȩ����֤����
    /// </summary>
    public interface IAuthorityService
    {

        /// <summary>
        /// ��ȡ��ɫ������
        /// </summary>
        IRoleReponsitory RoleReponsitory { get; }

        /// <summary>
        /// ��ȡ�˻����ʲ�
        /// </summary>
        IAccountRepository AccountRepository { get; }

        /// <summary>
        /// ��ȡ��ǰ���û�
        /// </summary>
        IUser CurrentUser { set; get; }

        /// <summary>
        /// �û���¼
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="remember">if set to <c>true</c> [remember].</param>
        /// <returns></returns>
        LogonResult Logon(string username, string password, bool remember);

        /// <summary>
        /// �û��Ƴ�
        /// </summary>
        /// <returns></returns>
        bool Logout();

        /// <summary>
        /// ��ȡһ���οͶ���
        /// </summary>
        /// <returns></returns>
        IUser CreateGuest();

        /// <summary>
        /// ��ȡ���ڵ�Ľ�ɫ
        /// </summary>
        /// <returns></returns>
        IRole GetRootRole();

        /// <summary>
        /// ��ȡ����İ�ȫ�ڵ�ĸ��ڵ�
        /// </summary>
        /// <returns></returns>
        ISecurityAction GetRootSecurityAction();


        /// <summary>
        /// ��ȡ�û���ͷ��
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        BaseUserPhoto GetUserPhoto(Guid id);
    }


    /// <summary>
    /// ��¼�Ľ��ö��
    /// </summary>
    public enum LogonResult
    {

        /// <summary>
        /// ��¼�ɹ�
        /// </summary>
        Success = 1,
        /// <summary>
        /// û�е�ǰ���û�
        /// </summary>
        NotUserName = 2,

        /// <summary>
        /// �������
        /// </summary>
        PasswordFail = 3,
        /// <summary>
        /// �û�����
        /// </summary>
        UserLocked = 4
    }


}