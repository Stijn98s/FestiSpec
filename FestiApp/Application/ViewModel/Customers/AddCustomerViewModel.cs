using AutoMapper;
using FestiApp.Util;
using FestiApp.View;
using FestiDB.Domain;

namespace FestiApp.ViewModel.Customers
{
    public class AddCustomerViewModel : GenericAddEntityViewModel<CustomerViewModel, Customer>
    {
        public AddCustomerViewModel(CustomerListViewModel userList, IMapper mapper, IFestiClient client) : base(userList, mapper, client)
        {

        }

        protected override bool CanAddEntity(IClosable window)
        {
            if (base.IsLoading) return false;
            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Name)) return false;
            if (!ValidationHelper.IsBetweenLength(45, 2, EntityViewModel.Name)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.KvK)) return false;
            if (!ValidationHelper.IsKVKNumber(EntityViewModel.KvK)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.PostalCode)) return false;
            if (!ValidationHelper.IsPostalCode(EntityViewModel.PostalCode)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.HouseNumber)) return false;
            if (!ValidationHelper.IsHouseNumber(EntityViewModel.HouseNumber)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.PhoneNumber)) return false;
            if (!ValidationHelper.IsPhoneNumber(EntityViewModel.PhoneNumber)) return false;
            return true;
        }
    }
}
