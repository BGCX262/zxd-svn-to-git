using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using C1.Silverlight;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using ZxdFramework.Commands;
using ZxdFramework.DataContract;
using ZxdFramework.Logging.Events;
using Category = Microsoft.Practices.Prism.Logging.Category;

namespace ZxdFramework.Logging.UI
{

    public interface ILoggingWindowViewModel
    {
        /// <summary>
        /// Gets the view.
        /// </summary>
        ILoggingWindowView View { get; }

        /// <summary>
        /// 显示当前窗口
        /// </summary>
        DelegateCommand ShowCommand { get; }

        /// <summary>
        /// 获取当前窗口是否显示
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is show logging window; otherwise, <c>false</c>.
        /// </value>
        bool IsShowLoggingWindow { set; get; }
    }

    [Export(typeof(ILoggingWindowViewModel))]
    public class LoggingWindowViewModel : ViewModel<ILoggingWindowView>, ILoggingWindowViewModel
    {
        [ImportingConstructor]
        public LoggingWindowViewModel(ILoggingWindowView view, IEventAggregator aggregator) : base(view, aggregator)
        {
            ShowCommand = new DelegateCommand(DoShowCommand, CanShowCommand);
            View.Model = this;
            //Logging.Events.LoggingEvent
            EventAggregator.Subscribe<LoggingEvent>(RunLogging);
        }

        private bool _isShowLoggingWindow = false;
        public bool IsShowLoggingWindow
        {
            get { return _isShowLoggingWindow; }
            set
            {
                if (_isShowLoggingWindow != value)
                {
                    _isShowLoggingWindow = value;
                    ShowCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private int _maxRow = 100;
        /// <summary>
        /// 设置获取最大的显示行数
        /// </summary>
        /// <value>
        /// The max row.
        /// </value>
        public int MaxRow
        {
            get { return _maxRow; }
            set
            {
                if (_maxRow != value)
                {
                    _maxRow = value;
                    Notify(() => this.MaxRow);
                }
            }
        }



        public void RunLogging(LoggingEvent @event)
        {
            var par = new Paragraph();

            var titleRun = new Run
                               {
                                   Text = Enum.GetName(typeof (Category), @event.Category) + " "
                               };

            switch (@event.Category)
            {
                case Category.Debug:
                    titleRun.Foreground = new SolidColorBrush(Colors.Black);
                    break;                    
                case Category.Exception:
                    titleRun.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                case Category.Info:
                    titleRun.Foreground = new SolidColorBrush(Colors.Blue);
                    break;
                default:
                    titleRun.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
            }

            par.Inlines.Add(titleRun);
            par.Inlines.Add(@event.Time.ToString("HH:mm:ss.fff") + " :");
            par.Inlines.Add(@event.Message);
            //par.Inlines.Add(new LineBreak());
            View.LoggingTextBox.Blocks.Add(par);
            ClearFirstRow();

        }

        void ClearFirstRow()
        {
            if(View.LoggingTextBox.Blocks.Count > MaxRow)
            {
                View.LoggingTextBox.Blocks.RemoveAt(0);
            }
        }


        #region 加载命令

        public DelegateCommand ShowCommand { protected set; get; }

        bool CanShowCommand()
        {
            return !IsShowLoggingWindow;
        }

        void DoShowCommand()
        {
            IsShowLoggingWindow = true;
            View.Show();
        }
        #endregion


    }
}