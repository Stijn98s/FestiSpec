using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FestiApp.persistence;
using FestiApp.ViewModel.Customers;
using FestiApp.ViewModel.Events;
using FestiDB.Domain;
using Microsoft.WindowsAzure.MobileServices;

namespace FestiApp.ViewModel.Contacts
{
    public class ContactEventViewModel
    {
        private readonly FestiMSClient _client;
        private readonly IMapper _mapper;

        public MobileServiceCollection<Contact, Contact> Contacts { get; set; }
        public Contact SelectedContact { get; set; }

        public ContactEventViewModel(FestiMSClient client, IMapper mapper, IEditViewModel<EventViewModel> e)
        {
            _client = client;
            _mapper = mapper;
            Contacts = client.Contacts.GetContact(e.EntityViewModel.CustomerId);
            Contacts.LoadMoreItemsAsync();
        }
    }
}