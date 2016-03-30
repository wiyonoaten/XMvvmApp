using System;

namespace Xvvm.Mvvm.Bindings
{
    public class EventHandlerBinding : DelegateBinding<EventHandler>
    {
        public EventHandlerBinding(
            Action<EventHandler> addDelegate,
            Action<EventHandler> removeDelegate,
            EventHandler evHandler)
            : base(addDelegate, removeDelegate, evHandler)
        {
        }
    }

    public class EventHandlerBinding<TEventArgs> : DelegateBinding<EventHandler<TEventArgs>>
    {
        public EventHandlerBinding(
            Action<EventHandler<TEventArgs>> addDelegate, 
            Action<EventHandler<TEventArgs>> removeDelegate,
            EventHandler<TEventArgs> evHandler)
            : base(addDelegate, removeDelegate, evHandler)
        {
        }
    }
}
