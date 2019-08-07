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
    public class DrawQuestionController : TableController<DrawQuestion>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<DrawQuestion>(context, Request);
        }

        // GET tables/DrawQuestion
        public IQueryable<DrawQuestion> GetAllDrawQuestion()
        {
            return Query(); 
        }

        // GET tables/DrawQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<DrawQuestion> GetDrawQuestion(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/DrawQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<DrawQuestion> PatchDrawQuestion(string id, Delta<DrawQuestion> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/DrawQuestion
        public async Task<IHttpActionResult> PostDrawQuestion(DrawQuestion item)
        {
            DrawQuestion current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/DrawQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteDrawQuestion(string id)
        {
             return DeleteAsync(id);
        }
    }
}
