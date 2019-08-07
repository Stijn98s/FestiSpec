using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using FestiDB.Domain;
using FestiMS.Models;

namespace FestiMS.Controllers
{
    public class AvailabilityController : TableController<Availability>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Availability>(context, Request);
        }

        // GET tables/Availability
        public IQueryable<Availability> GetAllAvailability()
        {
            return Query().Include(elem => elem.Inspector);
        }

        // GET tables/Availability/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Availability> GetAvailability(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Availability/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Availability> PatchAvailability(string id, Delta<Availability> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Availability
        public async Task<IHttpActionResult> PostAvailability(Availability item)
        {
            Availability current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Availability/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAvailability(string id)
        {
             return DeleteAsync(id);
        }
    }
}
