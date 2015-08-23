using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Events;
using ZxdFramework.Commands;
using ZxdFramework.DataContract;

namespace ZxdFramework.Shell.UI.Views
{
    /// <summary>
    /// 底部视图模型
    /// </summary>
    public interface IFootbarViewModel
    {
		/// <summary>
		/// 视图
		/// </summary>
		/// <returns></returns>
        IFootbarView View { get; }

        /// <summary>
        /// 设置获取是否显示当前的导航菜单
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is show menu; otherwise, <c>false</c>.
        /// </value>
		bool IsShowMenu { set;get; }

        /// <summary>
        /// 显示导航菜单窗口命令
        /// </summary>
        /// <value>The show menu command.</value>
        ICommand ShowMenuCommand { get; }
    }

    [Export(typeof(IFootbarViewModel))]
    public class FootbarViewModel : ViewModel<IFootbarView>, IFootbarViewModel
    {
        [ImportingConstructor]
        public FootbarViewModel(IFootbarView view, IEventAggregator aggregator) : base(view, aggregator)
        {
            ShowMenuCommand = new DelegateCommand(DoShowMenu);
            HideMenuCommand = new DelegateCommand(DoHideMenu);
            IsShowMenu = false;
            View.Model = this;
        }

        private bool _isShowmenu;
        public bool IsShowMenu
        {
            get { return _isShowmenu; }
            set
            {
                if (_isShowmenu != value)
                {
                    _isShowmenu = value;
                    Notify(() => this.IsShowMenu);
                }
            }
        }



        public ICommand ShowMenuCommand { protected set; get; }

        void DoShowMenu()
        {
            IsShowMenu = true;
        }

        public ICommand HideMenuCommand { protected set; get; }

        void DoHideMenu()
        {
            IsShowMenu = false;
        }

    }
}