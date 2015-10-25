using System;

namespace XMvvmApp.Mvvm.Bindings
{
    public class EventHandlerBinding : Binding
    {
        private readonly Action<EventHandler> _removeDelegate;

        public EventHandlerBinding(
            Action<EventHandler> addDelegate,
            Action<EventHandler> removeDelegate,
            EventHandler evHandler)
            : base(evHandler)
        {
            _removeDelegate = removeDelegate;

            addDelegate(evHandler);
        }

        public override void Detach()
        {
            base.Detach();

            _removeDelegate(this.Connection as EventHandler);
        }
    }

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

            _removeDelegate(this.Connection as EventHandler<TEventArgs>);
        }
    }
}
