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
    public class OpenQuestionAnswerController : TableController<OpenQuestionAnswer>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<OpenQuestionAnswer>(context, Request);
        }

        // GET tables/OpenQuestionAnswer
        public IQueryable<OpenQuestionAnswer> GetAllOpenQuestionAnswer()
        {
            return Query(); 
        }

        // GET tables/OpenQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<OpenQuestionAnswer> GetOpenQuestionAnswer(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/OpenQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<OpenQuestionAnswer> PatchOpenQuestionAnswer(string id, Delta<OpenQuestionAnswer> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/OpenQuestionAnswer
        public async Task<IHttpActionResult> PostOpenQuestionAnswer(OpenQuestionAnswer item)
        {
            OpenQuestionAnswer current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/OpenQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteOpenQuestionAnswer(string id)
        {
             return DeleteAsync(id);
        }
    }
}
