using System;

namespace ZxdFramework.DataContract.UI
{
    /// <summary>
    /// 提供在窗口视图需要长用到的方法, 一般给视图接口继承
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        /// 当窗体关闭触发的事件
        /// </summary>
        /// <value>The closed.</value>
        event EventHandler Closed;

        /// <summary>
        /// 显示窗口
        /// </summary>
        void Show();

        /// <summary>
        /// 关闭窗口
        /// </summary>
        void Close();

    }
}