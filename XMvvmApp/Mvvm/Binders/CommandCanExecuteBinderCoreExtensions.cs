using System;
using System.Linq.Expressions;
using XMvvmApp.Mvvm.Bindings;
using XMvvmApp.Utils;

namespace XMvvmApp.Mvvm.Binders
{
    public static class CommandCanExecuteBinderCoreExtensions
    {
        public static CommandCanExecuteBinder BindToProperty<V>(this CommandCanExecuteBinder binder,
            Expression<Func<V>> targetPropExp, IValueConverter<bool, V> valueConverter)
        {
            targetPropExp.SetPropertyValue(valueConverter.GetConvertedValue(binder.Command.CanExecute(binder.ParamDelegate?.Invoke())));

            binder.Bindings.Add(new EventHandlerBinding(h => binder.Command.CanExecuteChanged += h, h => binder.Command.CanExecuteChanged -= h, (sender, args) =>
            {
                // TODO: does / what if property expression hold strong ref to the property owner object??
                targetPropExp.SetPropertyValue(valueConverter.GetConvertedValue(binder.Command.CanExecute(binder.ParamDelegate?.Invoke())));
            }));
            return binder;
        }
    }
}
