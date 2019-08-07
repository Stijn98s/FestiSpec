using AutoMapper;
using FestiApp.persistence;
using FestiApp.Util;
using FestiApp.View.Contacts;
using FestiApp.ViewModel.Customers;
using FestiDB.Domain;
using System.Linq;

namespace FestiApp.ViewModel.Contacts
{
    public class ContactListViewModel : ListViewModelBase<ContactViewModel, Contact>
    {
        private readonly IEditViewModel<CustomerViewModel> _editViewModel;

        public ContactListViewModel(IEditViewModel<CustomerViewModel> customer, FestiMSClient client, IMapper mapper) : base(client, mapper)
        {
            _editViewModel = customer;
            Refresh();
        }

        protected override async void Refresh()
        {
            var entities = await _client.GetSyncTable<Contact>().ToListAsync();
            ViewModels
                .CopyFrom(
                    entities.Where(elem => elem.CustomerId == _editViewModel.Entity.Id)
                        .Select(elem =>
                            new GenericEditEntityViewModel<ContactViewModel, Contact>(_client, _mapper, elem))
                        .ToList()
                );
        }

        protected override void EditSelectedViewModel()
        {
            var window = new EditContactPage();
            window.ShowDialog();
        }

        protected override void ShowAddEntity()
        {
            var window = new AddContactPage();
            window.ShowDialog();
            Refresh();
        }
    }
}
