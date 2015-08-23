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
using C1.Silverlight;
using ZxdFramework.DataContract.UI;

namespace ZxdFramework.Logging.UI
{

    public interface ILoggingWindowView : IWindow
    {
        ILoggingWindowViewModel Model { set; get; }

        /// <summary>
        /// 获取日志记录显示文本
        /// </summary>
        RichTextBox LoggingTextBox { get; }
    }

    [Export(typeof(ILoggingWindowView))]
    public partial class LoggingWindowView : C1Window, ILoggingWindowView
    {
        public LoggingWindowView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public ILoggingWindowViewModel Model
        {
            get { return DataContext as ILoggingWindowViewModel; }
            set { DataContext = value; }
        }


        public RichTextBox LoggingTextBox
        {
            get { return textBox; }
        }

        protected void C1Window_Closing(object sender, CancelEventArgs e)
        {
            Model.IsShowLoggingWindow = false;
            e.Cancel = true;
            Hide();
        }
    }
}
