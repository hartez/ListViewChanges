using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ListViewChanges
{
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
}
