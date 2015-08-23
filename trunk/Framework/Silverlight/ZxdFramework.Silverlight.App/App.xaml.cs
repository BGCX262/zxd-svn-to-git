using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Browser;
using ZxdFramework.DataContract;
using ZxdFramework.DataContract.Events;

namespace ZxdFramework.App
{
    /// <summary>
    /// 启动类
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += Application_Startup;
            Exit += Application_Exit;
            UnhandledException += Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //if (!App.Current.IsRunningOutOfBrowser)
            //{
            //    Globals.ServerRoot = HtmlPage.Document.DocumentUri;
            //}
            Globals.Init();
            var boot = new Bootstrapper();
            boot.Run();
        }

        private void Application_Exit(object sender, EventArgs e)
        {
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {

            if (Globals.EventAggregator != null)
            {
                Globals.EventAggregator.Publish(new ApplicationErrorEvent(e.ExceptionObject));
                e.Handled = true;
            }


            // 如果应用程序是在调试器外运行的，则使用浏览器的
            // 异常机制报告该异常。在 IE 上，将在状态栏中用一个 
            // 黄色警报图标来显示该异常，而 Firefox 则会显示一个脚本错误。
            if (!(e.Handled || Debugger.IsAttached))
            {
                // 注意: 这使应用程序可以在已引发异常但尚未处理该异常的情况下
                // 继续运行。 
                // 对于生产应用程序，此错误处理应替换为向网站报告错误
                // 并停止应用程序。
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}