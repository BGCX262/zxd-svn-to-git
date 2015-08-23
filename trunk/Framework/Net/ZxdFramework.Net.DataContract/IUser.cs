using System;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// 定义系统中用户的基本信息
    /// </summary>
    public interface IUser : IModel
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        Guid Id { get; }

        /// <summary>
        /// 获取用户姓名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 获取用户的名称
        /// </summary>
        /// <value>
        /// The name of the login.
        /// </value>
        string LoginName { get; }


        /// <summary>
        /// 登录密码
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Gets the login time.
        /// </summary>
        DateTime? LoginTime { get; }

        /// <summary>
        /// 设置获取用户的状态<br/>
        /// 当前的状态值可用于系统中对用户的控制.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        UserStates State { set; get; }

        /// <summary>
        /// 当前用户的角色名称
        /// </summary>
        string[] RoleNames { set; get; }


        /// <summary>
        /// 设置获取用户的标记 ID, 可用于用户是否重复登录的标记. 每次
        /// </summary>
        /// <value>
        /// The token id.
        /// </value>
        string TokenId { set; get; }


        /// <summary>
        /// 设置获取登录的次数
        /// </summary>
        /// <value>The count.</value>
        int Count { set; get; }

        /// <summary>
        /// 设置获取最后一次登录的IP
        /// </summary>
        /// <value>The last ip.</value>
        string LastIp { set; get; }

        /// <summary>
        /// 设置获取用户类型
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        Category Category { set; get; }
    }


    /// <summary>
    /// 定义用户当前的状态
    /// </summary>
    public enum UserStates
    {
        /// <summary>
        /// 激活的
        /// </summary>
        Active,
        /// <summary>
        /// 锁定
        /// </summary>
        Locked,
        /// <summary>
        /// 有问题
        /// </summary>
        Problem
    }

}