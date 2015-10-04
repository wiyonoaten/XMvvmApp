using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XMvvmApp.Mvvm
{
    public class ObservableReadOnlyList<T> : ReadOnlyObservableCollection<T>
        , IObservableReadOnlyList<T>
    {
        public ObservableReadOnlyList(ObservableList<T> list)
            : base(list)
        {
        }

        public IReadOnlyList<T> GetItems()
        {
            return new List<T>(this.Items);
        }
    }
}
