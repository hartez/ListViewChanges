using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ListViewChanges
{
    public class CustomObservableCollection<T> : ObservableCollection<T>
    {
        public void RemoveRange(int start, int end)
        {
            var removed = new List<T>(end - start);

            for (int n = end - 1; n >= start; n--)
            {
                removed.Insert(0, this[n]);
                Items.Remove(this[n]);
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed, start));
        }

        internal void AddRange(List<T> toAdd)
        {
            var startIndex = Items.Count;

            foreach (var item in toAdd)
            {
                Items.Add(item);
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, toAdd, startIndex));
        }

        internal void InsertRange(List<T> toInsert, int startIndex)
        {
            for(int n = 0; n < toInsert.Count; n++)
            {
                Items.Insert(startIndex + n, toInsert[n]);
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, toInsert, startIndex));
        }
    }
}
