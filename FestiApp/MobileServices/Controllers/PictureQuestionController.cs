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
    public class PictureQuestionController : TableController<PictureQuestion>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<PictureQuestion>(context, Request);
        }

        // GET tables/PictureQuestion
        public IQueryable<PictureQuestion> GetAllPictureQuestion()
        {
            return Query(); 
        }

        // GET tables/PictureQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<PictureQuestion> GetPictureQuestion(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/PictureQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<PictureQuestion> PatchPictureQuestion(string id, Delta<PictureQuestion> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/PictureQuestion
        public async Task<IHttpActionResult> PostPictureQuestion(PictureQuestion item)
        {
            PictureQuestion current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/PictureQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeletePictureQuestion(string id)
        {
             return DeleteAsync(id);
        }
    }
}
