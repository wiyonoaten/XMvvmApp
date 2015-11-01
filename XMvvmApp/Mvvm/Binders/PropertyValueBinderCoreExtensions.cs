using System;
using System.ComponentModel;
using System.Linq.Expressions;
using XMvvmApp.Mvvm.Bindings;
using XMvvmApp.Utils;

namespace XMvvmApp.Mvvm.Binders
{
    public static class PropertyValueBinderCoreExtensions
    {
        public static PropertyValueBinder<T> BindToProperty<T, V>(this PropertyValueBinder<T> binder, 
            Expression<Func<V>> targetPropExp, IValueConverter<T, V> valueConverter = null)
        {
            targetPropExp.SetPropertyValue(valueConverter.GetTargetValue(binder.PropertyValue));

            binder.Bindings.Add(new PropertyChangedBinding<T>(binder.PropertyOwner, binder.PropertyExp, (newValue) =>
            {
                // TODO: does / what if property expression hold strong ref to the property owner object??
                targetPropExp.SetPropertyValue(valueConverter.GetTargetValue(newValue));
            }));
            return binder;
        }

        public static PropertyValueBinder<T> BindFromInpcProperty<T, V>(this PropertyValueBinder<T> binder, 
            Expression<Func<V>> sourcePropExp, IValueConverter<V, T> valueConverter = null)
        {
            binder.PropertyExp.SetPropertyValue(valueConverter.GetTargetValue(sourcePropExp.GetPropertyValue()));

            var sourcePropOwner = sourcePropExp.GetPropertyOwner<V, INotifyPropertyChanged>();
            binder.Bindings.Add(new PropertyChangedBinding<V>(sourcePropOwner, sourcePropExp, (newValue) =>
            {
                // TODO: does / what if property expression hold strong ref to the property owner object??
                binder.PropertyExp.SetPropertyValue(valueConverter.GetTargetValue(newValue));
            }));
            return binder;
        }
    }
}
