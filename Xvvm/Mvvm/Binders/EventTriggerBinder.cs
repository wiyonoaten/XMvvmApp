using System;
using System.Collections.Generic;

namespace Xvvm.Mvvm.Binders
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

    public class EventTriggerBinder : EventTriggerBinder<EventArgs>
    {
        private static readonly object HANDLERS_LOCK = new object();
        private static readonly IDictionary<EventHandler<EventArgs>, EventHandler> HANDLERS_MAP = new Dictionary<EventHandler<EventArgs>, EventHandler>();

        private static Action<EventHandler<EventArgs>> _GenericsizeAddDelegate(Action<EventHandler> addDelegate)
        {
            return h =>
            {
                lock(HANDLERS_LOCK)
                {
                    EventHandler h_ = (sender, args) => h.Invoke(sender, args);
                    HANDLERS_MAP[h] = h_;
                    addDelegate.Invoke(h_);
                }
            };
        }

        private static Action<EventHandler<EventArgs>> _GenericsizeRemoveDelegate(Action<EventHandler> removeDelegate)
        {
            return h =>
            {
                lock(HANDLERS_LOCK)
                {
                    var h_ = HANDLERS_MAP[h];
                    removeDelegate.Invoke(h_);
                    HANDLERS_MAP.Remove(h);
                }
            };
        }

        public EventTriggerBinder(Action<EventHandler> addDelegate, Action<EventHandler> removeDelegate)
            : base(_GenericsizeAddDelegate(addDelegate), _GenericsizeRemoveDelegate(removeDelegate))
        {
        }
    }
}
