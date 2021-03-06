/**
 * 创建者：宗旭东
 *
 */

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace ZxdFramework.Controls.Components.Bindings
{
    public static class AttachedBindingExtensions
    {
        private static readonly DependencyProperty AttachedBindingsProperty =
            DependencyProperty.RegisterAttached("AttachedBindings",
                                                typeof (Dictionary<DependencyProperty, AttachedBinding>),
                                                typeof (AttachedBindingExtensions), null);

        public static void SetAttachedBinding(this DependencyObject target, FrameworkElement attachTarget,
                                              BindingInfo bindingInfo)
        {
            if (bindingInfo == null) throw new ArgumentNullException("bindingInfo");
            SetAttachedBinding(target, attachTarget, bindingInfo.BindingProperty, bindingInfo.BindingType,
                               bindingInfo.Binding);
        }

        public static void SetAttachedBinding<T>(this DependencyObject target, FrameworkElement attachTarget,
                                                 DependencyProperty bindingProperty, Binding binding)
        {
            SetAttachedBinding(target, attachTarget, bindingProperty, typeof (T), binding);
        }

        public static void SetAttachedBinding(this DependencyObject target, FrameworkElement attachTarget,
                                              DependencyProperty bindingProperty, Type bindingType, Binding binding)
        {
            // basic check
            if (target == null) throw new ArgumentNullException("target");
            if (attachTarget == null) throw new ArgumentNullException("attachTarget");
            if (bindingProperty == null) throw new ArgumentNullException("bindingProperty");
            if (bindingType == null) throw new ArgumentNullException("bindingType");

            // we get the bindings dictionary or lazy create it
            // note we add to the owner, because it can only one of each dependency property
            var _bindings = target.GetValue(AttachedBindingsProperty)
                            as Dictionary<DependencyProperty, AttachedBinding>;
            if (_bindings == null)
            {
                _bindings = new Dictionary<DependencyProperty, AttachedBinding>();
                attachTarget.SetValue(AttachedBindingsProperty, _bindings);
            }

            // and we save the binding
            AttachedBinding _atachedBinding = (_bindings.ContainsKey(bindingProperty)
                                                   ? _bindings[bindingProperty]
                                                   : null) ??
                                              new AttachedBinding(target, attachTarget, bindingProperty, bindingType);

            // and we set the binding
            _atachedBinding.SetBinding(binding);

            // and we save the binding
            if (_bindings.ContainsKey(bindingProperty))
                _bindings[bindingProperty] = _atachedBinding;
            else
                _bindings.Add(bindingProperty, _atachedBinding);
        }

        public static Binding GetAttachedBinding(this DependencyObject target, DependencyProperty bindingProperty)
        {
            // basic checks
            if (target == null) throw new ArgumentNullException("target");
            if (bindingProperty == null) throw new ArgumentNullException("bindingProperty");

            // we get the binding dictionary
            var _bindings = target.GetValue(AttachedBindingsProperty)
                            as Dictionary<DependencyProperty, AttachedBinding>;
            if (_bindings == null) return null;

            // we return the binding
            return _bindings.ContainsKey(bindingProperty) ? _bindings[bindingProperty].Binding : null;
        }

        public static void ClearAttachedBinding(this DependencyObject target, DependencyProperty bindingProperty)
        {
            // basic checks
            if (target == null) throw new ArgumentNullException("target");
            if (bindingProperty == null) throw new ArgumentNullException("bindingProperty");

            // we get the binding dictionary
            var _bindings = target.GetValue(AttachedBindingsProperty)
                            as Dictionary<DependencyProperty, AttachedBinding>;
            if (_bindings == null) return;

            // we clear the specific binding
            if (_bindings.ContainsKey(bindingProperty)) _bindings.Remove(bindingProperty);
        }

        public static void ClearAttachedBindings(this DependencyObject target)
        {
            // basic check
            if (target == null) throw new ArgumentNullException("target");

            // we get the binding dictionary
            var _bindings = target.GetValue(AttachedBindingsProperty)
                            as Dictionary<DependencyProperty, AttachedBinding>;
            if (_bindings == null) return;

            // we clear
            _bindings.Clear();
            target.ClearValue(AttachedBindingsProperty);
        }
    }
}