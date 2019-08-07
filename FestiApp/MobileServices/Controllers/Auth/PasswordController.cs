using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FestiMS.Manager;
using FestiMS.Models;
using FestiMS.Util;
using Microsoft.AspNet.Identity;
using Microsoft.Azure.Mobile.Server.Config;

namespace FestiMS.Controllers.Auth
{
   [MobileAppController]
    public class PasswordController : ApiController
    {
        private readonly MobileServiceContext _context;
        private readonly AuthManager _authManager;
        private readonly EmailService _emailService;

        public PasswordController(MobileServiceContext context, AuthManager authManager, EmailService emailService)
        {
            _context = context;
            _authManager = authManager;
            _emailService = emailService;
        }

        [ActionName("ChangePassword")]
        [HttpPost]
        public async Task<IdentityResult> ChangePassword([FromBody]LoginPoco loginPoco)
        {
            var findByNameAsync = await _context.FestiUsers.Include(el => el.UserAccount).FirstOrDefaultAsync(elem => elem.Id == loginPoco.UserId);
            
            _authManager.RemovePassword(findByNameAsync.UserAccount.Id);
            return await _authManager.AddPasswordAsync(findByNameAsync.UserAccount.Id, loginPoco.Password);
        }

        [ActionName("ResetPassword")]
        [HttpPut]
        public async Task<IHttpActionResult> SetInspectorPassword([FromBody]ResetPasswordPoco resetPoco)
        {
            if (!resetPoco.NewPassword.Equals(resetPoco.ConfirmNewPassword)) return Conflict();
            var resetResult =
                await _authManager.ResetPasswordAsync(resetPoco.User, resetPoco.Token, resetPoco.NewPassword);
            if (resetResult.Succeeded) return Ok();
            return Unauthorized();
        }

        [ActionName("RequestResetPassword")]
        [HttpPost]
        public async Task RequestResetPassword([FromBody]PasswordChangeRequestPOCO poco)
        {
            var inspector = await _context.Inspectors.Include(elem => elem.UserAccount).FirstOrDefaultAsync(elem => elem.UserAccount.UserName == poco.UserName);
            await _emailService.SendChangePasswordMailAsync(inspector);
        }
    }
}
