using AutoMapper;
using FestiApp.Util;
using FestiApp.View;
using FestiApp.ViewModel.Customers;
using FestiDB.Domain;
using Ninject;

namespace FestiApp.ViewModel.Contacts
{
    public class AddContactViewModel : GenericAddEntityViewModel<ContactViewModel, Contact>
    {

        public AddContactViewModel(IEditViewModel<CustomerViewModel> customer, ContactListViewModel contactList, IMapper mapper, IFestiClient client) : base(contactList, mapper, client)
        {
            EntityViewModel = new ContactViewModel()
            {
                CustomerId = customer.Entity.Id
            };
        }

        protected override bool CanAddEntity(IClosable window)
        {
            if (!ValidationHelper.IsNotEmpty(EntityViewModel.FirstName)) return false;
            if (!ValidationHelper.IsBetweenLength(45, 2, EntityViewModel.FirstName)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.LastName)) return false;
            if (!ValidationHelper.IsBetweenLength(45, 2, EntityViewModel.LastName)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Email)) return false;
            if (!ValidationHelper.IsEmail(EntityViewModel.Email)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.PhoneNumber)) return false;
            if (!ValidationHelper.IsPhoneNumber(EntityViewModel.PhoneNumber)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.LastName)) return false;
            if (!ValidationHelper.IsBetweenLength(50, 2, EntityViewModel.LastName)) return false;

            if (!ValidationHelper.IsBetweenLength(50, 0, EntityViewModel.Role)) return false;

            if (!ValidationHelper.IsBetweenLength(200, 0, EntityViewModel.Note)) return false;
            return true;
        }
    }
}
