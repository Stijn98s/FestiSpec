using FestiDB.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FestiApp.ViewModel.Inspectors
{
    public class InspectorViewModel : ViewModelBase
    {
        private readonly Inspector elem;

        public InspectorViewModel(Inspector elem)
        {
            this.elem = elem;
        }

        public InspectorViewModel()
        {

        }

        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PostalCode { get; set; }
        public string HouseNumber { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; } = DateTime.Now;
        public Gender Gender { get; set; }
        public ICollection<Questionnaire> Questionnaires { get; set; }


        public IEnumerable<Gender> Genders => Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
    }
}