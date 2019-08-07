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
    public class TableQuestionNumberColumnController : TableController<TableQuestionNumberColumn>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<TableQuestionNumberColumn>(context, Request);
        }

        // GET tables/TableQuestionNumberColumn
        public IQueryable<TableQuestionNumberColumn> GetAllTableQuestionNumberColumn()
        {
            return Query(); 
        }

        // GET tables/TableQuestionNumberColumn/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<TableQuestionNumberColumn> GetTableQuestionNumberColumn(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TableQuestionNumberColumn/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TableQuestionNumberColumn> PatchTableQuestionNumberColumn(string id, Delta<TableQuestionNumberColumn> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/TableQuestionNumberColumn
        public async Task<IHttpActionResult> PostTableQuestionNumberColumn(TableQuestionNumberColumn item)
        {
            TableQuestionNumberColumn current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TableQuestionNumberColumn/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTableQuestionNumberColumn(string id)
        {
             return DeleteAsync(id);
        }
    }
}
