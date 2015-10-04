using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace XMvvmApp.Mvvm
{
    // Adapted from following SO: http://stackoverflow.com/a/13303245
    // Note: not a thread-safe implementation.
    public class ObservableList<T> : ObservableCollection<T>, IObservableList<T>
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

        public void AddRange(List<T> range)
        {
            if (range == null)
            {
                throw new ArgumentNullException(nameof(range));
            }

            _AddRange(range);

			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, range));
        }

        public void ResetTo(List<T> newItems)
        {
            if (newItems == null)
            {
                throw new ArgumentNullException(nameof(newItems));
            }

            var oldItems = new List<T>(this.Items);

            this.Items.Clear();
           _AddRange(newItems);
            
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItems, oldItems, 0));
        }

        private void _AddRange(List<T> range)
        {
            foreach (var item in range)
            {
                this.Items.Add(item);
            }

            this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Count)));
            this.OnPropertyChanged(new PropertyChangedEventArgs("[]"));
        }
    }
}