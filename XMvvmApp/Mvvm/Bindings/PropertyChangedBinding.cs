using System;
using System.ComponentModel;
using System.Linq.Expressions;
using XMvvmApp.Utils;

namespace XMvvmApp.Mvvm.Bindings
{
    public class PropertyChangedBinding : Binding
    {
        public PropertyChangedBinding(object connection) : base(connection) { }

        public static PropertyChangedBinding<T> Create<T>(INotifyPropertyChanged inpc, Expression<Func<T>> propertyExp, Action<T> changedDelegate)
        {
            return new PropertyChangedBinding<T>(inpc, propertyExp, changedDelegate);
        }
    }

    public class PropertyChangedBinding<T> : PropertyChangedBinding
    {
        private readonly INotifyPropertyChanged _inpc;
        private readonly Expression<Func<T>> _propertyExp;

        public PropertyChangedBinding(INotifyPropertyChanged inpc, Expression<Func<T>> propertyExp, Action<T> changedDelegate)
            : base(changedDelegate)
        {
            _inpc = inpc;
            _propertyExp = propertyExp;

            _inpc.PropertyChanged += _PropertyChanged; ;
        }

        public override void Detach()
        {
            base.Detach();

            _inpc.PropertyChanged -= _PropertyChanged;
        }

        private void _PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(_propertyExp.GetPropertyName()))
            {
                var changedHandler = this.Connection as Action<T>;

                changedHandler.Invoke(_propertyExp.GetPropertyValue());
            }
        }
    }
}
