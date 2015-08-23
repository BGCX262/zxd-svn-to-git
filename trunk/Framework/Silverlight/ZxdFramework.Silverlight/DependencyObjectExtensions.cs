using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZxdFramework
{
    /// <summary>
    /// 扩展 Silverlight 基本对象的方法
    /// </summary>
    public static class DependencyObjectExtensions
    {

        /// <summary>
        /// 把当前的可视对象从他的父节点中移除
        /// </summary>
        /// <param name="element"></param>
        public static bool RemoveFromParentPanel(this DependencyObject element)
        {
            UIElement removeObject = element as UIElement;

            if (removeObject != null)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(removeObject) as Panel;
                if (parent == null && removeObject is FrameworkElement)
                    parent = ((FrameworkElement)removeObject).Parent;

                if (parent is Panel)
                {
                    ((Panel)parent).Children.Remove(removeObject);
                    return true;
                }
                else if (parent is ItemsControl)
                {
                    ((ItemsControl)parent).Items.Remove(removeObject);
                    return true;
                }
                else if (parent is ContentControl) // && ((ContentControl)removeObject.Parent).Content is Panel)
                {
                    ((ContentControl)parent).Content = null;
                    return true;
                }
            }

            return false;
        }
    }
}