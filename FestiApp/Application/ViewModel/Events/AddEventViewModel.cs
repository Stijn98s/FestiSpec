using AutoMapper;
using FestiApp.persistence;
using FestiApp.Util;
using FestiApp.View;
using FestiApp.ViewModel.Customers;
using FestiApp.ViewModel.Validation;
using FestiDB.Domain;

namespace FestiApp.ViewModel.Events
{
    public class AddEventViewModel : GenericAddEntityViewModel<EventViewModel, Event>
    {
        public GenericEditEntityViewModel<CustomerViewModel, Customer> SelectedCustomerViewModel { get; set; }

        public AddEventViewModel(EventListViewModel list, IMapper mapper, FestiMSClient client) : base(list, mapper, client)
        {
            IsSubsequentDateValidationRule.DateTime = EntityViewModel.StartDate;
        }

        public override void AddEntity(IClosable window)
        {
            EntityViewModel.CustomerId = SelectedCustomerViewModel?.Entity.Id;
            base.AddEntity(window);
        }

        protected override bool CanAddEntity(IClosable window)
        {
            IsSubsequentDateValidationRule.DateTime = EntityViewModel.StartDate;

            if (base.IsLoading) return false;
            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Name)) return false;
            if (!ValidationHelper.IsBetweenLength(45, 2, EntityViewModel.Name)) return false;

            if (!ValidationHelper.IsEmpty(EntityViewModel.StartDate)) return false;

            if (!ValidationHelper.IsEmpty(EntityViewModel.EndDate)) return false;
            if (!ValidationHelper.IsSubsequentDate(EntityViewModel.StartDate, EntityViewModel.EndDate)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Location)) return false;
            if (!ValidationHelper.IsBetweenLength(50, 2, EntityViewModel.Location)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.PostalCode)) return false;
            if (!ValidationHelper.IsPostalCode(EntityViewModel.PostalCode)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.HouseNumber)) return false;
            if (!ValidationHelper.IsHouseNumber(EntityViewModel.HouseNumber)) return false;
            if (SelectedCustomerViewModel == null) return false;
            return true;
        }
    }
}
