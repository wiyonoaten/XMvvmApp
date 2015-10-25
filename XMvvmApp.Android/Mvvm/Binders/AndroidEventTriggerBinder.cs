using Android.App;
using Android.Content;
using System;
using XMvvmApp.Mvvm;
using XMvvmApp.Mvvm.Binders;
using XMvvmApp.Mvvm.Bindings;
using XMvvmApp.Utils;

namespace XMvvmApp.Android.Mvvm.Binders
{
    public class AndroidEventTriggerBinder<TEventArgs> : EventTriggerBinder<TEventArgs>
    {
        public AndroidEventTriggerBinder(Action<EventHandler<TEventArgs>> addDelegate, Action<EventHandler<TEventArgs>> removeDelegate)
            : base(addDelegate, removeDelegate)
        {
        }

        public AndroidEventTriggerBinder<TEventArgs> BindToAlertDialogMessage(Context context, string title, IValueConverter<TEventArgs, string> valueConverter)
        {
            var weakContext = new WeakReference<Context>(context);
            this.Bindings.Add(new EventHandlerBinding<TEventArgs>(this.AddDelegate, this.RemoveDelegate, (sender, args) =>
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
            return this;
        }
    }
}
