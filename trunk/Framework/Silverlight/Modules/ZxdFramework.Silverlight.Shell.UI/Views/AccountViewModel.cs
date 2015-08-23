using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using ZxdFramework.App;
using ZxdFramework.DataContract;
using ZxdFramework.DataContract.Events;

namespace ZxdFramework.Shell.UI.Views
{
    /// <summary>
    /// 账号窗口模型
    /// </summary>
    public interface IAccountViewModel
    {
        /// <summary>
        /// Gets the view.
        /// </summary>
        IAccountView View { get; }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <value>The current user.</value>
        IUser CurrentUser { get; }
    }

    [Export(typeof(IAccountViewModel))]
    public class AccountViewModel : ViewModel<IAccountView>, IAccountViewModel
    {
        [ImportingConstructor]
        public AccountViewModel(IAccountView view, IEventAggregator aggregator) : base(view, aggregator)
        {
            View.Model = this;
            EventAggregator.Subscribe<AccountChangedEvent>(DoUserChanged);
        }


        public IUser CurrentUser 
        {
            get
            {
                return Globals.CurrentUser;
            }
        }

        public void DoUserChanged(AccountChangedEvent @event)
        {
            NotifyPropertyChanged("CurrentUser");
        }
    }
}