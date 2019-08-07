using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using FestiApp.ViewModel;
using Microsoft.Maps.MapControl.WPF;

namespace FestiApp.View
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class DashboardPage : Page
    {
        public DashboardPage()
        {
            InitializeComponent();

            var mvm = (DashboardViewModel) DataContext;
            mvm.Locations.CollectionChanged += UpdateList;
            UpdateList(mvm.Locations, null);
        }

        private void UpdateList(object sender, NotifyCollectionChangedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                Mapmap.Children.Clear();
                foreach (var location in (ObservableCollection<Location>) sender)
                {
                    var pushPin = new Pushpin {Location = location};
                    Mapmap.Children.Add(pushPin);
                }
            }));
        }
    }
}