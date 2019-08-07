using FestiDB.Domain;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace FestiMS.Manager
{
    public class ApplicationSignInManager : SignInManager<UserAccount, string>
    {
        public ApplicationSignInManager(AuthManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

    }

    
}