using System;
using System.Linq.Expressions;

namespace Xvvm.Mvvm.Helpers
{
    public static class IUiThreadAwareViewModelExtensions
    {
        public static bool RunWeaklyOnUiThread<TViewModel>(this IUiThreadAwareViewModel<TViewModel> vm, 
            TimeSpan delay, Action<TViewModel> action)
            where TViewModel : class, IViewModel
        {
            return new UiThreadHelper(vm.UiThreadScheduler)
                .RunWeakly(vm as TViewModel, delay, action);
        }

        public static bool RunWeaklyOnUiThread<TViewModel>(this IUiThreadAwareViewModel<TViewModel> vm,
            Action<TViewModel> action)
            where TViewModel : class, IViewModel
        {
            return new UiThreadHelper(vm.UiThreadScheduler)
                .RunWeakly(vm as TViewModel, action);
        }

        public static void SetPropertyValueOnUiThread<TViewModel, TProp>(this IUiThreadAwareViewModel<TViewModel> vm,
            Expression<Func<TProp>> propertyExp, TProp value)
            where TViewModel : class, IViewModel
        {
            new UiThreadHelper(vm.UiThreadScheduler)
                .SetPropertyValue(vm as TViewModel, propertyExp, value);
        }

        public static void TriggerEventOnUiThread<TViewModel>(this IUiThreadAwareViewModel<TViewModel> vm,
            EventHandler evHandler)
            where TViewModel : class, IViewModel
        {
            new UiThreadHelper(vm.UiThreadScheduler)
                .TriggerEvent(vm as TViewModel, evHandler);
        }

        public static void TriggerEventOnUiThread<TViewModel, TEventArgs>(this IUiThreadAwareViewModel<TViewModel> vm,
            EventHandler<TEventArgs> evHandler, TEventArgs args)
            where TViewModel : class, IViewModel
        {
            new UiThreadHelper(vm.UiThreadScheduler)
                .TriggerEvent(vm as TViewModel, evHandler, args);
        }
    }
}
