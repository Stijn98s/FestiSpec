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
    public class MultipleChoiceQuestionAnswerController : TableController<MultipleChoiceQuestionAnswer>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<MultipleChoiceQuestionAnswer>(context, Request);
        }

        // GET tables/MultipleChoiceQuestionAnswer
        public IQueryable<MultipleChoiceQuestionAnswer> GetAllMultipleChoiceQuestionAnswer()
        {
            return Query(); 
        }

        // GET tables/MultipleChoiceQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<MultipleChoiceQuestionAnswer> GetMultipleChoiceQuestionAnswer(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/MultipleChoiceQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<MultipleChoiceQuestionAnswer> PatchMultipleChoiceQuestionAnswer(string id, Delta<MultipleChoiceQuestionAnswer> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/MultipleChoiceQuestionAnswer
        public async Task<IHttpActionResult> PostMultipleChoiceQuestionAnswer(MultipleChoiceQuestionAnswer item)
        {
            MultipleChoiceQuestionAnswer current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/MultipleChoiceQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteMultipleChoiceQuestionAnswer(string id)
        {
             return DeleteAsync(id);
        }
    }
}
