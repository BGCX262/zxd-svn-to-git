using Microsoft.Practices.Prism.Events;

namespace ZxdFramework.DataContract.Events
{
    /// <summary>
    /// 账号切换事件
    /// </summary>
    public class AccountChangedEvent : CompositePresentationEvent<AccountChangedEvent>
    {

        /// <summary>
        /// 当前登录用户
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public IUser CurrentUser { set; get; }

    }
}