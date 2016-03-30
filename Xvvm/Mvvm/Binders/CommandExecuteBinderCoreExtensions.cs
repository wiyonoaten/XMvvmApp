using System;
using Xvvm.Mvvm.Bindings;

namespace Xvvm.Mvvm.Binders
{
    public static class CommandExecuteBinderCoreExtensions
    {
        public static bool ExecuteCommandIfCan<T>(this CommandExecuteBinder<T> binder)
        {
            var param = binder.ParamDelegate != null
                ? binder.ParamDelegate.Invoke()
                : default(T);

            if (false == binder.Command.CanExecute(param))
            {
                return false;
            }
            binder.Command.Execute(param);
            return true;
        }

        public static bool ExecuteCommandWithArgsIfCan<T, TArgs>(this CommandExecuteBinder<T> binder,
            TArgs args, IValueConverter<TArgs, T> valueConverter)
        {
            var param = binder.ParamDelegate != null
                ? binder.ParamDelegate.Invoke()
                : valueConverter.GetTargetValue(args);

            if (false == binder.Command.CanExecute(param))
            {
                return false;
            }
            binder.Command.Execute(param);
            return true;
        }

        public static CommandExecuteBinder<T> BindFromEvent<T>(this CommandExecuteBinder<T> binder,
            Action<EventHandler> evAddDelegate, Action<EventHandler> evRemoveDelegate)
        {
            binder.Bindings.Add(new EventHandlerBinding(evAddDelegate, evRemoveDelegate, (sender, args) =>
            {
                binder.ExecuteCommandIfCan();
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
                binder.ExecuteCommandWithArgsIfCan(args, valueConverter);
            }));
            return binder;
        }
    }
}
