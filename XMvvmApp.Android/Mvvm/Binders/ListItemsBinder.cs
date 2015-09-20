using Android.Widget;
using System.Collections.Generic;
using XMvvmApp.Mvvm;

namespace XMvvmApp.Android.Mvvm.Binders
{
    public class ListItemsBinder<T> : IBinder
    {
        protected readonly ObservableList<T> _list;

        public BindingCollection Bindings { get; private set; }

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

            

            return this;
        }
    }
}