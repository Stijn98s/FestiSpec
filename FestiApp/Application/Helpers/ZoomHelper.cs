using Microsoft.Maps.MapControl.WPF;
using System.Windows;

namespace FestiApp.Helpers
{
    public static class ZoomHelper
    {
        public static readonly DependencyProperty ZoomProperty = DependencyProperty.RegisterAttached(
            "Zoom",
            typeof(double),
            typeof(ZoomHelper),
            new FrameworkPropertyMetadata(7.0, ZoomChanged)
        );

        public static void SetZoom(DependencyObject obj, double value)
        {
            obj.SetValue(ZoomProperty, value);
        }

        public static double GetZoom(DependencyObject obj)
        {
            return (double)obj.GetValue(ZoomProperty);
        }

        private static void ZoomChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var map = (Map)obj;

            if (map != null)
            {
                map.ZoomLevel = (double) args.NewValue;
            }
        }
    }
}