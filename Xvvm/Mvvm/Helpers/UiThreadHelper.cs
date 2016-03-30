using System;
using System.Linq.Expressions;
using Xvvm.Platform;
using Xvvm.Utils;

namespace Xvvm.Mvvm.Helpers
{
    public class UiThreadHelper
    {
        private readonly IThreadScheduler _uiThreadScheduler;

        public UiThreadHelper(IThreadScheduler uiThreadScheduler)
        {
            _uiThreadScheduler = uiThreadScheduler;
        }

        public bool RunWeakly<T>(T context, TimeSpan delay, Action<T> action)
            where T : class
        {
            if (context == null || action == null)
            {
                return false;
            }

            var weakContext = new WeakReference<T>(context);
            Action wrappedAction = () =>
            {
                T context_ = weakContext.Get();
                if (context_ != null)
                {
                    action.Invoke(context_);
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

        public bool RunWeakly<T>(T context, Action<T> action)
            where T : class
        {
            return RunWeakly(context, TimeSpan.Zero, action);
        }

        public void SetPropertyValue<TOwner, TProp>(TOwner owner, Expression<Func<TProp>> propertyExp, TProp value)
            where TOwner : class
        {
            var propertyInfo = propertyExp.GetPropertyInfo();
            RunWeakly(owner, (self) =>
            {
                propertyInfo.SetValue(self, value);
            });
        }

        public void TriggerEvent<TOwner>(TOwner owner, EventHandler evHandler)
            where TOwner : class
        {
            var weakEvHandler = new WeakReference<EventHandler>(evHandler);
            RunWeakly(owner, (self) =>
            {
                var evHandler_ = weakEvHandler.Get();
                evHandler_.TriggerSafely(owner);
            });
        }

        public void TriggerEvent<TOwner, TEventArgs>(TOwner owner, EventHandler<TEventArgs> evHandler, TEventArgs args)
            where TOwner : class
        {
            var weakEvHandler = new WeakReference<EventHandler<TEventArgs>>(evHandler);
            RunWeakly(owner, (self) =>
            {
                var evHandler_ = weakEvHandler.Get();
                evHandler_.TriggerSafely(owner, args);
            });
        }
    }
}