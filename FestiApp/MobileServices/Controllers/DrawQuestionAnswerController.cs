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
    public class DrawQuestionAnswerController : TableController<DrawQuestionAnswer>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<DrawQuestionAnswer>(context, Request);
        }

        // GET tables/DrawQuestionAnswer
        public IQueryable<DrawQuestionAnswer> GetAllDrawQuestionAnswer()
        {
            return Query(); 
        }

        // GET tables/DrawQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<DrawQuestionAnswer> GetDrawQuestionAnswer(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/DrawQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<DrawQuestionAnswer> PatchDrawQuestionAnswer(string id, Delta<DrawQuestionAnswer> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/DrawQuestionAnswer
        public async Task<IHttpActionResult> PostDrawQuestionAnswer(DrawQuestionAnswer item)
        {
            DrawQuestionAnswer current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/DrawQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteDrawQuestionAnswer(string id)
        {
             return DeleteAsync(id);
        }
    }
}
