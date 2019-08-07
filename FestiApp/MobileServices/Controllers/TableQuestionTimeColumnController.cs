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
    public class TableQuestionTimeColumnController : TableController<TableQuestionTimeColumn>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<TableQuestionTimeColumn>(context, Request);
        }

        // GET tables/TableQuestionTimeColumn
        public IQueryable<TableQuestionTimeColumn> GetAllTableQuestionTimeColumn()
        {
            return Query(); 
        }

        // GET tables/TableQuestionTimeColumn/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<TableQuestionTimeColumn> GetTableQuestionTimeColumn(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TableQuestionTimeColumn/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TableQuestionTimeColumn> PatchTableQuestionTimeColumn(string id, Delta<TableQuestionTimeColumn> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/TableQuestionTimeColumn
        public async Task<IHttpActionResult> PostTableQuestionTimeColumn(TableQuestionTimeColumn item)
        {
            TableQuestionTimeColumn current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TableQuestionTimeColumn/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTableQuestionTimeColumn(string id)
        {
             return DeleteAsync(id);
        }
    }
}
