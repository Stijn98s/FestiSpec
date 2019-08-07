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
    public class TableQuestionAnswerController : TableController<TableQuestionAnswer>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<TableQuestionAnswer>(context, Request);
        }

        // GET tables/TableQuestionAnswer
        public IQueryable<TableQuestionAnswer> GetAllTableQuestionAnswer()
        {
            return Query(); 
        }

        // GET tables/TableQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<TableQuestionAnswer> GetTableQuestionAnswer(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TableQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TableQuestionAnswer> PatchTableQuestionAnswer(string id, Delta<TableQuestionAnswer> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/TableQuestionAnswer
        public async Task<IHttpActionResult> PostTableQuestionAnswer(TableQuestionAnswer item)
        {
            TableQuestionAnswer current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TableQuestionAnswer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTableQuestionAnswer(string id)
        {
             return DeleteAsync(id);
        }
    }
}
