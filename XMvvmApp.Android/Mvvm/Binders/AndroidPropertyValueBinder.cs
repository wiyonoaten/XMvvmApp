using Android.Views;
using Android.Widget;
using System;
using System.Linq.Expressions;
using XMvvmApp.Mvvm;
using XMvvmApp.Mvvm.Binders;
using XMvvmApp.Mvvm.Bindings;
using XMvvmApp.Utils;

namespace XMvvmApp.Android.Mvvm.Binders
{
    public class AndroidPropertyValueBinder<T> : PropertyValueBinder<T>
    {
        public AndroidPropertyValueBinder(Expression<Func<T>> propertyExp)
            : base(propertyExp)
        {
        }

        public AndroidPropertyValueBinder<T> BindToViewEnabled(View view, IValueConverter<T, bool> valueConverter)
        {
            view.Enabled = valueConverter.GetBoolValue(GetPropertyValue());

            var weakView = new WeakReference<View>(view);
            this.Bindings.Add(new ViewModelPropertyChangedBinding(GetPropertyOwner(), (sender, args) =>
            {
                var view_ = weakView.Get();
                if (view_ != null)
                {
                    view.Enabled = valueConverter.GetBoolValue(GetPropertyValue());
                }
            }));
            return this;
        }

        public AndroidPropertyValueBinder<T> BindToViewVisibility(View view, IValueConverter<T, ViewStates> valueConverter)
        {
            view.Visibility = valueConverter.GetConvertedValue(GetPropertyValue());

            var weakView = new WeakReference<View>(view);
            this.Bindings.Add(new ViewModelPropertyChangedBinding(GetPropertyOwner(), (sender, args) =>
            {
                var view_ = weakView.Get();
                if (view_ != null)
                {
                    view.Visibility = valueConverter.GetConvertedValue(GetPropertyValue());
                }
            }));
            return this;
        }

        public AndroidPropertyValueBinder<T> BindToViewAlpha(View view, IValueConverter<T, float> valueConverter)
        {
            view.Alpha = valueConverter.GetConvertedValue(GetPropertyValue());

            var weakView = new WeakReference<View>(view);
            this.Bindings.Add(new ViewModelPropertyChangedBinding(GetPropertyOwner(), (sender, args) =>
            {
                var view_ = weakView.Get();
                if (view_ != null)
                {
                    view.Alpha = valueConverter.GetConvertedValue(GetPropertyValue());
                }
            }));
            return this;
        }

        public AndroidPropertyValueBinder<T> BindToTextViewText(TextView textView, IValueConverter<T, string> valueConverter)
        {
            textView.Text = valueConverter.GetStringValue(GetPropertyValue());

            var weakTextView = new WeakReference<TextView>(textView);
            this.Bindings.Add(new ViewModelPropertyChangedBinding(GetPropertyOwner(), (sender, args) =>
            {
                var textView_ = weakTextView.Get();
                if (textView_ != null)
                {
                    textView_.Text = valueConverter.GetStringValue(GetPropertyValue());
                }
            }));
            return this;
        }

        public AndroidPropertyValueBinder<T> BindToCompoundButtonChecked(CompoundButton compoundButton, IValueConverter<T, bool> valueConverter)
        {
            compoundButton.Checked = valueConverter.GetBoolValue(GetPropertyValue());

            var weakCompoundButton = new WeakReference<CompoundButton>(compoundButton);
            this.Bindings.Add(new ViewModelPropertyChangedBinding(GetPropertyOwner(), (sender, args) =>
            {
                var compoundButton_ = weakCompoundButton.Get();
                if (compoundButton_ != null)
                {
                    compoundButton_.Checked = valueConverter.GetBoolValue(GetPropertyValue());
                }
            }));
            return this;
        }

        public AndroidPropertyValueBinder<T> BindToMenuItemEnabled(IMenuItem menuItem, IValueConverter<T, bool> valueConverter)
        {
            menuItem.SetEnabled(valueConverter.GetBoolValue(GetPropertyValue()));

            var weakCompoundButton = new WeakReference<IMenuItem>(menuItem);
            this.Bindings.Add(new ViewModelPropertyChangedBinding(GetPropertyOwner(), (sender, args) =>
            {
                var menuItem_ = weakCompoundButton.Get();
                if (menuItem_ != null)
                {
                    menuItem_.SetEnabled(valueConverter.GetBoolValue(GetPropertyValue()));
                }
            }));
            return this;
        }
    }
}