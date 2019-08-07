using FestiDB.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using FestiDB.Domain.Roles;

namespace FestiApp.ViewModel.Users
{
    public class UserViewModel : ViewModelBase
    {
        public UserViewModel(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            BirthDate = user.BirthDate;
            Gender = user.Gender;
            Email = user.Email;
            Phone = user.Phone;
        }

        public UserViewModel()
        {

        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; } = DateTime.Now;
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IEnumerable<Gender> Genders => Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
        public Role Role { get; set; } = Role.Operational;
        public IEnumerable<Role> UserRoles => Enum.GetValues(typeof(Role)).Cast<Role>().Where(elem => elem != Role.Inspector).ToList();

    }
}