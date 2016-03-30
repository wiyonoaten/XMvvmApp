using System;

namespace Xvvm.Mvvm.Bindings
{
    public class DelegateBinding<T> : Binding<T>
        where T : class
    {
        private readonly Action<T> _detachDelegate;

        public DelegateBinding(Action<T> attachDelegate, Action<T> detachDelegate, T connection)
            : base(connection)
        {
            _detachDelegate = detachDelegate;

            attachDelegate.Invoke(this.Connection);
        }

        public override void Detach()
        {
            _detachDelegate.Invoke(this.Connection);

            base.Detach();
        }
    }
}
