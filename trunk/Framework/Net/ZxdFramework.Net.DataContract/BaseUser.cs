using System;
using Newtonsoft.Json;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// 基本的用户对象
    /// </summary>
    public abstract class BaseUser : Entity<Guid>, IUser
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// Gets the name of the login.
        /// </summary>
        /// <value>
        /// The name of the login.
        /// </value>
        public virtual string LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [JsonIgnore]
        public virtual string Password { set; get; }

        /// <summary>
        /// Gets the login time.
        /// </summary>
        public virtual DateTime? LoginTime { get; set; }
        /// <summary>
        /// 设置获取用户的状态<br/>
        /// 当前的状态值可用于系统中对用户的控制.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public virtual UserStates State { get; set; }

        /// <summary>
        /// 当前用户的角色名称
        /// </summary>
        public virtual string[] RoleNames { set; get; }

        /// <summary>
        /// 设置获取用户的标记 ID, 可用于用户是否重复登录的标记. 每次
        /// </summary>
        /// <value>
        /// The token id.
        /// </value>
        public virtual string TokenId { set; get; }

        /// <summary>
        /// 设置获取登录的次数
        /// </summary>
        /// <value>The count.</value>
        public virtual int Count { set; get; }

        /// <summary>
        /// 设置获取最后一次登录的IP
        /// </summary>
        /// <value>The last ip.</value>
        public virtual string LastIp { set; get; }

        /// <summary>
        /// 设置获取用户类型
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public virtual Category Category { set; get; }


        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var temp = obj as BaseUser;

            return temp != null && temp.Id == Id;

        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode() + 344;
        }

    }
}