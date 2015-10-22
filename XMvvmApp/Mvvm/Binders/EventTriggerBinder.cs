using System;
using System.Linq.Expressions;
using XMvvmApp.Utils;

namespace XMvvmApp.Mvvm.Binders
{
    public class EventTriggerBinder<TEventArgs> : IBinder
    {
        private readonly Action<EventHandler<TEventArgs>> _addDelegate;
        private readonly Action<EventHandler<TEventArgs>> _removeDelegate;

        public BindingCollection Bindings { get; }

        protected Action<EventHandler<TEventArgs>> AddDelegate { get { return _addDelegate; } }
        protected Action<EventHandler<TEventArgs>> RemoveDelegate { get { return _removeDelegate; } }

        public EventTriggerBinder(Action<EventHandler<TEventArgs>> addDelegate, Action<EventHandler<TEventArgs>> removeDelegate)
        {
            _addDelegate = addDelegate;
            _removeDelegate = removeDelegate;

            this.Bindings = new BindingCollection();
        }

        public EventTriggerBinder<TEventArgs> BindToTargetProperty<V>(Expression<Func<V>> targetPropExp, IValueConverter<TEventArgs, V> valueConverter)
        {
            this.Bindings.Add(new EventHandlerBinding<TEventArgs>(_addDelegate, _removeDelegate, (sender, args) =>
            {
                // TODO: does / what if property expression hold strong ref to the property owner object??
                targetPropExp.SetPropertyValue(valueConverter.GetConvertedValue(args));
            }));
            return this;
        }
    }
}
