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
    public class MultipleChoiceQuestionOptionController : TableController<MultipleChoiceQuestionOption>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<MultipleChoiceQuestionOption>(context, Request);
        }

        // GET tables/MultipleChoiceQuestionOption
        public IQueryable<MultipleChoiceQuestionOption> GetAllMultipleChoiceQuestionOption()
        {
            return Query(); 
        }

        // GET tables/MultipleChoiceQuestionOption/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<MultipleChoiceQuestionOption> GetMultipleChoiceQuestionOption(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/MultipleChoiceQuestionOption/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<MultipleChoiceQuestionOption> PatchMultipleChoiceQuestionOption(string id, Delta<MultipleChoiceQuestionOption> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/MultipleChoiceQuestionOption
        public async Task<IHttpActionResult> PostMultipleChoiceQuestionOption(MultipleChoiceQuestionOption item)
        {
            MultipleChoiceQuestionOption current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/MultipleChoiceQuestionOption/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteMultipleChoiceQuestionOption(string id)
        {
             return DeleteAsync(id);
        }
    }
}
