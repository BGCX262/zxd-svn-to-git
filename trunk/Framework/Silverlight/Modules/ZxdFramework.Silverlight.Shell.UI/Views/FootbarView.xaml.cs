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
    /// <summary>
    /// 底部视图接口
    /// </summary>
    public interface IFootbarView
    {
        /// <summary>
        /// 绑定模型
        /// </summary>
        /// <value>The model.</value>
        IFootbarViewModel Model { set; get; }
    }

    [Export(typeof(IFootbarView))]
    public partial class FootbarView : UserControl, IFootbarView
    {
        public FootbarView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绑定模型
        /// </summary>
        /// <value>The model.</value>
        public IFootbarViewModel Model
        {
            get { return DataContext as IFootbarViewModel; }
            set { DataContext = value; }
        }

    }
}
