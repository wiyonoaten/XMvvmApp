using System;

namespace Xvvm.Platform
{
    public interface IThreadScheduler
    {
        bool Post(Action action);
        bool PostDelayed(Action action, TimeSpan delay);
    }
}
