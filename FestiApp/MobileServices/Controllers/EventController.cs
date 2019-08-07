using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using FestiDB.Domain;
using FestiMS.Models;
using FestiMS.Util;
using System;
using FestiApp.Util.Util;
using System.Diagnostics;

namespace FestiMS.Controllers
{
    public class EventController : TableController<Event>
    {
        private GeodanHelperService _geo;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Event>(context, Request);
            _geo = new GeodanHelperService();
        }

        // GET tables/Event
        public IQueryable<Event> GetAllEvent()
        {

            return Query(); 
        }
        // GET tables/Event/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Event> GetEvent(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Event/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<Event> PatchEvent(string id, Delta<Event> patch)
        {
            object post;
            patch.TryGetPropertyValue("PostalCode", out post);
            object houseNumber;
            patch.TryGetPropertyValue("HouseNumber", out houseNumber);

            try
            {
                var result = await _geo.GetDocByAdres(null, null, houseNumber.ToString(), post.ToString());
                patch.TrySetPropertyValue("GeodanAdresId", result.id);
                patch.TrySetPropertyValue("GeodanAdresX", result.location.X.ToString());
                patch.TrySetPropertyValue("GeodanAdresY", result.location.Y.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return await UpdateAsync(id, patch);
        }

        // POST tables/Event
        public async Task<IHttpActionResult> PostEvent(Event item)
        {
            try
            {
                              
                if (_geo.Error == null)
                {
                    var result = await _geo.GetDocByAdres(null, null, item.HouseNumber, item.PostalCode);
                    item.GeodanAdresId = result.id;
                    item.GeodanAdresX = result.location.X.ToString();
                    item.GeodanAdresY = result.location.Y.ToString();
                }
                else
                {
                    item.GeodanAdresId = _geo.Error;
                    Debug.WriteLine(_geo.Error);
                    item.GeodanAdresX = "1";
                    item.GeodanAdresY = "1";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Event current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Event/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteEvent(string id)
        {
             return DeleteAsync(id);
        }
    }
}
