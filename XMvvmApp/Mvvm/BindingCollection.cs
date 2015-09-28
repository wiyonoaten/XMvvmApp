using System.Collections.Generic;

namespace XMvvmApp.Mvvm
{
    public class BindingCollection
    {
        private readonly List<Binding> _bindings;

        public BindingCollection()
        {
            _bindings = new List<Binding>();
        }

        public void Add(Binding binding)
        {
            _bindings.Add(binding);
        }

        public void AddAny<TSubscription>(TSubscription subscription)
            where TSubscription : class
        {
            AddAny(new Binding<TSubscription>(subscription));
        }

        public void AddAll(IEnumerable<Binding> bindings)
        {
            _bindings.AddRange(bindings);
        }

        public void AddAll(BindingCollection bindingCollection)
        {
            _bindings.AddRange(bindingCollection._bindings);
        }

        public void AddAll(IBinder binder)
        {
            AddAll(binder.Bindings);
        }

        public void DetachAll()
        {
            foreach (Binding binding in _bindings)
            {
                binding.Detach();
            }
        }
    }

}