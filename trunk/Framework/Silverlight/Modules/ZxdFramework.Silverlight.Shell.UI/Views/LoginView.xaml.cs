using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace ZxdFramework.Shell.UI.Views
{
	/// <summary>
	/// 登录视图接口
	/// </summary>
	public interface ILoginView
	{
        /// <summary>
        /// Gets or sets the 视图模型.
        /// </summary>
        /// <value>The model.</value>
        ILoginViewModel Model { set; get; }
	}
	
	[Export(typeof(ILoginView))]
	public partial class LoginView : UserControl, ILoginView
	{
		public LoginView()
		{
			// Required to initialize variables
			InitializeComponent();
		}

	    public ILoginViewModel Model
	    {
            get { return DataContext as ILoginViewModel; }
            set { DataContext = value; }
	    }
	}
}