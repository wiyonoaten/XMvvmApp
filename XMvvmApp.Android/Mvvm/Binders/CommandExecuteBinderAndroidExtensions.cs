using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using System;
using XMvvmApp.Android.EventListeners;
using XMvvmApp.Mvvm;
using XMvvmApp.Mvvm.Binders;
using XMvvmApp.Mvvm.Bindings;
using XMvvmApp.Utils;

namespace XMvvmApp.Android.Mvvm.Binders
{
    public static class CommandExecuteBinderAndroidExtensions
    {
        public static CommandExecuteBinder<T> BindFromViewClick<T>(this CommandExecuteBinder<T> binder,
            View view)
        {
            var weakView = new WeakReference<View>(view);
            binder.Bindings.Add(new EventHandlerBinding(
                h => { var view_ = weakView.Get(); if (view_ != null) view_.Click += h; },
                h => { var view_ = weakView.Get(); if (view_ != null) view_.Click -= h; },
                (sender, args) => binder.ExecuteCommandIfCan()));
            return binder;
        }

        public static CommandExecuteBinder<T> BindFromMenuItemClick<T>(this CommandExecuteBinder<T> binder,
            IMenuItem menuItem, IValueConverter<IMenuItem, T> valueConverter = null)
        {
            var weakMenuItem = new WeakReference<IMenuItem>(menuItem);
            binder.Bindings.Add(new DelegateBinding<IMenuItemOnMenuItemClickListener>(
                l => { var menuItem_ = weakMenuItem.Get(); if (menuItem_ != null) menuItem_.SetOnMenuItemClickListener(l); },
                l => { var menuItem_ = weakMenuItem.Get(); if (menuItem_ != null) menuItem_.SetOnMenuItemClickListener(null); },
                new DelegateMenuItemOnMenuItemClickListener(args => binder.ExecuteCommandWithArgsIfCan(args, valueConverter))));
            return binder;
        }

        public static CommandExecuteBinder<T> BindFromListViewItemClick<T>(this CommandExecuteBinder<T> binder,
            ListView listView, IValueConverter<AdapterView.ItemClickEventArgs, T> valueConverter = null)
        {
            var weakListView = new WeakReference<ListView>(listView);
            binder.Bindings.Add(new EventHandlerBinding<AdapterView.ItemClickEventArgs>(
                h => { var listView_ = weakListView.Get(); if (listView_ != null) listView_.ItemClick += h; },
                h => { var listView_ = weakListView.Get(); if (listView_ != null) listView_.ItemClick -= h; },
                (sender, args) => binder.ExecuteCommandWithArgsIfCan(args, valueConverter)));
            return binder;
        }

        public static CommandExecuteBinder<T> BindFromTextViewEditorActionEvent<T>(this CommandExecuteBinder<T> binder,
            TextView textView, IValueConverter<TextView.EditorActionEventArgs, T> valueConverter = null)
        {
            var weakTextView = new WeakReference<TextView>(textView);
            binder.Bindings.Add(new EventHandlerBinding<TextView.EditorActionEventArgs>(
                h => { var textView_ = weakTextView.Get(); if (textView_ != null) textView_.EditorAction += h; },
                h => { var textView_ = weakTextView.Get(); if (textView_ != null) textView_.EditorAction -= h; },
                (sender, args) => args.Handled = binder.ExecuteCommandWithArgsIfCan(args, valueConverter)));
            return binder;
        }

        public static CommandExecuteBinder<T> BindFromTextViewEditorActionId<T>(this CommandExecuteBinder<T> binder,
            TextView textView, ImeAction imeActionId)
        {
            var weakTextView = new WeakReference<TextView>(textView);
            binder.Bindings.Add(new EventHandlerBinding<TextView.EditorActionEventArgs>(
                h => { var textView_ = weakTextView.Get(); if (textView_ != null) textView_.EditorAction += h; },
                h => { var textView_ = weakTextView.Get(); if (textView_ != null) textView_.EditorAction -= h; },
                (sender, args) => args.Handled = (args.ActionId == imeActionId) ? binder.ExecuteCommandIfCan() : false));
            return binder;
        }
    }
}
