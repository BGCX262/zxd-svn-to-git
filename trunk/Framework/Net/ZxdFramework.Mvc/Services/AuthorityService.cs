using System;
using System.Web;
using System.Web.Security;
using Spring.Transaction.Interceptor;
using ZxdFramework.Authorization;
using ZxdFramework.Authorization.Dao;
using ZxdFramework.Authorization.Services;
using ZxdFramework.DataContract;
using ZxdFramework.Model;

namespace ZxdFramework.Mvc.Services
{
    /// <summary>
    /// 实现用户操作类
    /// </summary>
    internal class AuthorityService : IAuthorityService
    {
        private const string UserSessionKey = "_usersessionkey";

        /// <summary>
        /// 游客账号
        /// </summary>
        public const string Guest = "Guest";
        /// <summary>
        /// 超级管理员
        /// </summary>
        public const string Administrator = "Administrator";


        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorityService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="roleReponsitory">The role reponsitory.</param>
        public AuthorityService(IAccountRepository repository, IRoleReponsitory roleReponsitory)
        {
            AccountRepository = repository;
            RoleReponsitory = roleReponsitory;
        }

        #region IAuthorityService Members

        /// <summary>
        /// 获取角色操作层
        /// </summary>
        /// <value></value>
        public IRoleReponsitory RoleReponsitory { protected set; get; }

        /// <summary>
        /// 获取账户访问层
        /// </summary>
        /// <value></value>
        public IAccountRepository AccountRepository { protected set; get; }

        /// <summary>
        /// 获取当前的用户
        /// </summary>
        /// <value></value>
        public IUser CurrentUser
        {
            get
            {
                var user = HttpContext.Current.Session[UserSessionKey] as IUser;
                if (user == null) user = CreateGuest();
                return user;
            }
            set { HttpContext.Current.Session[UserSessionKey] = value; }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="remember">if set to <c>true</c> [remember].</param>
        /// <returns></returns>
        public LogonResult Logon(string username, string password, bool remember)
        {
            var user = AccountRepository.GetUser(username);
            if (user == null) return LogonResult.NotUserName;
            if (user.Password != DESEncrypt.Instance.Encrypt(password)) return LogonResult.PasswordFail;
            if (user.State != UserStates.Active) return LogonResult.UserLocked;

            HttpContext.Current.Session[UserSessionKey] = user;
            FormsAuthentication.SetAuthCookie(username, remember);

            user.LoginTime = DateTime.Now;
            
            //user.TokenId = HttpContext.Current.Session.SessionID;
            user.TokenId = Guid.NewGuid().ToString("N");
            user.Count++;
            user.LastIp = HttpContext.Current.Request.UserHostAddress;
            
            AccountRepository.UpdateUser(user);

            return LogonResult.Success;
        }

        /// <summary>
        /// 用户推出
        /// </summary>
        /// <returns></returns>
        public bool Logout()
        {
            HttpContext.Current.Session.Clear();
            FormsAuthentication.SignOut();
            return true;
        }

        /// <summary>
        /// 获取一个游客对象
        /// </summary>
        /// <returns></returns>
        public IUser CreateGuest()
        {
            var user = AccountRepository.GetUser(Guest);
            if (user == null)
            {
                user = new User
                           {
                               Name = "游客",
                               LoginName = "Guest",
                               RoleNames = new string[] { GetGuestRole().Name },
                               LoginTime = DateTime.Now
                           };
                AccountRepository.CreateUser(user);
            }

            return user;
        }

        /// <summary>
        /// Gets the guest role.
        /// </summary>
        /// <returns></returns>
        public IRole GetGuestRole()
        {
            return new Role
                       {
                           Name = "公共用户"
                       };
        }


        /// <summary>
        /// 获取根节点的角色
        /// </summary>
        /// <returns></returns>
        public IRole GetRootRole()
        {
            return RoleReponsitory.GetRoot();
        }

        public ISecurityAction GetRootSecurityAction()
        {
            return new SecurityAction("")
                       {
                           Name = "超级权限"
                       };
        }

        /// <summary>
        /// 获取用户的头像
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public BaseUserPhoto GetUserPhoto(Guid id)
        {
            var ph = AccountRepository.GetUserPhoto(id);
            if (ph == null)
            {
                ph = AccountRepository.GetUserPhoto(Guest);
            }
            return ph;
        }

        #endregion
    }
}