using System.Threading.Tasks;
using System.Web.Http;
using FestiDB.Domain;
using FestiMS.Controllers.Auth;
using FestiMS.Manager;
using Microsoft.Azure.Mobile.Server.Config;

namespace FestiMS.Controllers
{
    [MobileAppController]
    public class WebAuthController : ApiController
    {
        private TokenManager _tokenManager;
        private AuthManager _authManager;

        public WebAuthController(TokenManager tokenManager, AuthManager authManager)
        {
            _tokenManager = tokenManager;
            _authManager = authManager;
        }

        // GET api/WebAuth
        public async Task<IHttpActionResult> Post([FromBody] LoginPoco assertion)
        {
            UserAccount user;
            if ((user = await _authManager.IsValidAssertion(assertion)) == null) return Unauthorized();
            var token = await _tokenManager.CreateToken(user);
            return Ok(new LoginResult
            {
                AuthenticationToken = token,
                User = new LoginResultUser { UserId = user.UserName }
            });
        }
    }
}
