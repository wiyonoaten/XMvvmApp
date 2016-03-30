using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Xvvm.Mvvm
{
    public class ObservableReadOnlyList<T> : ReadOnlyObservableCollection<T>
        , IObservableReadOnlyList<T>
    {
        public ObservableReadOnlyList(ObservableList<T> list)
            : base(list)
        {
        }

        public ObservableReadOnlyList(IEnumerable<T> collection)
            : base(new ObservableCollection<T>(collection))
        {
        }

        public ObservableReadOnlyList(List<T> list)
            : base(new ObservableCollection<T>(list))
        {
        }

        public IReadOnlyList<T> GetItems()
        {
            return new List<T>(this.Items);
        }
    }
}
