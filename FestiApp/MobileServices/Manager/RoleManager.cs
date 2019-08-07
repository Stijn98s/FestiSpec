using FestiDB.Domain;
using FestiDB.Domain.Answers;
using FestiMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FestiMS.Manager
{
    public class RoleManager : RoleManager<AppRole>
    {
        public RoleManager(MobileServiceContext context) : base(new RoleStore<AppRole>(context))
        {
            
        }
    }
}