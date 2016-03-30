using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Xvvm.Utils;

namespace Xvvm.Mvvm
{
    public static class IViewModelExtensions
    {
        public static bool SetProperty<T>(this IViewModel vm, Expression<Func<T>> propertyExp, ref T field, T newValue)
        {
            return _SetProperty(vm, ref field, newValue, propertyExp.GetPropertyName());
        }

        public static bool SetProperty<T>(this IViewModel vm, ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(propertyName);
            }

            return _SetProperty(vm, ref field, newValue, propertyName);
        }

        private static bool _SetProperty<T>(IViewModel vm, ref T field, T newValue, string propertyName)
        {
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
