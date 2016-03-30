using System;
using System.Windows.Input;

namespace Xvvm.Mvvm.Binders
{
    public class CommandExecuteBinder<T> : IBinder
    {
        public ICommand Command { get; }
        public Func<T> ParamDelegate { get; }

        public BindingCollection Bindings { get; }

        public CommandExecuteBinder(ICommand command)
        {
            this.Command = command;
            this.ParamDelegate = null;

            this.Bindings = new BindingCollection();
        }

        public CommandExecuteBinder(ICommand command, Func<T> paramDelegate)
        {
            this.Command = command;
            this.ParamDelegate = paramDelegate;

            this.Bindings = new BindingCollection();
        }
    }

    public class CommandExecuteBinder : CommandExecuteBinder<object>
    {
        public CommandExecuteBinder(ICommand command)
            : base(command)
        {
        }

        public CommandExecuteBinder(ICommand command, Func<object> paramDelegate)
            : base(command, () => paramDelegate.Invoke())
        {
        }
    }
}
