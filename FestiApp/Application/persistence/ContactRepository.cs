using System;
using System.Threading.Tasks;
using AutoMapper;
using FestiDB.Domain;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace FestiApp.persistence
{
    public class ContactRepository : IContactRepository
    {

        private FestiMSClient _client;

        public ContactRepository(FestiMSClient client)
        {
            _client = client;
            Repo = _client.GetSyncTable<Contact>();
        }

        public async Task UpdateContact(Contact contact)
        {
            await Repo.UpdateAsync(contact);
            await _client.SyncAsync();
            await _client.GetSyncTable<Contact>().RefreshAsync(contact);
        }

        private IMobileServiceSyncTable<Contact> Repo { get; }

        public MobileServiceCollection<Contact> GetContact(string customerId)
        {
            return new MobileServiceCollection<Contact>(Repo.Where(elem => elem.CustomerId == customerId));
        }

        public async Task DeleteAsync(Contact selectedContact)
        {
            await Repo.DeleteAsync(selectedContact);
        }
    }
}