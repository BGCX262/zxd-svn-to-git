using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace ZxdFramework.Shell.UI.Views
{

    /// <summary>
    /// 右侧视图
    /// </summary>
    public interface ILeftNavigateView
    {
        ILeftNavigateViewModel Model { set; get; }
    }

    [Export(typeof(ILeftNavigateView))]
	public partial class LeftNavigateView : UserControl, ILeftNavigateView
	{
		public LeftNavigateView()
		{
			// Required to initialize variables
			InitializeComponent();
		}

	    public ILeftNavigateViewModel Model
	    {
            get { return DataContext as ILeftNavigateViewModel; }
            set { DataContext = value; }
	    }
	}
}