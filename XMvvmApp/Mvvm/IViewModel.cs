using System.ComponentModel;
using System.Windows.Input;

namespace XMvvmApp.Mvvm
{
    public interface IViewModel : INotifyPropertyChanging, INotifyPropertyChanged
    {
        PropertyChangedEventHandler PropertyChangedEventHandler { get; }
        PropertyChangingEventHandler PropertyChangingEventHandler { get; }

        object GetSavedState();
        void RestoreSavedState(object state);

        ICommand SleepCommand { get; }
        ICommand WakeupCommand { get; }
    }
}
