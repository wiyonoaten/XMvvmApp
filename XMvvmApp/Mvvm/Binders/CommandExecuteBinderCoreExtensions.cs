using System;
using XMvvmApp.Mvvm.Bindings;

namespace XMvvmApp.Mvvm.Binders
{
    public static class CommandExecuteBinderCoreExtensions
    {
        // If the binder object was created with ParamDelegate, then the command will be executed using that delegate value,
        // otherwise the EventArgs (converted if valueConverter is provided) from the raised event will be used instead.
        public static CommandExecuteBinder<T> BindFromEvent<T>(this CommandExecuteBinder<T> binder,
            Action<EventHandler> evAddDelegate, Action<EventHandler> evRemoveDelegate, 
            IValueConverter<EventArgs, T> valueConverter = null)
        {
            binder.Bindings.Add(new EventHandlerBinding(evAddDelegate, evRemoveDelegate, (sender, args) =>
            {
                binder.Command.Execute(binder.ParamDelegate != null
                    ? binder.ParamDelegate.Invoke()
                    : valueConverter.GetTargetValue(args));
            }));
            return binder;
        }

        // If the binder object was created with ParamDelegate, then the command will be executed using that delegate value,
        // otherwise the T args (converted if valueConverter is provided) from the raised event will be used instead.
        public static CommandExecuteBinder<T> BindFromEvent<T, TEventArgs>(this CommandExecuteBinder<T> binder,
            Action<EventHandler<TEventArgs>> evAddDelegate, Action<EventHandler<TEventArgs>> evRemoveDelegate, 
            IValueConverter<TEventArgs, T> valueConverter = null)
        {
            binder.Bindings.Add(new EventHandlerBinding<TEventArgs>(evAddDelegate, evRemoveDelegate, (sender, args) =>
            {
                binder.Command.Execute(binder.ParamDelegate != null 
                    ? binder.ParamDelegate.Invoke() 
                    : valueConverter.GetTargetValue(args));
            }));
            return binder;
        }
    }
}
