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
    public class OpenQuestionController : TableController<OpenQuestion>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<OpenQuestion>(context, Request);
        }

        // GET tables/OpenQuestion
        public IQueryable<OpenQuestion> GetAllOpenQuestion()
        {
            return Query(); 
        }

        // GET tables/OpenQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<OpenQuestion> GetOpenQuestion(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/OpenQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<OpenQuestion> PatchOpenQuestion(string id, Delta<OpenQuestion> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/OpenQuestion
        public async Task<IHttpActionResult> PostOpenQuestion(OpenQuestion item)
        {
            OpenQuestion current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/OpenQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteOpenQuestion(string id)
        {
             return DeleteAsync(id);
        }
    }
}
