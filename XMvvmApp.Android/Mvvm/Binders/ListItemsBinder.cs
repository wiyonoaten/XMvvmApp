using Android.Widget;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using XMvvmApp.Mvvm;
using XMvvmApp.Utils;

namespace XMvvmApp.Android.Mvvm.Binders
{
    public class ListItemsBinder<T> : IBinder
    {
        protected readonly ObservableList<T> _list;

        public BindingCollection Bindings { get; }

        public ListItemsBinder(ObservableList<T> list)
        {
            _list = list;

            Bindings = new BindingCollection();
        }

        public ListItemsBinder<T> BindToArrayAdapter(ArrayAdapter<T> adapter)
        {
            adapter.SetNotifyOnChange(false);
            adapter.AddAll(new List<T>(_list.GetItems()));
            adapter.NotifyDataSetChanged();

            var weakAdapter = new WeakReference<ArrayAdapter<T>>(adapter);

            NotifyCollectionChangedEventHandler evHandler = (sender, args) =>
            {
                var adapter_ = weakAdapter.Get();
                if (adapter_ == null)
                {
                    return;
                }

                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        adapter_.SetNotifyOnChange(false);
                        adapter_.AddAll(args.NewItems);
                        adapter_.NotifyDataSetChanged();
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        adapter_.SetNotifyOnChange(false);
                        foreach (T item in args.OldItems)
                        {
                            adapter_.Remove(item);
                        }
                        adapter_.NotifyDataSetChanged();
                        break;

                    case NotifyCollectionChangedAction.Replace:
                        adapter_.SetNotifyOnChange(false);
                        foreach (T item in args.OldItems)
                        {
                            adapter_.Remove(item);
                        }
                        for (int i = 0; i < args.NewItems.Count; i++)
                        {
                            adapter_.Insert((T)args.NewItems[i], args.OldStartingIndex + i);
                        }
                        adapter_.NotifyDataSetChanged();
                        break;

                    case NotifyCollectionChangedAction.Move:
                        // TODO:
                        throw new NotImplementedException("case " + nameof(NotifyCollectionChangedAction.Move) + " not implemented yet!.");

                    case NotifyCollectionChangedAction.Reset:
                        adapter_.Clear();
                        adapter_.NotifyDataSetChanged();
                        break;
                }
            };
            _list.CollectionChanged += evHandler;

            Bindings.Add(new ListCollectionChangedBinding(_list, evHandler));

            return this;
        }

        private class ListCollectionChangedBinding : Binding
        {
            private readonly ObservableList<T> _list;

            public ListCollectionChangedBinding(ObservableList<T> list, NotifyCollectionChangedEventHandler evHandler)
                : base(evHandler)
            {
                _list = list;

                _list.CollectionChanged += evHandler;
            }

            public override void Detach()
            {
                base.Detach();

                _list.CollectionChanged -= base.Connection as NotifyCollectionChangedEventHandler;
            }
        }
    }
}