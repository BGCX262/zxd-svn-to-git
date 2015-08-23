using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace ZxdFramework.Shell.UI.Views
{

    /// <summary>
    /// 主要的显示视图
    /// </summary>
    public interface IShellView
    {
        IShellViewModel Model { set; get; }

        /// <summary>
        /// 设置获取是否显示登录窗口
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is show login; otherwise, <c>false</c>.
        /// </value>
        bool IsShowLogin { set; get; }
    }

    [Export(typeof(IShellView))]
	public partial class ShellView : UserControl, IShellView
	{
		public ShellView()
		{
			// 为初始化变量所必需
			InitializeComponent();
		    IsShowLogin = true;
		}

        public IShellViewModel Model
	    {
            get { return DataContext as IShellViewModel; }
            set { DataContext = value; }
	    }

        private bool _isShowLogin = true;
        public bool IsShowLogin
        {
            get { return _isShowLogin; }
            set { 
                if(value != _isShowLogin) VisualStateManager.GoToState(this, value ? "Login" : "View", true);
                _isShowLogin = value;
            }
        }
	}
}