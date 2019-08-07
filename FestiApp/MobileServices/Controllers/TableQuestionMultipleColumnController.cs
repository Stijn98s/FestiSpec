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
    public class TableQuestionMultipleColumnController : TableController<TableQuestionMultipleColumn>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<TableQuestionMultipleColumn>(context, Request);
        }

        // GET tables/TableQuestionMultipleColumn
        public IQueryable<TableQuestionMultipleColumn> GetAllTableQuestionMultipleColumn()
        {
            return Query(); 
        }

        // GET tables/TableQuestionMultipleColumn/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<TableQuestionMultipleColumn> GetTableQuestionMultipleColumn(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TableQuestionMultipleColumn/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TableQuestionMultipleColumn> PatchTableQuestionMultipleColumn(string id, Delta<TableQuestionMultipleColumn> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/TableQuestionMultipleColumn
        public async Task<IHttpActionResult> PostTableQuestionMultipleColumn(TableQuestionMultipleColumn item)
        {
            TableQuestionMultipleColumn current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TableQuestionMultipleColumn/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTableQuestionMultipleColumn(string id)
        {
             return DeleteAsync(id);
        }
    }
}
