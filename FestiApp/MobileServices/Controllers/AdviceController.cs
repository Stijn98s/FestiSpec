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
    public class AdviceController : TableController<Advice>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Advice>(context, Request);
        }

        // GET tables/Advice
        public IQueryable<Advice> GetAllAdvice()
        {
            return Query(); 
        }

        // GET tables/Advice/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Advice> GetAdvice(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Advice/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Advice> PatchAdvice(string id, Delta<Advice> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Advice
        public async Task<IHttpActionResult> PostAdvice(Advice item)
        {
            Advice current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Advice/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAdvice(string id)
        {
             return DeleteAsync(id);
        }
    }
}
