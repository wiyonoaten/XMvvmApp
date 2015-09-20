using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace XMvvmApp.Mvvm
{
    // Adapted from following SO: http://stackoverflow.com/a/13303245
    public class ObservableList<T> : ObservableCollection<T>
    {
        public ObservableList()
            : base()
        {
        }

        public ObservableList(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public ObservableList(List<T> list)
            : base(list)
        {
        }

        public IReadOnlyList<T> GetItems()
        {
            return new List<T>(this.Items);
        }

        public void AddRange(IEnumerable<T> range)
        {
            if (range == null)
            {
                throw new ArgumentNullException(nameof(range));
            }

            _AddRange(range);

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, range));
        }

        public void Reset(IEnumerable<T> range)
        {
            this.Items.Clear();

            if (range != null)
            {
                _AddRange(range);
            }

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void _AddRange(IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                Items.Add(item);
            }

            this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
            this.OnPropertyChanged(new PropertyChangedEventArgs("[]"));
        }
    }
}
