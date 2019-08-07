using Microsoft.AspNet.Identity.EntityFramework;

namespace FestiDB.Domain
{
    public class UserAccount : IdentityUser
    {
        public User User { get; set; }
    }
}