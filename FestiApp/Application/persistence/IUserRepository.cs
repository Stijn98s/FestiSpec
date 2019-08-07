using FestiDB.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FestiApp.persistence
{
    public interface IUserRepository
    {
        Task<ICollection<UserAccount>> GetUsersAsync();
        Task ChangePassword(string userId, string password);
        Task<ICollection<string>> GetEmailsAsync(string id);
        Task<bool> EmailExists(string email);
        Task<User> GetCurrentUser();
    }
}