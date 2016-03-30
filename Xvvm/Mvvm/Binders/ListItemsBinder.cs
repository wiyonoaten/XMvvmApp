namespace Xvvm.Mvvm.Binders
{
    public class ListItemsBinder<T> : IBinder
    {
        public IObservableReadOnlyList<T> List { get; }

        public BindingCollection Bindings { get; }

        public ListItemsBinder(IObservableReadOnlyList<T> list)
        {
            this.List = list;

            this.Bindings = new BindingCollection();
        }
    }
}
