using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using XMvvmApp.Mvvm.Bindings;

namespace XMvvmApp.Mvvm.Binders
{
    public class ListItemsBinder<T> : IBinder
    {
        private readonly IObservableReadOnlyList<T> _list;

        public BindingCollection Bindings { get; }

        protected IObservableReadOnlyList<T> List { get { return _list; } }

        public ListItemsBinder(IObservableReadOnlyList<T> list)
        {
            _list = list;

            this.Bindings = new BindingCollection();
        }

        public ListItemsBinder<T> BindToTargetList(List<T> targetList)
        {
            targetList.AddRange(_list);

            this.Bindings.Add(new ListCollectionChangedBinding<T>(_list, (sender, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (T item in args.NewItems)
                        {
                            targetList.Add(item);
                        }
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        foreach (T item in args.OldItems)
                        {
                            targetList.Remove(item);
                        }
                        break;

                    case NotifyCollectionChangedAction.Replace:
                        foreach (T item in args.OldItems)
                        {
                            targetList.Remove(item);
                        }
                        for (int i = 0; i < args.NewItems.Count; i++)
                        {
                            targetList.Insert(args.OldStartingIndex + i, (T)args.NewItems[i]);
                        }
                        break;

                    case NotifyCollectionChangedAction.Move:
                        // TODO:
                        throw new NotImplementedException("case " + nameof(NotifyCollectionChangedAction.Move) + " not implemented yet!.");

                    case NotifyCollectionChangedAction.Reset:
                        targetList.Clear();
                        break;
                }
			}));

            return this;
        }
    }
}
