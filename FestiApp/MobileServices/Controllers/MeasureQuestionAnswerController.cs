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
    public class MeasureQuestionAnswerController : TableController<MeasureQuestionAnswer>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<MeasureQuestionAnswer>(context, Request);
        }

        // GET tables/MeasureQuestionAnswer
        public IQueryable<MeasureQuestionAnswer> GetAllMeasureQuestionAnswer()
        {
            return Query(); 
        }

        // GET tables/MeasureQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<MeasureQuestionAnswer> GetMeasureQuestionAnswer(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/MeasureQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<MeasureQuestionAnswer> PatchMeasureQuestionAnswer(string id, Delta<MeasureQuestionAnswer> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/MeasureQuestionAnswer
        public async Task<IHttpActionResult> PostMeasureQuestionAnswer(MeasureQuestionAnswer item)
        {
            MeasureQuestionAnswer current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/MeasureQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteMeasureQuestionAnswer(string id)
        {
             return DeleteAsync(id);
        }
    }
}
