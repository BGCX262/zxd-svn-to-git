using Microsoft.Practices.Prism.Events;

namespace ZxdFramework.Shell.UI.Events
{
    /// <summary>
    /// ��¼ҳ��״̬ת���¼�
    /// </summary>
    public class LoginViewStateChangeEvent : CompositePresentationEvent<LoginViewStateChangeEvent>
    {
        /// <summary>
        /// �Ƿ���ʾ��ǰ����ʾҳ��
        /// </summary>
        /// <value><c>true</c> if this instance is show; otherwise, <c>false</c>.</value>
        public bool IsShow { set; get; }
    }
}