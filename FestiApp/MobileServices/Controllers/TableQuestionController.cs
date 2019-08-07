using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using FestiDB.Domain;
using FestiDB.Domain.Table;
using FestiMS.Models;

namespace FestiMS.Controllers
{
    public class TableQuestionController : TableController<TableQuestion>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<TableQuestion>(context, Request);
        }

        // GET tables/TableQuestion
        public IQueryable<TableQuestion> GetAllTableQuestion()
        {
            return Query(); 
        }

        // GET tables/TableQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<TableQuestion> GetTableQuestion(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TableQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TableQuestion> PatchTableQuestion(string id, Delta<TableQuestion> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/TableQuestion
        public async Task<IHttpActionResult> PostTableQuestion(TableQuestion item)
        {
            TableQuestion current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TableQuestion/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTableQuestion(string id)
        {
             return DeleteAsync(id);
        }
    }
}
