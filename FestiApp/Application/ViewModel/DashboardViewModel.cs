using FestiApp.persistence;
using FestiDB.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace FestiApp.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        public ObservableCollection<Location> Locations { get; set; }

        public List<Event> Events { get; set; }
        private double _zoom = 7;
        private Location _curLocation;
        public ICommand SetPinsCommand { get; set; }

        public DashboardViewModel(IEventRepository repository)
        {

            CurrentLocation = new Location(51.690090, 5.303690);
            Locations = new ObservableCollection<Location>();

            SetPinsCommand = new RelayCommand<Event>(PinSelected);

            Task.Run(async () =>
            {
                Events = await repository.GetUpcommingEvents();
                RaisePropertyChanged("Events");
                AddLocationAsync();
            });
        }

        private void PinSelected(Event inspectionEvent)
        {
            var y = Convert.ToDouble(inspectionEvent.GeodanAdresY);
            var x = Convert.ToDouble(inspectionEvent.GeodanAdresX);

            if (y > 10000)
            {
                y = Convert.ToDouble(inspectionEvent.GeodanAdresY, CultureInfo.InvariantCulture);
                x = Convert.ToDouble(inspectionEvent.GeodanAdresX, CultureInfo.InvariantCulture);
            }

            CurrentLocation = new Location(y, x);
            Zoom = 12;
        }

        private void AddLocationAsync()
        {
            if (Events == null)
            {
                return;
            }

            foreach (var e in Events)
            {
                if (e.GeodanAdresX == null)
                {
                    e.GeodanAdresX = "51.690090";
                    e.GeodanAdresY = "5.303690";
                }

                var y = Convert.ToDouble(e.GeodanAdresY);
                var x = Convert.ToDouble(e.GeodanAdresX);

                if (y > 10000)
                {
                    y = Convert.ToDouble(e.GeodanAdresY, CultureInfo.InvariantCulture);
                    x = Convert.ToDouble(e.GeodanAdresX, CultureInfo.InvariantCulture);
                }
                Zoom = 10;
                CurrentLocation = new Location(y, x);
                Locations.Add(CurrentLocation);
            }
        }

        public Location CurrentLocation
        {
            get => _curLocation;
            set
            {
                _curLocation = value;
                RaisePropertyChanged();
            }
        }

        public double Zoom
        {
            get => _zoom;
            set
            {
                _zoom = value;
                RaisePropertyChanged("Zoom");
            }
        }
    }
}

