using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using ZxdFramework.Commands;
using ZxdFramework.DataContract;
using ZxdFramework.DataContract.Events;
using ZxdFramework.Service.Events;
using ZxdFramework.Service.Services;
using ZxdFramework.Shell.UI.Events;

namespace ZxdFramework.Shell.UI.Views
{
	public interface ILoginViewModel
	{
	    ILoginView View { get; }
	}
	
	[Export(typeof(ILoginViewModel))]
	public class LoginViewModel : ViewModel<ILoginView>, ILoginViewModel
	{
        [Import]
        public IAccountService AccountService;

        [ImportingConstructor]
	    public LoginViewModel(ILoginView view, IEventAggregator aggregator) : base(view, aggregator)
        {
            LoginCommand = new DelegateCommand(DoLogin, CanLogin);
            View.Model = this;
        }

        #region 登录代码区域

        private string _password;
        private string _username;
        public DelegateCommand LoginCommand { protected set; get; }

        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    Notify(() => Username);
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    Notify(() => Password);
                }
            }
        }


        private void DoLogin()
        {
            var @event = new UserLoginEvent(View)
            {
                LoginName = Username,
                LoginPassword = Password
            };

            @event.Callback = (r, p) =>
            {
                if (@event.LoginUser != null)
                {
                    Globals.CurrentUser = @event.LoginUser;
                    EventAggregator.Publish(new AccountChangedEvent
                    {
                        CurrentUser = @event.LoginUser
                    });

                    EventAggregator.Publish(new LoginViewStateChangeEvent
                                                {
                                                    IsShow = false
                                                });
                }
            };

            EventAggregator.Publish(@event);
        }

        private bool CanLogin()
        {
            //return !string.IsNullOrWhiteSpace(Username) || !string.IsNullOrWhiteSpace(Password);
            return true;
        }

        #endregion
	}
}