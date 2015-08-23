using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ZxdFramework.DataContract.UI;

namespace ZxdFramework.SystemManager.Views
{
    public interface  ITestView : IWindow
    {
        ITestViewModel Model { get; set; }

    }

    [Export(typeof(ITestView))]
    public partial class TestView : ChildWindow,ITestView
    {
        public TestView()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    
        #region ITestView 成员

        public ITestViewModel  Model
        {
	          get { return DataContext as ITestViewModel; }
	          set { DataContext = value; }
        }

        #endregion
    }
}

