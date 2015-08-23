using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using ZxdFramework.Commands;
using ZxdFramework.DataContract;
using ZxdFramework.DataContract.Events;
using ZxdFramework.Infrastructure.Services;
using ZxdFramework.Service.Events;
using ZxdFramework.Shell.UI.Events;

namespace ZxdFramework.Shell.UI.Views
{
    /// <summary>
    /// 主界面显示模型
    /// </summary>
    public interface IShellViewModel
    {
        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <value>The view.</value>
        IShellView View { get; }
    }

    [Export(typeof (IShellViewModel))]
    public class ShellViewModel : ViewModel<IShellView>, IShellViewModel
    {

        [ImportingConstructor]
        public ShellViewModel(IShellView view, IEventAggregator aggregator) : base(view, aggregator)
        {
            //LoginCommand = new DelegateCommand(DoLogin, CanLogin);
            EventAggregator.Subscribe<LoginViewStateChangeEvent>(OnViewStateChange);
            View.Model = this;
        }

        public void OnViewStateChange(LoginViewStateChangeEvent @event)
        {
            View.IsShowLogin = @event.IsShow;
        }

    }
}