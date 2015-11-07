using System;
using XMvvmApp.Platform;
using XMvvmApp.Utils;

namespace XMvvmApp.Mvvm.Helpers
{
    public class UiThreadHelper
    {
        private readonly IThreadScheduler _uiThreadScheduler;

        public UiThreadHelper(IThreadScheduler uiThreadScheduler)
        {
            _uiThreadScheduler = uiThreadScheduler;
        }

        public bool RunWeakly<T>(T self, Action<T> action)
            where T : class
        {
            return RunWeakly(self, TimeSpan.Zero, action);
        }

        public bool RunWeakly<T>(T self, TimeSpan delay, Action<T> action)
            where T : class
        {
            if (self == null || action == null)
            {
                return false;
            }

            var weakSelf = new WeakReference<T>(self);
            Action wrappedAction = () =>
            {
                T strongSelf = weakSelf.Get();
                if (strongSelf != null)
                {
                    action.Invoke(strongSelf);
                }
            };
            if (delay == TimeSpan.Zero)
            {
                return _uiThreadScheduler.Post(wrappedAction);
            }
            else
            {
                return _uiThreadScheduler.PostDelayed(wrappedAction, delay);
            }
        }
    }
}