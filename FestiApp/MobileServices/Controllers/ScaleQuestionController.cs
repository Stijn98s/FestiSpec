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
    public class ScaleQuestionController : TableController<ScaleQuestion>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<ScaleQuestion>(context, Request);
        }

        // GET tables/ScaleQuestion
        public IQueryable<ScaleQuestion> GetAllScaleQuestion()
        {
            return Query(); 
        }

        // GET tables/ScaleQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ScaleQuestion> GetScaleQuestion(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/ScaleQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<ScaleQuestion> PatchScaleQuestion(string id, Delta<ScaleQuestion> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/ScaleQuestion
        public async Task<IHttpActionResult> PostScaleQuestion(ScaleQuestion item)
        {
            ScaleQuestion current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/ScaleQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteScaleQuestion(string id)
        {
             return DeleteAsync(id);
        }
    }
}
