using System.ComponentModel;
using System.Windows.Input;
using XMvvmApp.Mvvm.Helpers;

namespace XMvvmApp.Mvvm
{
    public abstract class BasicViewModel : IViewModel
    {
        private readonly ViewModelStateHelper _viewModelStateHelper;

        protected BasicViewModel()
        {
            this.WakeupCommand = new DelegateCommand(DoWakeup);
            this.SleepCommand = new DelegateCommand(DoSleep);

            _viewModelStateHelper = new ViewModelStateHelper(this);
        }

        #region Property Changed/Changing Events

        public PropertyChangedEventHandler PropertyChangedEventHandler
        {
            get { return this.PropertyChanged; }
        }

        public PropertyChangingEventHandler PropertyChangingEventHandler
        {
            get { return this.PropertyChanging; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

        #region IViewModel Implementations

        public ICommand SleepCommand { get; }
        public ICommand WakeupCommand { get; }

        public virtual object GetSavedState()
        {
            return _viewModelStateHelper.GetState();
        }

        public virtual void RestoreSavedState(object state)
        {
            _viewModelStateHelper.RestoreState(state);
        }

        #endregion

        #region Abstracts

        protected abstract void DoWakeup();
        protected abstract void DoSleep();

        #endregion
    }
}
