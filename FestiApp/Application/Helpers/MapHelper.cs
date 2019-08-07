using Microsoft.Maps.MapControl.WPF;
using System.Windows;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace FestiApp.Helpers
{

    public static class MapHelper
    {
        public static readonly DependencyProperty CenterProperty = DependencyProperty.RegisterAttached(
            "Center",
            typeof(Location),
            typeof(MapHelper),
            new PropertyMetadata(new Location(51.690090, 5.303690), CenterChanged)
        );

        public static void SetCenter(DependencyObject obj, Location value)
        {
            obj.SetValue(CenterProperty, value);
        }

        public static Location GetCenter(DependencyObject obj)
        {
            return (Location)obj.GetValue(CenterProperty);
        }

        private static void CenterChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var map = (Map)obj;

            if (map != null)
            {
                map.Center = (Location) args.NewValue;
            }
        }
    }
}
