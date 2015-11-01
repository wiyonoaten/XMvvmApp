using Android.Views;
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
        private static bool _ExecuteCommandIfCan<T>(CommandExecuteBinder<T> binder)
        {
            var param = binder.ParamDelegate != null 
                ? binder.ParamDelegate.Invoke()
                : default(T);

            if (false == binder.Command.CanExecute(param))
            {
                return false;
            }
            binder.Command.Execute(param);
            return true;
        }

        private static bool _ExecuteCommandWithArgsIfCan<T, TArgs>(CommandExecuteBinder<T> binder, 
            TArgs args, IValueConverter<TArgs, T> valueConverter)
        {
            var param = binder.ParamDelegate != null
                ? binder.ParamDelegate.Invoke()
                : valueConverter.GetTargetValue(args);

            if (false == binder.Command.CanExecute(param))
            {
                return false;
            }
            binder.Command.Execute(param);
            return true;
        }

        public static CommandExecuteBinder<T> BindFromViewClick<T>(this CommandExecuteBinder<T> binder,
            View view)
        {
            var weakView = new WeakReference<View>(view);
            binder.Bindings.Add(new EventHandlerBinding(
                h => { var view_ = weakView.Get(); if (view_ != null) view_.Click += h; },
                h => { var view_ = weakView.Get(); if (view_ != null) view_.Click -= h; },
                (sender, args) => _ExecuteCommandIfCan(binder)));
            return binder;
        }

        public static CommandExecuteBinder<T> BindFromMenuItemClick<T>(this CommandExecuteBinder<T> binder,
            IMenuItem menuItem, IValueConverter<IMenuItem, T> valueConverter = null)
        {
            var weakMenuItem = new WeakReference<IMenuItem>(menuItem);
            binder.Bindings.Add(new DelegateBinding<IMenuItemOnMenuItemClickListener>(
                l => { var menuItem_ = weakMenuItem.Get(); if (menuItem_ != null) menuItem_.SetOnMenuItemClickListener(l); },
                l => { var menuItem_ = weakMenuItem.Get(); if (menuItem_ != null) menuItem_.SetOnMenuItemClickListener(null); },
                new DelegateMenuItemOnMenuItemClickListener(args => _ExecuteCommandWithArgsIfCan(binder, args, valueConverter))));
            return binder;
        }

        public static CommandExecuteBinder<T> BindFromListViewItemClick<T>(this CommandExecuteBinder<T> binder,
            ListView listView, IValueConverter<AdapterView.ItemClickEventArgs, T> valueConverter = null)
        {
            var weakListView = new WeakReference<ListView>(listView);
            binder.Bindings.Add(new EventHandlerBinding<AdapterView.ItemClickEventArgs>(
                h => { var listView_ = weakListView.Get(); if (listView_ != null) listView_.ItemClick += h; },
                h => { var listView_ = weakListView.Get(); if (listView_ != null) listView_.ItemClick -= h; },
                (sender, args) => _ExecuteCommandWithArgsIfCan(binder, args, valueConverter)));
            return binder;
        }
    }
}
