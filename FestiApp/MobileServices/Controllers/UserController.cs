using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using FestiDB.Domain;
using FestiDB.Persistence;
using FestiMS.Models;
using FestiMS.Manager;

namespace FestiMS.Controllers
{
    public class UserController : TableController<User>
    {
        private AuthManager _authManager;
        private readonly MobileServiceContext _festiContext;
        private MobileServiceContext context;

        public UserController(MobileServiceContext context, AuthManager authManager, MobileServiceContext festiContext)
        {
            this.context = context;
            this._authManager = authManager;
            _festiContext = festiContext;
        }
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
        
            DomainManager = new EntityDomainManager<User>(context, Request);
        }

        // GET tables/User
        public IQueryable<User> GetAllUser()
        {
            return Query();
        }

        // GET tables/User/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<User> GetUser(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/User/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<User> PatchUser(string id, Delta<User> patch)
        {
            var useraccount = await context.FestiUsers.Include(elem => elem.UserAccount).Where(elem => elem.Id == id)
                .Select(elem => elem.UserAccount).FirstOrDefaultAsync();

            object post;
            object housenumber;
            object Email;
            object PhoneNumber;
            object Role;

            patch.TrySetPropertyValue("UserAccount",useraccount);
            patch.TryGetPropertyValue("Role", out Role);
            if (useraccount != null)
            {
                if (patch.TryGetPropertyValue("PhoneNumber", out PhoneNumber))
                {
                    useraccount.PhoneNumber = PhoneNumber.ToString();
                }
                if (patch.TryGetPropertyValue("Email", out Email))
                {
                    patch.GetEntity().UserAccount.UserName = Email.ToString();
                }

                if (Role != null)
                {                    
                    var list = await _authManager.GetRolesAsync(useraccount.Id);
                    await _authManager.RemoveFromRolesAsync(useraccount.Id, list.ToArray());
                    await _authManager.AddToRoleAsync(useraccount.Id, Role.ToString());
                }
            }
            patch.TryGetPropertyValue("HouseNumber", out housenumber);

            patch.TryGetPropertyValue("PostalCode", out post);

            return await UpdateAsync(id, patch);
        }

        // POST tables/User
        public async Task<IHttpActionResult> PostUser(User item)
        {
            // role toevoegen
            User itemin = item;

            itemin.UserAccount = new UserAccount();

            itemin.UserAccount.UserName = item.Email;
            itemin.UserAccount.Email = item.Email;
            itemin.UserAccount.PhoneNumber = item.Phone;
            itemin.Email = item.Email;
            itemin.BirthDate = item.BirthDate;
            itemin.FirstName = item.FirstName;
            itemin.LastName = item.LastName;
            itemin.Phone = item.Phone;
            itemin.Gender = item.Gender;
            itemin.Role = item.Role;
            var result = await _authManager.CreateAsync(itemin.UserAccount, "password");
            string rol = item.Role.ToString();
            await _authManager.AddToRoleAsync(item.UserAccount.Id, rol);
            User current = await InsertAsync(itemin);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/User/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task DeleteUser(string id)
        {
            var useraccount = await _festiContext.Users.Where(el => el.User.Id == id).FirstOrDefaultAsync();
            _festiContext.Users.Remove(useraccount);
            await DeleteAsync(id);
        }
    }
}
