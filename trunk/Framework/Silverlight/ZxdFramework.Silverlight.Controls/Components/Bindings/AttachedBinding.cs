/**
 * 创建者：宗旭东
 *
 */

using System;
using System.Windows;
using System.Windows.Data;

namespace ZxdFramework.Controls.Components.Bindings
{
    public class AttachedBinding
    {
        private const string DP_NAME_FROMAT = "Attached_Binding_{0}";
        private static int _indexer;

        private readonly WeakReference _attachTarget;
        private readonly DependencyProperty _bindingProperty;
        private readonly WeakReference _target;
        private DependencyProperty _attachedProperty;
        private Binding _binding;

        public AttachedBinding(DependencyObject target, FrameworkElement attachTarget,
                               DependencyProperty bindingProperty, Type bindingType)
        {
            // basic checks
            if (target == null) throw new ArgumentNullException("target");
            if (attachTarget == null) throw new ArgumentNullException("attachTarget");
            if (bindingProperty == null) throw new ArgumentNullException("bindingProperty");
            if (bindingType == null) throw new ArgumentNullException("bindingType");

            // we save the reference to the source
            _target = new WeakReference(target);
            _attachTarget = new WeakReference(attachTarget);
            _bindingProperty = bindingProperty;

            // we get the default value
            object _defValue = bindingProperty.GetMetadata(bindingType).DefaultValue;

            // we attach the dp
            if (attachTarget != null)
            {
                // we create the attached property
                _attachedProperty = DependencyProperty.RegisterAttached(string.Format(DP_NAME_FROMAT, _indexer++),
                                                                        bindingType, attachTarget.GetType(),
                                                                        new PropertyMetadata(_defValue,
                                                                                             OnPropertyChanged));
            }
            else
            {
                attachTarget.Loaded += (s, e) =>
                                           {
                                               // we create the binding property
                                               _attachedProperty =
                                                   DependencyProperty.RegisterAttached(
                                                       string.Format(DP_NAME_FROMAT, _indexer++),
                                                       bindingType, attachTarget.GetType(),
                                                       new PropertyMetadata(_defValue, OnPropertyChanged));

                                               // and we if have binding then 
                                               if (_binding != null) SetBinding(_binding);
                                           };
            }
        }

        #region Properties

        public FrameworkElement AttachTarget
        {
            get
            {
                if (_attachTarget == null || !_attachTarget.IsAlive) return null;
                object _attachTargetObject = _attachTarget.Target;
                return _attachTargetObject as FrameworkElement;
            }
        }

        public DependencyObject Target
        {
            get
            {
                if (_target == null || !_target.IsAlive) return null;
                object _ownerObject = _target.Target;
                return _ownerObject as DependencyObject;
            }
        }

        public DependencyProperty BindingProperty
        {
            get { return _bindingProperty; }
        }

        public DependencyProperty AttachedProperty
        {
            get { return _attachedProperty; }
        }

        public Binding Binding
        {
            get { return _binding; }
        }

        #endregion

        #region Helpers

        public void SetBinding(Binding binding)
        {
            // we save the binding
            _binding = binding;

            // we set the binding
            FrameworkElement _attachTargetObj = AttachTarget;
            if (_attachTargetObj != null) _attachTargetObj.SetBinding(AttachedProperty, binding);
        }

        private void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // we get the owner
            DependencyObject _ownerObj = Target;
            if (_ownerObj != null) _ownerObj.SetValue(BindingProperty, e.NewValue);
        }

        #endregion
    }
}