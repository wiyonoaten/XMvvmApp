using Android.Views;
using Android.Widget;
using System;
using System.Linq.Expressions;
using XMvvmApp.Mvvm;
using XMvvmApp.Mvvm.Bindings;
using XMvvmApp.Utils;

namespace XMvvmApp.Android.Mvvm.Binders
{
    public class PropertyValueBinder<T> : IBinder
    {
        protected readonly Expression<Func<T>> _propertyExp;
        protected readonly string _propertyName;
        
        public BindingCollection Bindings { get; }

        public PropertyValueBinder(Expression<Func<T>> propertyExp)
        {
            _propertyExp = propertyExp;
            _propertyName = propertyExp.GetPropertyName();

            this.Bindings = new BindingCollection();
        }

        private IViewModel _GetPropertyOwner()
        {
            return _propertyExp.GetPropertyOwner<T, IViewModel>();
        }

        private T _GetPropertyValue()
        {
            return _propertyExp.GetPropertyValue();
        }

        public PropertyValueBinder<T> BindToViewEnabled(View view, IValueConverter<T, bool> valueConverter)
        {
            view.Enabled = valueConverter.GetBoolValue(_GetPropertyValue());

            var weakView = new WeakReference<View>(view);
            this.Bindings.Add(new ViewModelPropertyChangedBinding(_GetPropertyOwner(), (sender, args) =>
            {
                var view_ = weakView.Get();
                if (view_ != null)
                {
                    view.Enabled = valueConverter.GetBoolValue(_GetPropertyValue());
                }
            }));
            return this;
        }

        public PropertyValueBinder<T> BindToViewVisibility(View view, IValueConverter<T, ViewStates> valueConverter)
        {
            view.Visibility = valueConverter.GetConvertedValue(_GetPropertyValue());

            var weakView = new WeakReference<View>(view);
            this.Bindings.Add(new ViewModelPropertyChangedBinding(_GetPropertyOwner(), (sender, args) =>
            {
                var view_ = weakView.Get();
                if (view_ != null)
                {
                    view.Visibility = valueConverter.GetConvertedValue(_GetPropertyValue());
                }
            }));
            return this;
        }

        public PropertyValueBinder<T> BindToViewAlpha(View view, IValueConverter<T, float> valueConverter)
        {
            view.Alpha = valueConverter.GetConvertedValue(_GetPropertyValue());

            var weakView = new WeakReference<View>(view);
            this.Bindings.Add(new ViewModelPropertyChangedBinding(_GetPropertyOwner(), (sender, args) =>
            {
                var view_ = weakView.Get();
                if (view_ != null)
                {
                    view.Alpha = valueConverter.GetConvertedValue(_GetPropertyValue());
                }
            }));
            return this;
        }

        public PropertyValueBinder<T> BindToTextViewText(TextView textView, IValueConverter<T, string> valueConverter)
        {
            textView.Text = valueConverter.GetStringValue(_GetPropertyValue());

            var weakTextView = new WeakReference<TextView>(textView);
            this.Bindings.Add(new ViewModelPropertyChangedBinding(_GetPropertyOwner(), (sender, args) =>
            {
                var textView_ = weakTextView.Get();
                if (textView_ != null)
                {
                    textView_.Text = valueConverter.GetStringValue(_GetPropertyValue());
                }
            }));
            return this;
        }

        public PropertyValueBinder<T> BindToCompoundButtonChecked(CompoundButton compoundButton, IValueConverter<T, bool> valueConverter)
        {
            compoundButton.Checked = valueConverter.GetBoolValue(_GetPropertyValue());

            var weakCompoundButton = new WeakReference<CompoundButton>(compoundButton);
            this.Bindings.Add(new ViewModelPropertyChangedBinding(_GetPropertyOwner(), (sender, args) =>
            {
                var compoundButton_ = weakCompoundButton.Get();
                if (compoundButton_ != null)
                {
                    compoundButton_.Checked = valueConverter.GetBoolValue(_GetPropertyValue());
                }
            }));
            return this;
        }

        public PropertyValueBinder<T> BindToMenuItemEnabled(IMenuItem menuItem, IValueConverter<T, bool> valueConverter)
        {
            menuItem.SetEnabled(valueConverter.GetBoolValue(_GetPropertyValue()));

            var weakCompoundButton = new WeakReference<IMenuItem>(menuItem);
            this.Bindings.Add(new ViewModelPropertyChangedBinding(_GetPropertyOwner(), (sender, args) =>
            {
                var menuItem_ = weakCompoundButton.Get();
                if (menuItem_ != null)
                {
                    menuItem_.SetEnabled(valueConverter.GetBoolValue(_GetPropertyValue()));
                }
            }));
            return this;
        }
    }
}