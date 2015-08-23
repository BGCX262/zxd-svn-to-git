using System;
using ZxdFramework.Authorization.Dao;
using ZxdFramework.DataContract;

namespace ZxdFramework.Authorization.Services
{
    /// <summary>
    /// 定义权限验证服务
    /// </summary>
    public interface IAuthorityService
    {

        /// <summary>
        /// 获取角色操作层
        /// </summary>
        IRoleReponsitory RoleReponsitory { get; }

        /// <summary>
        /// 获取账户访问层
        /// </summary>
        IAccountRepository AccountRepository { get; }

        /// <summary>
        /// 获取当前的用户
        /// </summary>
        IUser CurrentUser { set; get; }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="remember">if set to <c>true</c> [remember].</param>
        /// <returns></returns>
        LogonResult Logon(string username, string password, bool remember);

        /// <summary>
        /// 用户推出
        /// </summary>
        /// <returns></returns>
        bool Logout();

        /// <summary>
        /// 获取一个游客对象
        /// </summary>
        /// <returns></returns>
        IUser CreateGuest();

        /// <summary>
        /// 获取根节点的角色
        /// </summary>
        /// <returns></returns>
        IRole GetRootRole();

        /// <summary>
        /// 获取定义的安全节点的根节点
        /// </summary>
        /// <returns></returns>
        ISecurityAction GetRootSecurityAction();


        /// <summary>
        /// 获取用户的头像
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        BaseUserPhoto GetUserPhoto(Guid id);
    }


    /// <summary>
    /// 登录的结果枚举
    /// </summary>
    public enum LogonResult
    {

        /// <summary>
        /// 登录成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 没有当前的用户
        /// </summary>
        NotUserName = 2,

        /// <summary>
        /// 密码错误
        /// </summary>
        PasswordFail = 3,
        /// <summary>
        /// 用户锁定
        /// </summary>
        UserLocked = 4
    }


}