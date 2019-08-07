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
    public class TableQuestionAnswerEntryController : TableController<TableQuestionAnswerEntry>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<TableQuestionAnswerEntry>(context, Request);
        }

        // GET tables/TableQuestionAnswerEntry
        public IQueryable<TableQuestionAnswerEntry> GetAllTableQuestionAnswerEntry()
        {
            return Query(); 
        }

        // GET tables/TableQuestionAnswerEntry/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<TableQuestionAnswerEntry> GetTableQuestionAnswerEntry(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TableQuestionAnswerEntry/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TableQuestionAnswerEntry> PatchTableQuestionAnswerEntry(string id, Delta<TableQuestionAnswerEntry> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/TableQuestionAnswerEntry
        public async Task<IHttpActionResult> PostTableQuestionAnswerEntry(TableQuestionAnswerEntry item)
        {
            TableQuestionAnswerEntry current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TableQuestionAnswerEntry/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTableQuestionAnswerEntry(string id)
        {
             return DeleteAsync(id);
        }
    }
}
