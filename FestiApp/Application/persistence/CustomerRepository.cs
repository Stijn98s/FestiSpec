using System.Collections.Generic;
using FestiDB.Domain;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;

namespace FestiApp.persistence
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FestiMSClient _festiMsClient;
        
        public CustomerRepository(FestiMSClient festiMsClient)
        {
  
            _festiMsClient = festiMsClient;
        }

        public async Task<Customer> GetCustomer(string id)
        {
            return await _festiMsClient.GetSyncTable<Customer>().LookupAsync(id);
        }

        public async Task DeleteAsync(Customer selectedCustomer)
        {
            await _festiMsClient.GetSyncTable<Customer>().DeleteAsync(selectedCustomer);
        }

        public async Task<ICollection<Customer>> GetCustomers()
        {
           return  await _festiMsClient.GetSyncTable<Customer>().ToListAsync();
        }
    }
}