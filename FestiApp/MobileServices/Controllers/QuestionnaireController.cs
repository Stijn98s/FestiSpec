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
    public class QuestionnaireController : TableController<Questionnaire>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Questionnaire>(context, Request);
        }

        // GET tables/Questionnaire
        public IQueryable<Questionnaire> GetAllQuestionnaire()
        {
            return Query(); 
        }

        // GET tables/Questionnaire/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Questionnaire> GetQuestionnaire(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Questionnaire/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Questionnaire> PatchQuestionnaire(string id, Delta<Questionnaire> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Questionnaire
        public async Task<IHttpActionResult> PostQuestionnaire(Questionnaire item)
        {
            Questionnaire current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Questionnaire/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteQuestionnaire(string id)
        {
             return DeleteAsync(id);
        }
    }
}
