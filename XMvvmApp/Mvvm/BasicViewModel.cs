using System.ComponentModel;
using System.Windows.Input;

namespace XMvvmApp.Mvvm
{
    public abstract class BasicViewModel : IViewModel
    {
        private readonly DelegateCommand _wakeupCommand;
        private readonly DelegateCommand _sleepCommand;

        protected BasicViewModel()
        {
            _wakeupCommand = new DelegateCommand(DoWakeup);
            _sleepCommand = new DelegateCommand(DoSleep);
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

        #region IViewModel Abstracts

        public abstract object GetSavedState();
        public abstract void RestoreSavedState(object state);

        #endregion

        #region IViewModel Implementations

        public ICommand SleepCommand { get { return _sleepCommand; } }

        public ICommand WakeupCommand { get { return _wakeupCommand; } }

        #endregion

        #region Abstracts

        protected abstract void DoWakeup();
        protected abstract void DoSleep();

        #endregion
    }
}
