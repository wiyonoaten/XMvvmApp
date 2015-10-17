using System;

namespace XMvvmApp.Mvvm.Binders
{
    public class EventHandlerBinding<TEventArgs> : Binding
    {
        private readonly Action<EventHandler<TEventArgs>> _removeDelegate;

        public EventHandlerBinding(
            Action<EventHandler<TEventArgs>> addDelegate, 
            Action<EventHandler<TEventArgs>> removeDelegate,
            EventHandler<TEventArgs> evHandler)
            : base(evHandler)
        {
            _removeDelegate = removeDelegate;

            addDelegate(evHandler);
        }

        public override void Detach()
        {
            base.Detach();

            _removeDelegate(base.Connection as EventHandler<TEventArgs>);
        }
    }
}
