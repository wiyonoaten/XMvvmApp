using System;
using XMvvmApp.Platform;
using XMvvmApp.Utils;

namespace XMvvmApp.Mvvm.Helpers
{
    public class UiThreadSchedulerHelper
    {
        private readonly IThreadScheduler _mainThreadHandler;

        public UiThreadSchedulerHelper(IThreadScheduler mainThreadHandler)
        {
            _mainThreadHandler = mainThreadHandler;
        }

        public bool RunOnUiThreadWeakly<T>(T self, Action<T> action)
            where T : class
        {
            return RunOnUiThreadWeakly(self, TimeSpan.Zero, action);
        }

        public bool RunOnUiThreadWeakly<T>(T self, TimeSpan delay, Action<T> action)
            where T : class
        {
            if (self == null || action == null)
            {
                return false;
            }

            var weakSelf = new WeakReference<T>(self);
            var weakAction = new WeakReference<Action<T>>(action);
            Action wrappedAction = () =>
            {
                T strongSelf = weakSelf.Get();
                Action<T> strongAction = weakAction.Get();

                if (strongSelf != null && strongAction != null)
                {
                    strongAction.Invoke(strongSelf);
                }
            };
            if (delay == TimeSpan.Zero)
            {
                return _mainThreadHandler.Post(wrappedAction);
            }
            else
            {
                return _mainThreadHandler.PostDelayed(wrappedAction, delay);
            }
        }
    }
}