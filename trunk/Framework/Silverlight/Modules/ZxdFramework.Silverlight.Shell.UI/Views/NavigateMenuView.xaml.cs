using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace ZxdFramework.Shell.UI.Views
{
    /// <summary>
    /// 导航视图
    /// </summary>
    public interface INavigateMenuView
    {
        INavigateMenuViewModel Model { set; get; }
    }

    [Export(typeof(INavigateMenuView))]
	public partial class NavigateMenuView : UserControl, INavigateMenuView
	{
		public NavigateMenuView()
		{
			// Required to initialize variables
			InitializeComponent();
		}

	    public INavigateMenuViewModel Model
	    {
            get { return DataContext as INavigateMenuViewModel; }
            set { DataContext = value; }
	    }
	}
}