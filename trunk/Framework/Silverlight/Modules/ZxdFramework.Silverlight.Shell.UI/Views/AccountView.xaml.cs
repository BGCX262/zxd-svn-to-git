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

namespace ZxdFramework.Shell.UI.Views
{

    public interface IAccountView
    {
        IAccountViewModel Model { set; get; }
    }

    /// <summary>
    /// 用户制定的显示信息
    /// </summary>
    [Export(typeof(IAccountView))]
    public partial class AccountView : UserControl, IAccountView
    {
        public AccountView()
        {
            InitializeComponent();
        }

        public IAccountViewModel Model
        {
            get { return DataContext as IAccountViewModel; }
            set { DataContext = value; }
        }
    }
}
