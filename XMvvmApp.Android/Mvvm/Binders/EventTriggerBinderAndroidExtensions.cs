using Android.App;
using Android.Content;
using System;
using XMvvmApp.Mvvm;
using XMvvmApp.Mvvm.Binders;
using XMvvmApp.Mvvm.Bindings;
using XMvvmApp.Utils;

namespace XMvvmApp.Android.Mvvm.Binders
{
    public static class EventTriggerBinderAndroidExtensions
    {
        public static EventTriggerBinder<T> BindToAlertDialogMessage<T>(this EventTriggerBinder<T> binder,
            Context context, string title, IValueConverter<T, string> valueConverter = null)
        {
            var weakContext = new WeakReference<Context>(context);
            binder.Bindings.Add(new EventHandlerBinding<T>(binder.AddDelegate, binder.RemoveDelegate, (sender, args) =>
            {
                var context_ = weakContext.Get();
                if (context_ != null)
                {
                    new AlertDialog.Builder(context_)
                        .SetTitle(title)
                        .SetMessage(valueConverter.GetStringValue(args))
                        .SetPositiveButton(global::Android.Resource.String.Ok, (IDialogInterfaceOnClickListener)null)
                        .Create()
                        .Show();
                }
            }));
            return binder;
        }
    }
}
