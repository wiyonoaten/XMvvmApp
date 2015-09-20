using System;

namespace XMvvmApp.Platform
{
    public interface IThreadScheduler
    {
        bool Post(Action action);
        bool PostDelayed(Action action, TimeSpan delay);
    }
}
