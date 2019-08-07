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
    public class MultipleChoiceQuestionController : TableController<MultipleChoiceQuestion>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<MultipleChoiceQuestion>(context, Request);
        }

        // GET tables/MultipleChoiceQuestion
        public IQueryable<MultipleChoiceQuestion> GetAllMultipleChoiceQuestion()
        {
            return Query(); 
        }

        // GET tables/MultipleChoiceQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<MultipleChoiceQuestion> GetMultipleChoiceQuestion(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/MultipleChoiceQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<MultipleChoiceQuestion> PatchMultipleChoiceQuestion(string id, Delta<MultipleChoiceQuestion> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/MultipleChoiceQuestion
        public async Task<IHttpActionResult> PostMultipleChoiceQuestion(MultipleChoiceQuestion item)
        {
            MultipleChoiceQuestion current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/MultipleChoiceQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteMultipleChoiceQuestion(string id)
        {
             return DeleteAsync(id);
        }
    }
}
