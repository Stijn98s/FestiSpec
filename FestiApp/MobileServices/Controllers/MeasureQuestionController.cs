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
    public class MeasureQuestionController : TableController<MeasureQuestion>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<MeasureQuestion>(context, Request);
        }

        // GET tables/MeasureQuestion
        public IQueryable<MeasureQuestion> GetAllMeasureQuestion()
        {
            return Query(); 
        }

        // GET tables/MeasureQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<MeasureQuestion> GetMeasureQuestion(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/MeasureQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<MeasureQuestion> PatchMeasureQuestion(string id, Delta<MeasureQuestion> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/MeasureQuestion
        public async Task<IHttpActionResult> PostMeasureQuestion(MeasureQuestion item)
        {
            MeasureQuestion current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/MeasureQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteMeasureQuestion(string id)
        {
             return DeleteAsync(id);
        }
    }
}
