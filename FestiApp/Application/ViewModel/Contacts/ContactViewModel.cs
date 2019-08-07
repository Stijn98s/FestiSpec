using FestiDB.Domain.Roles;
using GalaSoft.MvvmLight;

namespace FestiApp.ViewModel.Contacts
{
    public class ContactViewModel : ViewModelBase
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
        public string Note { get; set; } = "";
        public string PhoneNumber { get; set; } = "";

        public string CustomerId { get; set; }
    }
}
