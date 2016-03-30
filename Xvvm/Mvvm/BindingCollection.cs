using System.Collections.Generic;

namespace Xvvm.Mvvm
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
            foreach (var binding in _bindings)
            {
                binding.Detach();
            }
        }
    }

}