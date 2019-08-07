using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using FestiDB.Domain.Table;
using FestiMS.Models;

namespace FestiMS.Controllers
{
    public class TableQuestionStringColumnController : TableController<TableQuestionStringColumn>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<TableQuestionStringColumn>(context, Request);
        }

        // GET tables/TableQuestionStringColumn
        public IQueryable<TableQuestionStringColumn> GetAllTableQuestionStringColumn()
        {
            return Query(); 
        }

        // GET tables/TableQuestionStringColumn/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<TableQuestionStringColumn> GetTableQuestionStringColumn(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TableQuestionStringColumn/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TableQuestionStringColumn> PatchTableQuestionStringColumn(string id, Delta<TableQuestionStringColumn> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/TableQuestionStringColumn
        public async Task<IHttpActionResult> PostTableQuestionStringColumn(TableQuestionStringColumn item)
        {
            TableQuestionStringColumn current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TableQuestionStringColumn/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTableQuestionStringColumn(string id)
        {
             return DeleteAsync(id);
        }
    }
}
