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
    public class CategorieQuestionController : TableController<CategorieQuestion>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<CategorieQuestion>(context, Request);
        }

        // GET tables/CategorieQuestion
        public IQueryable<CategorieQuestion> GetAllCategorieQuestion()
        {
            return Query();
        }

        // GET tables/CategorieQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<CategorieQuestion> GetCategorieQuestion(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/CategorieQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<CategorieQuestion> PatchCategorieQuestion(string id, Delta<CategorieQuestion> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/CategorieQuestion
        public async Task<IHttpActionResult> PostCategorieQuestion(CategorieQuestion item)
        {
            CategorieQuestion current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/CategorieQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteCategorieQuestion(string id)
        {
            return DeleteAsync(id);
        }
    }
}
