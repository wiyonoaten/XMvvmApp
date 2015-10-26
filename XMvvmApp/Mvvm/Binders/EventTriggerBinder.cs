using System;
namespace XMvvmApp.Mvvm.Binders
{
    public class EventTriggerBinder<TEventArgs> : IBinder
    {
        public Action<EventHandler<TEventArgs>> AddDelegate { get; }
        public Action<EventHandler<TEventArgs>> RemoveDelegate { get; }

        public BindingCollection Bindings { get; }

        public EventTriggerBinder(Action<EventHandler<TEventArgs>> addDelegate, Action<EventHandler<TEventArgs>> removeDelegate)
        {
            this.AddDelegate = addDelegate;
            this.RemoveDelegate = removeDelegate;

            this.Bindings = new BindingCollection();
        }
    }
}
