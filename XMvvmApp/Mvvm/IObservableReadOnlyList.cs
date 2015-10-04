using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace XMvvmApp.Mvvm
{
    // Mimics ReadOnlyObservableCollection in terms of interfaces.
    public interface IObservableReadOnlyList<T> 
        : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
        , INotifyCollectionChanged, INotifyPropertyChanged
    {
        bool Contains(T value);
        void CopyTo(T[] array, int index);
        int IndexOf(T value);

        IReadOnlyList<T> GetItems();
    }
}