using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using FestiDB.Domain;
using FestiDB.Domain.Answers;
using FestiMS.Models;

namespace FestiMS.Controllers
{
    public class PictureQuestionAnswerController : TableController<PictureQuestionAnswer>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<PictureQuestionAnswer>(context, Request);
        }

        // GET tables/PictureQuestionAnswer
        public IQueryable<PictureQuestionAnswer> GetAllPictureQuestionAnswer()
        {
            return Query(); 
        }

        // GET tables/PictureQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<PictureQuestionAnswer> GetPictureQuestionAnswer(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/PictureQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<PictureQuestionAnswer> PatchPictureQuestionAnswer(string id, Delta<PictureQuestionAnswer> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/PictureQuestionAnswer
        public async Task<IHttpActionResult> PostPictureQuestionAnswer(PictureQuestionAnswer item)
        {
            PictureQuestionAnswer current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/PictureQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeletePictureQuestionAnswer(string id)
        {
             return DeleteAsync(id);
        }
    }
}
