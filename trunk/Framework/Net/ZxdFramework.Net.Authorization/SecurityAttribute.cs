using System;

namespace ZxdFramework.Authorization
{
    /// <summary>
    /// 权限标签, 设置在方法之上. 配置当前的方法执行安全检查
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class SecurityAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityAttribute"/> class.
        /// </summary>
        public SecurityAttribute()
        {
            CheckScript = true;
        }


        /// <summary>
        /// 设置获取是否执行角色中的脚本检查, 默认True
        /// </summary>
        /// <value>
        ///   <c>true</c> if [check script]; otherwise, <c>false</c>.
        /// </value>
        public bool CheckScript { set; get; }

        /// <summary>
        /// 设置获取当前方法所属的角色, 角色的检查优先于节点的检查
        /// </summary>
        /// <value>
        /// The role names.
        /// </value>
        public string[] RoleNames { set; get; }


        /// <summary>
        /// 设置获取用户的角色或者操作中包含当前的操作节点
        /// </summary>
        /// <value>
        /// The action key.
        /// </value>
        public string ActionKey { set; get; }

        /// <summary>
        /// 设置获取如果验证失败后的显示信息
        /// </summary>
        /// <value>
        /// The fail message.
        /// </value>
        public string FailMessage { set; get; }
    }
}