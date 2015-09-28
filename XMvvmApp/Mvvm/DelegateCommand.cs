using System;
using System.Windows.Input;

namespace XMvvmApp.Mvvm
{
    public interface ICommand<T> : ICommand
    {
        bool CanExecute(T parameter);
        void Execute(T parameter);
    }

    public class DelegateCommand<T> : ICommand<T>
    {
        private readonly Action<T> _executeAction;
        private readonly Func<T, bool> _canExecuteFunc;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> executeAction)
            : this(executeAction, null)
        {
        }

        public DelegateCommand(Action<T> executeAction, Func<T, bool> canExecuteFunc)
        {
            _executeAction = executeAction;
            _canExecuteFunc = canExecuteFunc;
        }

        #region ICommand Implementations

        public bool CanExecute(object parameter)
        {
            return _canExecuteFunc == null ? true : _canExecuteFunc.Invoke((T)parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction.Invoke((T)parameter);
        }

        #endregion

        #region ICommand<T> Implementations

        public bool CanExecute(T parameter)
        {
            return _canExecuteFunc == null ? true : _canExecuteFunc.Invoke(parameter);
        }

        public void Execute(T parameter)
        {
            _executeAction.Invoke(parameter);
        }

        #endregion

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }

    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action executeAction)
            : this(executeAction, null)
        {
        }

        public DelegateCommand(Action executeAction, Func<bool> canExecuteFunc)
            : base((_) => executeAction.Invoke(), (_) => canExecuteFunc.Invoke())
        {
        }
    }
}