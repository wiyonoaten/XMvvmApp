using System.Collections.Specialized;

namespace XMvvmApp.Mvvm.Bindings
{
    public class ListCollectionChangedBinding<T> : Binding<NotifyCollectionChangedEventHandler>
    {
        private readonly IObservableReadOnlyList<T> _list;

        public ListCollectionChangedBinding(IObservableReadOnlyList<T> list, NotifyCollectionChangedEventHandler evHandler)
            : base(evHandler)
        {
            _list = list;

            _list.CollectionChanged += evHandler;
        }

        public override void Detach()
        {
            _list.CollectionChanged -= this.Connection;

            base.Detach();
        }
    }
}
