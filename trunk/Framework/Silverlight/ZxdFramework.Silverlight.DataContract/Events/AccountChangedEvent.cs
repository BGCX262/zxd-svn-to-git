using Microsoft.Practices.Prism.Events;

namespace ZxdFramework.DataContract.Events
{
    /// <summary>
    /// �˺��л��¼�
    /// </summary>
    public class AccountChangedEvent : CompositePresentationEvent<AccountChangedEvent>
    {

        /// <summary>
        /// ��ǰ��¼�û�
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public IUser CurrentUser { set; get; }

    }
}