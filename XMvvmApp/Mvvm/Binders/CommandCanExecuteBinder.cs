using System;
using System.Windows.Input;

namespace XMvvmApp.Mvvm.Binders
{
    public class CommandCanExecuteBinder : IBinder
    {
        public ICommand Command { get; }
        public Func<object> ParamDelegate { get; }

        public BindingCollection Bindings { get; }

        public CommandCanExecuteBinder(ICommand command)
        {
            this.Command = command;
            this.ParamDelegate = null;

            this.Bindings = new BindingCollection();
        }

        public CommandCanExecuteBinder(ICommand command, Func<object> paramDelegate)
        {
            this.Command = command;
            this.ParamDelegate = paramDelegate;

            this.Bindings = new BindingCollection();
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
