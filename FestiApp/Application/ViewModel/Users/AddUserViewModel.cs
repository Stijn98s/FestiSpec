using AutoMapper;
using FestiApp.persistence;
using FestiApp.Util;
using FestiApp.View;
using FestiApp.View.User;
using FestiDB.Domain;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FestiApp.ViewModel.Users
{
    public class AddUserViewModel : GenericAddEntityViewModel<UserViewModel, User>
    {
        public ICommand ChangePasswordCommand { get; set; }
        private ICollection<string> Emails { get; set; }

        public AddUserViewModel(IUserRepository repo, UserListViewModel userList, IMapper mapper, IFestiClient client) : base(userList, mapper, client)
        {
            ChangePasswordCommand = new RelayCommand(ChangePassword);

            Task.Run(async () =>
            {
                Emails = await repo.GetEmailsAsync(EntityViewModel.Id);
            });
        }

        private void ChangePassword()
        {
            var window = new ChangePasswordWindow();
            window.Show();
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

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Email)) return false;
            if (!ValidationHelper.IsEmail(EntityViewModel.Email)) return false;

            if (!ValidationHelper.IsNotEmpty(EntityViewModel.Phone)) return false;
            if (!ValidationHelper.IsPhoneNumber(EntityViewModel.Phone)) return false;

            if (!ValidationHelper.IsEmpty(EntityViewModel.Role)) return false;

            if (Emails.Any(elem => elem == EntityViewModel.Email)) return false;

            return true;
        }
    }
}
