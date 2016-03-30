using Xvvm.Platform;

namespace Xvvm.Mvvm.Helpers
{
    public interface IUiThreadAwareViewModel<TViewModel> : IViewModel
        where TViewModel : class, IViewModel
    {
        IThreadScheduler UiThreadScheduler { get; }
    }
}
