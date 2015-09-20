using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using XMvvmApp.Utils;

namespace XMvvmApp.Mvvm
{
    public static class IViewModelExtensions
    {
        public static bool SetProperty<T>(this IViewModel vm, Expression<Func<T>> propertyExp, ref T field, T newValue)
        {
            if (Equals(field, newValue))
            {
                return false;
            }

            _RaisePropertyChanging(vm, propertyExp.GetPropertyName());
            field = newValue;
            _RaisePropertyChanged(vm, propertyExp.GetPropertyName());
            return true;
        }

        public static bool SetProperty<T>(this IViewModel vm, ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(propertyName);
            }

            if (Equals(field, newValue))
            {
                return false;
            }

            _RaisePropertyChanging(vm, propertyName);
            field = newValue;
            _RaisePropertyChanged(vm, propertyName);
            return true;
        }

        private static void _RaisePropertyChanging(IViewModel vm, string propertyName)
        {
            if (vm.PropertyChangingEventHandler != null)
            {
                vm.PropertyChangingEventHandler(vm, new PropertyChangingEventArgs(propertyName));
            }
        }

        private static void _RaisePropertyChanged(IViewModel vm, string propertyName)
        {
            if (vm.PropertyChangedEventHandler != null)
            {
                vm.PropertyChangedEventHandler(vm, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
