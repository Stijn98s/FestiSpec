using System;
using System.Threading.Tasks;
using System.Web.Security;
using FestiDB.Domain;
using FestiMS.Controllers.Auth;
using FestiMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace FestiMS.Manager
{
    public class AuthManager : UserManager<UserAccount>
    {
        public AuthManager(MobileServiceContext context) : base(new UserStore<UserAccount>(context))
        {
            var provider = new MachineKeyProtectionProvider();
            UserTokenProvider = new DataProtectorTokenProvider<UserAccount>(
                provider.Create("ResetPasswordPurpose"));

            PasswordValidator = new PasswordValidator
            {
                RequiredLength =4,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
                
            };
        }


        public async Task<UserAccount> IsValidAssertion(LoginPoco assertion)
        {
//#if DEBUG
//            assertion.Username = "admin@admin.nl";
//            assertion.Password = "admin1";
//#endif
            var findByNameAsync = await FindByNameAsync(assertion.Username);

            if (!await CheckPasswordAsync(findByNameAsync, assertion.Password))
            {
                // if password is invalid
                return null;
            }
            return findByNameAsync;
        }
    }

    public class MachineKeyProtectionProvider : IDataProtectionProvider
    {
        public IDataProtector Create(params string[] purposes)
        {
            return new MachineKeyDataProtector(purposes);
        }
    }

    public class MachineKeyDataProtector : IDataProtector
    {
        private readonly string[] _purposes;

        public MachineKeyDataProtector(string[] purposes)
        {
            _purposes = purposes;
        }

        public byte[] Protect(byte[] userData)
        {
            return MachineKey.Protect(userData, _purposes);
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            return MachineKey.Unprotect(protectedData, _purposes);
        }
    }
}