using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;

namespace FestiMS.Controllers
{
    [MobileAppController]
    public class StatusController : ApiController
    {
        // GET api/Status
        public string Get()
        {
            return "Hello from custom controller!";
        }
    
        [HttpHead]
        public IHttpActionResult Status()
        {
            return Ok();
        }
    }
}
