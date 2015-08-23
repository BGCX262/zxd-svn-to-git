using Microsoft.Practices.Prism.Events;

namespace ZxdFramework.Shell.UI.Events
{
    /// <summary>
    /// 登录页面状态转换事件
    /// </summary>
    public class LoginViewStateChangeEvent : CompositePresentationEvent<LoginViewStateChangeEvent>
    {
        /// <summary>
        /// 是否显示当前的显示页面
        /// </summary>
        /// <value><c>true</c> if this instance is show; otherwise, <c>false</c>.</value>
        public bool IsShow { set; get; }
    }
}