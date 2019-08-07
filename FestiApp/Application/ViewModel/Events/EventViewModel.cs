using FestiApp.ViewModel.Advice;
using FestiDB.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;

namespace FestiApp.ViewModel.Events
{
    public class EventViewModel : ViewModelBase
    {

        protected internal Event EventModel { get; }

        public EventViewModel(Event eventModel)
        {
            EventModel = eventModel;
        }

        public EventViewModel()
        {

        }

        public string Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today;
        public string Location { get; set; } = "";
        public string Description { get; set; } = "";

        public List<Availability> Availabilities { get; set; }
        public List<AdviceViewModel> Advices { get; set; }
        public string PostalCode { get; set; } = "";
        public string HouseNumber { get; set; } = "";
        public string CustomerId { get; set; } = "";
    }
}