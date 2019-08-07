using FestiDB.Domain;
using Microsoft.WindowsAzure.MobileServices;

namespace FestiApp.persistence
{
    public interface IContactRepository
    {
        MobileServiceCollection<Contact> GetContact(string customerId);
        System.Threading.Tasks.Task DeleteAsync(Contact selectedContact);
    }
}