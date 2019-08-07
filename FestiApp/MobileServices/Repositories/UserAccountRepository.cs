using System.Collections.Generic;
using System.Linq;
using FestiDB.Domain;

namespace FestiMS.Repositories
{
    public class UserAccountRepository
    {
        public List<UserAccount> TestUsers;

        public UserAccountRepository()
        {
            TestUsers = new List<UserAccount>();

        }

        public UserAccount GetUser(string username)
        {
            try
            {
                return TestUsers.First(user => user.UserName.Equals(username));
            }
            catch
            {
                return null;
            }
        }
    }
}