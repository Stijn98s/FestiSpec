using Microsoft.AspNet.Identity.EntityFramework;

namespace FestiDB.Domain
{
    public class AppRole : IdentityRole
    {
        public AppRole()
        {

        }

        public AppRole(string name) : base(name)
        {

        }
    }
}