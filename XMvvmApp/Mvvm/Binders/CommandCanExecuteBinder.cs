using System;
using System.Linq.Expressions;
using System.Windows.Input;
using XMvvmApp.Mvvm.Bindings;
using XMvvmApp.Utils;

namespace XMvvmApp.Mvvm.Binders
{
    public class CommandCanExecuteBinder : IBinder
    {
        private readonly ICommand _command;
        private readonly Func<object> _paramDelegate;

        public BindingCollection Bindings { get; }

        public CommandCanExecuteBinder(ICommand command)
        {
            _command = command;
            _paramDelegate = null;

            this.Bindings = new BindingCollection();
        }

        public CommandCanExecuteBinder(ICommand command, Func<object> paramDelegate)
        {
            _command = command;
            _paramDelegate = paramDelegate;

            this.Bindings = new BindingCollection();
        }

        public CommandCanExecuteBinder BindToTargetProperty<V>(Expression<Func<V>> targetPropExp, IValueConverter<bool, V> valueConverter)
        {
            targetPropExp.SetPropertyValue(valueConverter.GetConvertedValue(_command.CanExecute(_paramDelegate?.Invoke())));

            this.Bindings.Add(new EventHandlerBinding(h => _command.CanExecuteChanged += h, h => _command.CanExecuteChanged -= h, (sender, args) =>
            {
                // TODO: does / what if property expression hold strong ref to the property owner object??
                targetPropExp.SetPropertyValue(valueConverter.GetConvertedValue(_command.CanExecute(_paramDelegate?.Invoke())));
            }));
            return this;
        }
    }

    public class CommandCanExecuteBinder<T> : CommandCanExecuteBinder
    {
        public CommandCanExecuteBinder(ICommand<T> command, Func<T> paramDelegate)
            : base(command, () => paramDelegate.Invoke())
        {
        }
    }
}
