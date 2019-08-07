using FestiDB.Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FestiApp.persistence
{
    internal class UserRepository : IUserRepository
    {
        private readonly FestiMSClient festiMSClient;

        public UserRepository(FestiMSClient festiMSClient)
        {
            this.festiMSClient = festiMSClient;
        }

        public async Task<ICollection<UserAccount>> GetUsersAsync()
        {
            return await festiMSClient.GetSyncTable<User>().Select(elem => elem.UserAccount).ToListAsync();
        }

        public async Task ChangePassword(string userId, string password)
        {
            try
            {
                await festiMSClient.InvokeApiAsync("Password/ChangePassword", JObject.FromObject(new ChangePasswordPoco { Password = password, UserId = userId }), System.Net.Http.HttpMethod.Post, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<ICollection<string>> GetEmailsAsync(string id)
        {
            return await festiMSClient.GetSyncTable<User>().Where(elem => elem.Id != id).Select(elem => elem.Email).ToListAsync();
        }

        public async Task<bool> EmailExists(string email)
        {
            var inspector = await festiMSClient.GetSyncTable<User>().Where(x => x.Email == email).Take(1).ToListAsync();
            return inspector.Count > 0;
        }

        public async Task<User> GetCurrentUser()
        {
            var current = festiMSClient.CurrentUser.UserId;
            var task = await festiMSClient.GetSyncTable<User>().Where(elem => elem.Email == current).ToListAsync();
            return task.FirstOrDefault();
        }
    }
}