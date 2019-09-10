using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.Specialized;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ListViewChanges
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CustomObservableCollection<Item> _data;

        public MainPage()
        {
            this.InitializeComponent();

            _data = new CustomObservableCollection<Item>();

            for (int n = 0; n < 10; n++)
            {
                _data.Add(new Item { Name = $"Item {n}" });
            }

            TheListView.ItemsSource = _data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _data.RemoveRange(0, 5);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var toAdd = new List<Item>()
            {
                new Item { Name = "Added 1" },
                new Item { Name = "Added 2" },
                new Item { Name = "Added 3" }
            };

            _data.AddRange(toAdd);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var toInsert = new List<Item>()
            {
                new Item { Name = "Inserted 1" },
                new Item { Name = "Inserted 2" },
                new Item { Name = "Inserted 3" }
            };

            _data.InsertRange(toInsert, 3);
        }
    }

    public class Item
    {
        public string Name { get; set; }
    }

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
