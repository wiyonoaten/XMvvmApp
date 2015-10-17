using System;
using System.Linq.Expressions;
using XMvvmApp.Mvvm.Bindings;
using XMvvmApp.Utils;

namespace XMvvmApp.Mvvm.Binders
{
    public class PropertyValueBinder<T> : IBinder
    {
        private readonly Expression<Func<T>> _propertyExp;
        private readonly string _propertyName;
        
        public BindingCollection Bindings { get; }

        public PropertyValueBinder(Expression<Func<T>> propertyExp)
        {
            _propertyExp = propertyExp;
            _propertyName = propertyExp.GetPropertyName();

            this.Bindings = new BindingCollection();
        }

        protected IViewModel GetPropertyOwner()
        {
            return _propertyExp.GetPropertyOwner<T, IViewModel>();
        }

        protected T GetPropertyValue()
        {
            return _propertyExp.GetPropertyValue();
        }

        public PropertyValueBinder<T> BindToTargetProperty<V>(Expression<Func<V>> targetPropExp, IValueConverter<T, V> valueConverter)
        {
            targetPropExp.SetPropertyValue(valueConverter.GetConvertedValue(GetPropertyValue()));

            this.Bindings.Add(new ViewModelPropertyChangedBinding(GetPropertyOwner(), (sender, args) =>
            {
                // TODO: does / what if property expression hold strong ref to the property owner object??
                targetPropExp.SetPropertyValue(valueConverter.GetConvertedValue(GetPropertyValue()));
            }));
            return this;
        }
    }
}
