using Android.Text;
using Android.Views;
using Android.Widget;
using System;
using XMvvmApp.Mvvm;
using XMvvmApp.Mvvm.Binders;
using XMvvmApp.Mvvm.Bindings;
using XMvvmApp.Utils;

namespace XMvvmApp.Android.Mvvm.Binders
{
    public static class PropertyValueBinderAndroidExtensions
    {
        #region To Views

        public static PropertyValueBinder<T> BindToViewEnabled<T>(this PropertyValueBinder<T> binder,
            View view, IValueConverter<T, bool> valueConverter)
        {
            view.Enabled = valueConverter.GetBoolValue(binder.PropertyValue);

            var weakView = new WeakReference<View>(view);
            binder.Bindings.Add(new PropertyChangedBinding<T>(binder.PropertyOwner, binder.PropertyExp, (newValue) =>
            {
                var view_ = weakView.Get();
                if (view_ != null)
                {
                    view.Enabled = valueConverter.GetBoolValue(newValue);
                }
            }));
            return binder;
        }

        public static PropertyValueBinder<T> BindToViewVisibility<T>(this PropertyValueBinder<T> binder, 
            View view, IValueConverter<T, ViewStates> valueConverter)
        {
            view.Visibility = valueConverter.GetConvertedValue(binder.PropertyValue);

            var weakView = new WeakReference<View>(view);
            binder.Bindings.Add(new PropertyChangedBinding<T>(binder.PropertyOwner, binder.PropertyExp, (newValue) =>
            {
                var view_ = weakView.Get();
                if (view_ != null)
                {
                    view.Visibility = valueConverter.GetConvertedValue(newValue);
                }
            }));
            return binder;
        }

        public static PropertyValueBinder<T> BindToViewAlpha<T>(this PropertyValueBinder<T> binder, 
            View view, IValueConverter<T, float> valueConverter)
        {
            view.Alpha = valueConverter.GetConvertedValue(binder.PropertyValue);

            var weakView = new WeakReference<View>(view);
            binder.Bindings.Add(new PropertyChangedBinding<T>(binder.PropertyOwner, binder.PropertyExp, (newValue) =>
            {
                var view_ = weakView.Get();
                if (view_ != null)
                {
                    view.Alpha = valueConverter.GetConvertedValue(newValue);
                }
            }));
            return binder;
        }

        public static PropertyValueBinder<T> BindToTextViewText<T>(this PropertyValueBinder<T> binder, 
            TextView textView, IValueConverter<T, string> valueConverter)
        {
            textView.Text = valueConverter.GetStringValue(binder.PropertyValue);

            var weakTextView = new WeakReference<TextView>(textView);
            binder.Bindings.Add(new PropertyChangedBinding<T>(binder.PropertyOwner, binder.PropertyExp, (newValue) =>
            {
                var textView_ = weakTextView.Get();
                if (textView_ != null)
                {
                    textView_.Text = valueConverter.GetStringValue(newValue);
                }
            }));
            return binder;
        }

        public static PropertyValueBinder<T> BindToCompoundButtonChecked<T>(this PropertyValueBinder<T> binder, 
            CompoundButton compoundButton, IValueConverter<T, bool> valueConverter)
        {
            compoundButton.Checked = valueConverter.GetBoolValue(binder.PropertyValue);

            var weakCompoundButton = new WeakReference<CompoundButton>(compoundButton);
            binder.Bindings.Add(new PropertyChangedBinding<T>(binder.PropertyOwner, binder.PropertyExp, (newValue) =>
            {
                var compoundButton_ = weakCompoundButton.Get();
                if (compoundButton_ != null)
                {
                    compoundButton_.Checked = valueConverter.GetBoolValue(newValue);
                }
            }));
            return binder;
        }

        public static PropertyValueBinder<T> BindToMenuItemEnabled<T>(this PropertyValueBinder<T> binder, 
            IMenuItem menuItem, IValueConverter<T, bool> valueConverter)
        {
            menuItem.SetEnabled(valueConverter.GetBoolValue(binder.PropertyValue));

            var weakMenuItem = new WeakReference<IMenuItem>(menuItem);
            binder.Bindings.Add(new PropertyChangedBinding<T>(binder.PropertyOwner, binder.PropertyExp, (newValue) =>
            {
                var menuItem_ = weakMenuItem.Get();
                if (menuItem_ != null)
                {
                    menuItem_.SetEnabled(valueConverter.GetBoolValue(newValue));
                }
            }));
            return binder;
        }

        #endregion

        #region From Views

        public static PropertyValueBinder<T> BindFromTextViewText<T>(this PropertyValueBinder<T> binder,
            TextView textView, IValueConverter<T, string> valueConverter)
        {
            binder.PropertyExp.SetPropertyValue(valueConverter.FromStringValue(textView.Text));

            var weakTextView = new WeakReference<TextView>(textView);
            binder.Bindings.Add(new EventHandlerBinding<AfterTextChangedEventArgs>(_ => textView.AfterTextChanged += _, _ => textView.AfterTextChanged -= _, (sender, args) =>
            {
                var textView_ = weakTextView.Get();
                if (textView_ != null)
                {
                    binder.PropertyExp.SetPropertyValue(valueConverter.FromStringValue(textView_.Text));
                }
            }));
            return binder;
        }

        public static PropertyValueBinder<T> BindFromCompoundButtonChecked<T>(this PropertyValueBinder<T> binder,
            CompoundButton compoundButton, IValueConverter<T, bool> valueConverter)
        {
            binder.PropertyExp.SetPropertyValue(valueConverter.FromBoolValue(compoundButton.Checked));

            var weakCompoundButton = new WeakReference<CompoundButton>(compoundButton);
            binder.Bindings.Add(new EventHandlerBinding<CompoundButton.CheckedChangeEventArgs>(_ => compoundButton.CheckedChange += _, _ => compoundButton.CheckedChange -= _, (sender, args) =>
            {
                var compoundButton_ = weakCompoundButton.Get();
                if (compoundButton_ != null)
                {
                    binder.PropertyExp.SetPropertyValue(valueConverter.FromBoolValue(compoundButton_.Checked));
                }
            }));
            return binder;
        }

        #endregion
    }
}