using System;
using System.Windows.Input;

namespace Xvvm.Mvvm.Binders
{
    public class CommandCanExecuteBinder<T> : IBinder
    {
        public ICommand Command { get; }
        public Func<T> ParamDelegate { get; }

        public BindingCollection Bindings { get; }

        public CommandCanExecuteBinder(ICommand command)
        {
            this.Command = command;
            this.ParamDelegate = null;

            this.Bindings = new BindingCollection();
        }

        public CommandCanExecuteBinder(ICommand command, Func<T> paramDelegate)
        {
            this.Command = command;
            this.ParamDelegate = paramDelegate;

            this.Bindings = new BindingCollection();
        }
    }

    public class CommandCanExecuteBinder : CommandCanExecuteBinder<object>
    {
        public CommandCanExecuteBinder(ICommand command)
            : base(command)
        {
        }

        public CommandCanExecuteBinder(ICommand command, Func<object> paramDelegate)
            : base(command, () => paramDelegate.Invoke())
        {
        }
    }
}
