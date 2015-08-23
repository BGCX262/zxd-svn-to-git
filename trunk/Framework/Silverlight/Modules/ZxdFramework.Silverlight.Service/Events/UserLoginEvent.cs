using Microsoft.Practices.Prism.Events;
using ZxdFramework.DataContract;

namespace ZxdFramework.Service.Events
{
    /// <summary>
    /// 用户请求登陆事件
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
        /// 获取显示登录窗口的父节点
        /// </summary>
        /// <value>
        /// The parent control.
        /// </value>
        public object ParentControl { protected set; get; }


        /// <summary>
        /// 设置获取初设登陆的用户名
        /// </summary>
        /// <value>
        /// The name of the login.
        /// </value>
        public string LoginName { set; get; }

        /// <summary>
        /// 设置获取初始登陆的用户密码
        /// </summary>
        /// <value>
        /// The login password.
        /// </value>
        public string LoginPassword { set; get; }


        /// <summary>
        /// 设置获取登录用户对象. 这个对象会在服务器验证用户成功后. 再次请求获取的当前登录的用户对象. 
        /// 所以如果用户登录失败这个对象为空. 或者为请求前设置的对象
        /// </summary>
        /// <value>
        /// The login user.
        /// </value>
        public IUser LoginUser { set; get; }

        /// <summary>
        /// 执行回调函数. 
        /// </summary>
        /// <value>
        /// The callback.
        /// </value>
        public ServiceCompleted<int> Callback { set; get; }
    }
}