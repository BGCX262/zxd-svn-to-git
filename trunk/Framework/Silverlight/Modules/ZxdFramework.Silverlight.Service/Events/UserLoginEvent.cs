using Microsoft.Practices.Prism.Events;
using ZxdFramework.DataContract;

namespace ZxdFramework.Service.Events
{
    /// <summary>
    /// �û������½�¼�
    /// </summary>
    public class UserLoginEvent : CompositePresentationEvent<UserLoginEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserLoginEvent"/> class.
        /// </summary>
        public UserLoginEvent()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserLoginEvent"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        public UserLoginEvent(object control)
        {
            ParentControl = control;
        }

        /// <summary>
        /// ��ȡ��ʾ��¼���ڵĸ��ڵ�
        /// </summary>
        /// <value>
        /// The parent control.
        /// </value>
        public object ParentControl { protected set; get; }


        /// <summary>
        /// ���û�ȡ�����½���û���
        /// </summary>
        /// <value>
        /// The name of the login.
        /// </value>
        public string LoginName { set; get; }

        /// <summary>
        /// ���û�ȡ��ʼ��½���û�����
        /// </summary>
        /// <value>
        /// The login password.
        /// </value>
        public string LoginPassword { set; get; }


        /// <summary>
        /// ���û�ȡ��¼�û�����. ���������ڷ�������֤�û��ɹ���. �ٴ������ȡ�ĵ�ǰ��¼���û�����. 
        /// ��������û���¼ʧ���������Ϊ��. ����Ϊ����ǰ���õĶ���
        /// </summary>
        /// <value>
        /// The login user.
        /// </value>
        public IUser LoginUser { set; get; }

        /// <summary>
        /// ִ�лص�����. 
        /// </summary>
        /// <value>
        /// The callback.
        /// </value>
        public ServiceCompleted<int> Callback { set; get; }
    }
}