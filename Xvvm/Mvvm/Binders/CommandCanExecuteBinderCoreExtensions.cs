using System;
using System.Linq.Expressions;
using Xvvm.Mvvm.Bindings;
using Xvvm.Utils;

namespace Xvvm.Mvvm.Binders
{
    public static class CommandCanExecuteBinderCoreExtensions
    {
        public static CommandCanExecuteBinder<T> BindToProperty<T, V>(this CommandCanExecuteBinder<T> binder,
            Expression<Func<V>> targetPropExp, IValueConverter<bool, V> valueConverter = null)
        {
            var param = binder.ParamDelegate != null ? binder.ParamDelegate.Invoke() : default(T);
            targetPropExp.SetPropertyValue(valueConverter.GetTargetValue(binder.Command.CanExecute(param)));

            binder.Bindings.Add(new EventHandlerBinding(h => binder.Command.CanExecuteChanged += h, h => binder.Command.CanExecuteChanged -= h, (sender, args) =>
            {
                var param_ = binder.ParamDelegate != null ? binder.ParamDelegate.Invoke() : default(T);
                // TODO: does / what if property expression hold strong ref to the property owner object??
                targetPropExp.SetPropertyValue(valueConverter.GetTargetValue(binder.Command.CanExecute(param_)));
            }));
            return binder;
        }
    }
}
