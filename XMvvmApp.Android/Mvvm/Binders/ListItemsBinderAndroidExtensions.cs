using Android.Widget;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using XMvvmApp.Mvvm.Binders;
using XMvvmApp.Mvvm.Bindings;
using XMvvmApp.Utils;

namespace XMvvmApp.Android.Mvvm.Binders
{
    public static class ListItemsBinderAndroidExtensions
    {
        public static ListItemsBinder<T> BindToArrayAdapter<T>(this ListItemsBinder<T> binder,
            ArrayAdapter<T> adapter)
        {
            adapter.SetNotifyOnChange(false);
            adapter.AddAll(new List<T>(binder.List.GetItems()));
            adapter.NotifyDataSetChanged();

            var weakAdapter = new WeakReference<ArrayAdapter<T>>(adapter);
            binder.Bindings.Add(new ListCollectionChangedBinding<T>(binder.List, (sender, args) =>
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
			}));
            return binder;
        }
    }
}
