using System;
using System.Linq.Expressions;
using XMvvmApp.Mvvm.Bindings;
using XMvvmApp.Utils;

namespace XMvvmApp.Mvvm.Binders
{
    public class PropertyValueBinder<T> : IBinder
    {
        protected Expression<Func<T>> PropertyExp { get; }
        
        public BindingCollection Bindings { get; }

        public PropertyValueBinder(Expression<Func<T>> propertyExp)
        {
            this.PropertyExp = propertyExp;

            this.Bindings = new BindingCollection();
        }

        protected IViewModel GetPropertyOwner()
        {
            return this.PropertyExp.GetPropertyOwner<T, IViewModel>();
        }

        protected T GetPropertyValue()
        {
            return this.PropertyExp.GetPropertyValue();
        }

        public PropertyValueBinder<T> BindToTargetProperty<V>(Expression<Func<V>> targetPropExp, IValueConverter<T, V> valueConverter)
        {
            targetPropExp.SetPropertyValue(valueConverter.GetConvertedValue(GetPropertyValue()));

            this.Bindings.Add(new PropertyChangedBinding<T>(GetPropertyOwner(), this.PropertyExp, (newValue) =>
            {
                // TODO: does / what if property expression hold strong ref to the property owner object??
                targetPropExp.SetPropertyValue(valueConverter.GetConvertedValue(newValue));
            }));
            return this;
        }
    }
}
