using ZxdFramework.DataContract;
using ZxdFramework.Events;

namespace ZxdFramework.Authorization
{
    /// <summary>
    /// 权限更改事件
    /// </summary>
    public class RoleChangedEvent : PresentationEvent<RoleChangedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleChangedEvent"/> class.
        /// </summary>
        public RoleChangedEvent()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleChangedEvent"/> class.
        /// </summary>
        /// <param name="role">The role.</param>
        public RoleChangedEvent(IRole role)
        {
            Target = role;
        }

        /// <summary>
        /// 获取变化的角色
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public IRole Target { protected set; get; }


        public IRole RootRole { set; get; }
    }
}