using FestiApp.Util.Util;
using FestiDB.Domain;
using FestiMS.Manager;
using FestiMS.Models;
using FestiMS.Util;
using Microsoft.Azure.Mobile.Server;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;

namespace FestiMS.Controllers
{

    public class InspectorController : TableController<Inspector>
    {
        private readonly AuthManager _authManager;
        private readonly EmailService _emailService;
        private readonly MobileServiceContext _context;
        private readonly GeodanHelperService _geodanHelper;


        public InspectorController(EmailService emailService, MobileServiceContext context,
            GeodanHelperService geodanHelper, AuthManager authManager)
        {
            _emailService = emailService;
            _context = context;
            _geodanHelper = geodanHelper;
            _authManager = authManager;
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            DomainManager = new EntityDomainManager<Inspector>(_context, Request);
        }

        // GET tables/Inspector

        public IQueryable<Inspector> GetAllInspector()
        {
            return Query();
        }

        // GET tables/Inspector/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Inspector> GetInspector(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Inspector/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<Inspector> PatchInspector(string id, Delta<Inspector> patch)
        {
            var useraccount = await _context.FestiUsers.Include(elem => elem.UserAccount).Where(elem => elem.Id == id)
                .Select(elem => elem.UserAccount).FirstOrDefaultAsync();

            if (useraccount != null)
            {
                if (patch.TryGetPropertyValue("PhoneNumber", out var phoneNumber))
                {
                    useraccount.PhoneNumber = phoneNumber.ToString();
                }

                if (patch.TryGetPropertyValue("Email", out var email))
                {
                    useraccount.UserName = email.ToString();
                }
            }

            var houseSucces = patch.TryGetPropertyValue("HouseNumber", out var housenumber);

            var postSuccess = patch.TryGetPropertyValue("PostalCode", out var post);

            if (_geodanHelper.Error == null && houseSucces && postSuccess)
            {
                var result = await _geodanHelper.GetDocByAdres(null, null, (string) housenumber, (string) post);


                patch.TrySetPropertyValue("GeodanAdresId", result?.id);
                patch.TrySetPropertyValue("Locx", result?.location?.X);
                patch.TrySetPropertyValue("Locy", result?.location?.Y);
            }



            return await UpdateAsync(id, patch);
        }

        // POST tables/Inspector
        public async Task<IHttpActionResult> PostInspector(Inspector item)
        {
            var exist = await _authManager.FindByEmailAsync(item.Email);
            if (exist != null)
            {
                Debug.WriteLine("User email Bestaat al");
                return null;
            }

            if (_geodanHelper.Error == null)
            {
                var result = await _geodanHelper.GetDocByAdres(null, null, item.HouseNumber, item.PostalCode);
                item.GeodanAdresId = result.id;
                item.Locx = result.location.X;
                item.Locy = result.location.Y;
            }
            else
            {
                item.GeodanAdresId = "InvalidID";

                item.Locx = 1.0;
                item.Locy = 1.0;
            }

            item.UserAccount = new UserAccount {UserName = item.Email, Email = item.Email, PhoneNumber = item.Phone};
            await _authManager.CreateAsync(item.UserAccount, "password");
            await _authManager.AddToRoleAsync(item.UserAccount.Id, "Inspector");
            Inspector current = await InsertAsync(item);
            await _emailService.SendChangePasswordMailAsync(item);
            return CreatedAtRoute("Tables", new {id = current.Id}, current);
        }

        // DELETE tables/Inspector/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task DeleteInspector(string id)
        {
            var useraccount = await _context.Users.Where(el => el.User.Id == id).FirstOrDefaultAsync();
            _context.Users.Remove(useraccount);
            await DeleteAsync(id);
        }
    }
}
