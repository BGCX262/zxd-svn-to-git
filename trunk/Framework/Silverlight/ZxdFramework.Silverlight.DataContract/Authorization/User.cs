using System;

namespace ZxdFramework.DataContract.Authorization
{
    /// <summary>
    /// 支持绑定视图的用户对象模型
    /// </summary>
    public class User : ViewModel, IUser
    {
        private Category _category;
        private string _loginName;
        private string _name;
        private string _password;
        private string[] _roleNames;
        private UserStates _state;

        #region IUser Members

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public Guid Id { set; get; }

        /// <summary>
        /// 获取用户姓名
        /// </summary>
        /// <value></value>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    Notify(() => Name);
                }
            }
        }


        /// <summary>
        /// 获取用户的名称
        /// </summary>
        /// <value>The name of the login.</value>
        public string LoginName
        {
            get { return _loginName; }
            set
            {
                if (_loginName != value)
                {
                    _loginName = value;
                    Notify(() => LoginName);
                }
            }
        }

        /// <summary>
        /// 登录密码
        /// </summary>
        /// <value></value>
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    Notify(() => Password);
                }
            }
        }

        /// <summary>
        /// Gets the login time.
        /// </summary>
        /// <value></value>
        public DateTime? LoginTime { set; get; }

        /// <summary>
        /// 设置获取用户的状态<br/>
        /// 当前的状态值可用于系统中对用户的控制.
        /// </summary>
        /// <value>The state.</value>
        public UserStates State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    Notify(() => State);
                }
            }
        }

        /// <summary>
        /// 当前用户的角色名称
        /// </summary>
        /// <value></value>
        public string[] RoleNames
        {
            get { return _roleNames; }
            set
            {
                if (_roleNames != value)
                {
                    _roleNames = value;
                    Notify(() => RoleNames);
                }
            }
        }


        /// <summary>
        /// 设置获取用户的标记 ID, 可用于用户是否重复登录的标记. 每次
        /// </summary>
        /// <value>The token id.</value>
        public string TokenId { set; get; }

        /// <summary>
        /// 设置获取登录的次数
        /// </summary>
        /// <value>The count.</value>
        public int Count { set; get; }

        /// <summary>
        /// 设置获取最后一次登录的IP
        /// </summary>
        /// <value>The last ip.</value>
        public string LastIp { set; get; }

        /// <summary>
        /// 设置获取用户类型
        /// </summary>
        /// <value>The category.</value>
        public Category Category
        {
            get { return _category; }
            set
            {
                if (_category != value)
                {
                    _category = value;
                    Notify(() => Category);
                }
            }
        }

        #endregion
    }
}