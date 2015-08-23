using ZxdFramework.DataContract;
using ZxdFramework.Events;

namespace ZxdFramework.Authorization
{
    /// <summary>
    /// Ȩ�޸����¼�
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
        /// ��ȡ�仯�Ľ�ɫ
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public IRole Target { protected set; get; }


        public IRole RootRole { set; get; }
    }
}