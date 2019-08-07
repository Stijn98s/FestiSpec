using System;
using System.Threading.Tasks;
using System.Web.Http;
using FestiDB.Domain;
using FestiDB.Domain.Answers;
using FestiMS.Manager;
using FestiMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FestiMS.Controllers.Auth
{
    public class AuthController : ApiController
    {
        private readonly TokenManager _tokenManager;
        private readonly MobileServiceContext _context;
        private readonly AuthManager _authManager;
        

        public AuthController(MobileServiceContext context, AuthManager authManager, TokenManager tokenManager)
        {
            _context = context;
            _authManager = authManager;
            _tokenManager = tokenManager;

        }

        public async Task<IHttpActionResult> Post([FromBody] LoginPoco assertion)
        {
            UserAccount user;
            if ((user = await _authManager.IsValidAssertion(assertion)) == null) return Unauthorized();
            var token = await _tokenManager.CreateTokenMSCLient(user);
            return Ok(new LoginResult
            {
                AuthenticationToken = token,
                User = new LoginResultUser {UserId = user.UserName }
            });
        }


    }
}