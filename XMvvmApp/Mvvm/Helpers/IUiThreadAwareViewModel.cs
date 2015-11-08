using XMvvmApp.Platform;

namespace XMvvmApp.Mvvm.Helpers
{
    public interface IUiThreadAwareViewModel<TViewModel> : IViewModel
        where TViewModel : class, IViewModel
    {
        IThreadScheduler UiThreadScheduler { get; }
    }
}
