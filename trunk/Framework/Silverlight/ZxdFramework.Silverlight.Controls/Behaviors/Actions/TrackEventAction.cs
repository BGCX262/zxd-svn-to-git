using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Interactivity;

namespace ZxdFramework.Controls.Behaviors.Actions
{
    /// <summary>
    /// 对外传递事件
    /// </summary>
    public class TrackEventAction : TriggerAction<FrameworkElement>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CategoryProperty =
        DependencyProperty.Register("Category", typeof(string),
        typeof(TrackEventAction),
        new PropertyMetadata("Silverlight.Event"));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ActionProperty =
        DependencyProperty.Register("Action", typeof(string),
        typeof(TrackEventAction),
        new PropertyMetadata("Unknown Action"));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register("Label", typeof(string),
        typeof(TrackEventAction),
        new PropertyMetadata("Unknown Action fired"));

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public string Category
        {
            get { return (string)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        public string Action
        {
            get { return (string)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        /// <summary>
        /// Invokes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override void Invoke(object parameter)
        {
            try
            {
                HtmlPage.Window.Invoke("trackEvent", new object[] { Category, Action, Label });
            }
            catch
            {
            }
        }
    }
}