using System;
using System.Linq.Expressions;
using XMvvmApp.Mvvm.Bindings;
using XMvvmApp.Utils;

namespace XMvvmApp.Mvvm.Binders
{
    public static class EventTriggerBinderCoreExtensions
    {
        public static EventTriggerBinder<T> BindToDelegate<T>(this EventTriggerBinder<T> binder,
            Action<T> action)
        {
            binder.Bindings.Add(new EventHandlerBinding<T>(binder.AddDelegate, binder.RemoveDelegate, (sender, args) =>
            {
                action.Invoke(args);
            }));
            return binder;
        }

        public static EventTriggerBinder<T> BindToProperty<T, V>(this EventTriggerBinder<T> binder,
            Expression<Func<V>> targetPropExp, IValueConverter<T, V> valueConverter = null)
        {
            binder.Bindings.Add(new EventHandlerBinding<T>(binder.AddDelegate, binder.RemoveDelegate, (sender, args) =>
            {
                // TODO: does / what if property expression hold strong ref to the property owner object??
                targetPropExp.SetPropertyValue(valueConverter.GetTargetValue(args));
            }));
            return binder;
        }
    }
}
