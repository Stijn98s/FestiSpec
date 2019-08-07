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
    public class ScaleQuestionAnswerController : TableController<ScaleQuestionAnswer>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<ScaleQuestionAnswer>(context, Request);
        }

        // GET tables/ScaleQuestionAnswer
        public IQueryable<ScaleQuestionAnswer> GetAllScaleQuestionAnswer()
        {
            return Query(); 
        }

        // GET tables/ScaleQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ScaleQuestionAnswer> GetScaleQuestionAnswer(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/ScaleQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<ScaleQuestionAnswer> PatchScaleQuestionAnswer(string id, Delta<ScaleQuestionAnswer> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/ScaleQuestionAnswer
        public async Task<IHttpActionResult> PostScaleQuestionAnswer(ScaleQuestionAnswer item)
        {
            ScaleQuestionAnswer current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/ScaleQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteScaleQuestionAnswer(string id)
        {
             return DeleteAsync(id);
        }
    }
}
