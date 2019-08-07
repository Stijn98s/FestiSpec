using GalaSoft.MvvmLight;
using System;

namespace FestiApp.ViewModel.Events
{
    public class AvailabilityViewModel : ViewModelBase
    {
        public string EventId { get; set; }
        public string InspectorId { get; set; }
        public int EventDay { get; set; } = 1;

        public bool HasResponded { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime StartAvailability { get; set; }
        public DateTime EndAvailability { get; set; }

    }
}