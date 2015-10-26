using System;
using System.ComponentModel;
using System.Linq.Expressions;
using XMvvmApp.Utils;

namespace XMvvmApp.Mvvm.Binders
{
    public class PropertyValueBinder<T> : IBinder
    {
        public Expression<Func<T>> PropertyExp { get; }
        
        public BindingCollection Bindings { get; }

        public PropertyValueBinder(Expression<Func<T>> propertyExp)
        {
            this.PropertyExp = propertyExp;

            this.Bindings = new BindingCollection();
        }

        public INotifyPropertyChanged PropertyOwner
        {
            get { return this.PropertyExp.GetPropertyOwner<T, INotifyPropertyChanged>(); }
        }

        public T PropertyValue
        {
            get { return this.PropertyExp.GetPropertyValue(); }
        }
    }
}
