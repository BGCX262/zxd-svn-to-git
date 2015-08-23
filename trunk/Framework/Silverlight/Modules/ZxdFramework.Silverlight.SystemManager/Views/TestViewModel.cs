using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Events;
using ZxdFramework.DataContract;
using System.ComponentModel.Composition;

namespace ZxdFramework.SystemManager.Views
{
    public  interface  ITestViewModel
    {

        ITestView View { get; }
    }

    [Export(typeof(ITestViewModel))]
    public class TestViewModel:ViewModel<ITestView>,ITestViewModel
    {
        [ImportingConstructor]
        public TestViewModel(ITestView view, IEventAggregator aggregator)
            : base(view, aggregator)
        {
            View.Model = this;
        }
    }
}
