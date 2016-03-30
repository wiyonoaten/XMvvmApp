using System.Collections;
using System.Collections.Generic;

namespace Xvvm.Mvvm
{
    // Mimics ObservableCollection in terms of interfaces, while also is an IObservableReadOnlyList. 
    public interface IObservableList<T> : IObservableReadOnlyList<T>
        , IList<T>, IList, ICollection<T>, ICollection
    {
        void Move(int oldIndex, int newIndex);

        void AddRange(List<T> range);
        void ResetTo(List<T> newItems);
    }
}