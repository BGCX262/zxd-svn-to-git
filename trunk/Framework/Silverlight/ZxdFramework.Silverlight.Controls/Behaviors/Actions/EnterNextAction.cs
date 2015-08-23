/**
 * 创建者：宗旭东
 *
 */

using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace ZxdFramework.Controls.Behaviors.Actions
{
    /// <summary>
    /// 如果在当前的控件上面回车, 那么将焦点移到设置的 Target 控件
    /// </summary>
    public class EnterNextAction : TargetedTriggerAction<Control>
    {
        protected override void Invoke(object parameter)
        {
            var keyEventArgs = parameter as KeyEventArgs;
            if (null != keyEventArgs && keyEventArgs.Key == Key.Enter && Target != null)
            {
                Target.Focus();
            }
        }

    }
}