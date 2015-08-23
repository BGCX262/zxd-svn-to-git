/**
 * 创建者：宗旭东
 *
 */

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ZxdFramework.Controls.Behaviors.Interactivity;
using ZxdFramework.Controls.Converters;

namespace ZxdFramework.Controls.Behaviors.Actions
{
    /// <summary>
    /// 将一个事件传递到执行一个命令
    /// </summary>
    public class ExecuteCommandAction : BindableTriggerAction<FrameworkElement>
    {
        private const double INTERACTIVITY_ENABLED = 1d;
        private const double INTERACTIVITY_DISABLED = 0.5d;

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof (ICommand), typeof (ExecuteCommandAction),
                                        new PropertyMetadata(null, HandleCommandChanged));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.Register("Parameter", typeof (object), typeof (ExecuteCommandAction),
                                        new PropertyMetadata(null, OnParameterChanged));

        /// <summary>
        /// Invokes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override void Invoke(object parameter)
        {
            if (CommandValue != null && CommandValue.CanExecute(ParameterValue))
            {
                CommandValue.Execute(ParameterValue);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            DisposeEnableState();
        }

        #region Properties

        /// <summary>
        /// Gets or sets the command value.
        /// </summary>
        /// <value>The command value.</value>
        public ICommand CommandValue
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// 绑定命令
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public Binding Command
        {
            get { return GetBinding(CommandProperty); }
            set { SetBinding<ICommand>(CommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the parameter value.
        /// </summary>
        /// <value>The parameter value.</value>
        [TypeConverter(typeof (ConvertFromStringConverter))]
        public object ParameterValue
        {
            get { return GetValue(ParameterProperty); }
            set { SetValue(ParameterProperty, value); }
        }

        /// <summary>
        /// 绑定命令的参数
        /// </summary>
        /// <value>
        /// The parameter.
        /// </value>
        public Binding Parameter
        {
            get { return GetBinding(ParameterProperty); }
            set { SetBinding<object>(ParameterProperty, value); }
        }

        /// <summary>
        /// 设置获取是否启用命令的状态来管理当前对象的状态
        /// </summary>
        /// <value>
        ///   <c>true</c> if [manage enable state]; otherwise, <c>false</c>.
        /// </value>
        public bool ManageEnableState { get; set; }

        #endregion

        #region Handlers

        private static void HandleCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ExecuteCommandAction) d).SetupEnableState(e.NewValue as ICommand, e.OldValue as ICommand);
        }

        private static void OnParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ExecuteCommandAction) d).UpdateEnabledState();
        }

        private void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }

        #endregion

        #region Helpers

        private void SetupEnableState(ICommand newCommand, ICommand oldCommand)
        {
            // basic checks
            if (!ManageEnableState || AssociatedObject == null) return;

            // we detach or attach
            if (oldCommand != null)
                oldCommand.CanExecuteChanged -= Command_CanExecuteChanged;
            if (newCommand != null)
                newCommand.CanExecuteChanged += Command_CanExecuteChanged;

            // and update
            UpdateEnabledState();
        }

        private void UpdateEnabledState()
        {
            // basic checks
            if (!ManageEnableState || AssociatedObject == null || CommandValue == null) return;

            // we get if it is enabled or not
            bool canExecute = CommandValue.CanExecute(ParameterValue);

            // we check if it is a control            
            if (typeof (Control).IsAssignableFrom(AssociatedObject.GetType()))
            {
                // we check if 
                var control = AssociatedObject as Control;
                if (control != null) control.IsEnabled = canExecute;
            }
            else
            {
                AssociatedObject.IsHitTestVisible = canExecute;
                AssociatedObject.Opacity = canExecute ? INTERACTIVITY_ENABLED : INTERACTIVITY_DISABLED;
            }
        }

        private void DisposeEnableState()
        {
            if (!ManageEnableState || AssociatedObject == null || CommandValue == null) return;

            if (AssociatedObject as Control != null)
                CommandValue.CanExecuteChanged -= Command_CanExecuteChanged;
        }

        #endregion
    }
}