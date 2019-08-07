using AutoMapper;
using FestiApp.persistence;
using FestiApp.Util;
using FestiApp.View;
using FestiDB.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FestiApp.ViewModel.Inspectors
{
    public class AddInspectorViewModel : GenericAddEntityViewModel<InspectorViewModel, Inspector>
    {
        private ICollection<string> Emails { get; set; }

        public AddInspectorViewModel(IUserRepository repo, InspectorListViewModel userList, IMapper mapper, IFestiClient client) : base(userList, mapper, client)
        {
            Task.Run(async () =>
            {
                Emails = await repo.GetEmailsAsync(EntityViewModel.Id);
            });
        }

        protected override bool CanAddEntity(IClosable window)
        {
            if (base.IsLoading) return false;
            if (!ValidationHelper.IsNotEmpty(EntityViewModel.FirstName)) return false;
            if (!ValidationHelper.IsCharacterOnly(EntityViewModel.FirstName)) return false;
            if (!ValidationHelper.IsBetweenLength(45, 2, EntityViewModel.FirstName)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.LastName)) return false;
            if (!ValidationHelper.IsCharacterOnly(EntityViewModel.LastName)) return false;
            if (!ValidationHelper.IsBetweenLength(45, 2, EntityViewModel.LastName)) return false;

            if (!ValidationHelper.IsEmpty(EntityViewModel.BirthDate)) return false;
            if (!ValidationHelper.IsPastDate(EntityViewModel.BirthDate)) return false;

            if (!ValidationHelper.IsEmpty(EntityViewModel.Gender)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.HouseNumber)) return false;
            if (!ValidationHelper.IsHouseNumber(EntityViewModel.HouseNumber)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Email)) return false;
            if (!ValidationHelper.IsEmail(EntityViewModel.Email)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.PostalCode)) return false;
            if (!ValidationHelper.IsPostalCode(EntityViewModel.PostalCode)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Phone)) return false;
            if (!ValidationHelper.IsPhoneNumber(EntityViewModel.Phone)) return false;

            if (Emails.Any(elem => elem == EntityViewModel.Email)) return false;

            return true;
        }
    }

}

