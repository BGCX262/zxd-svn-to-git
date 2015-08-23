using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace ZxdFramework.Shell.UI.Views
{
	/// <summary>
	/// 桌面内容
	/// </summary>
	public interface IContentView
	{
        IContentViewModel Model { set; get; }
	}
	
    [Export(typeof(IContentView))]
	public partial class ContentView : UserControl, IContentView
	{
		public ContentView()
		{
			// Required to initialize variables
			InitializeComponent();
		}

	    public IContentViewModel Model
	    {
            get { return DataContext as IContentViewModel; }
            set { DataContext = value; }
	    }
	}
}