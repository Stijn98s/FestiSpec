using System.Collections.Generic;
using System.Threading.Tasks;
using FestiDB.Domain;

namespace FestiApp.persistence
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomer(string id);
        Task DeleteAsync(Customer selectedCustomer);
        Task<ICollection<Customer>> GetCustomers();
    }
}