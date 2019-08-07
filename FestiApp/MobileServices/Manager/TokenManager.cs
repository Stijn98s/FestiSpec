
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FestiDB.Domain;
using FestiMS.Models;
using Microsoft.Azure.Mobile.Server.Login;
using SecurityAlgorithms = Microsoft.IdentityModel.Tokens.SecurityAlgorithms;

namespace FestiMS.Manager
{
    public class TokenManager
    {
        private readonly string _signingKey;
        private readonly string _audience;
        private readonly string _issuer;
        private readonly AuthManager _authManager;

        private TimeSpan TimeSpan => TimeSpan.FromHours(24);

        public TokenManager(MobileServiceContext festiContext)
        {
            _authManager = new AuthManager(festiContext);
            _signingKey = ConfigurationManager.AppSettings["MS_SigningKey"];
            _audience = ConfigurationManager.AppSettings["EMA_RuntimeUrl"]; // audience must match the url of the site
            _issuer = ConfigurationManager.AppSettings["EMA_RuntimeUrl"]; // audience must match the url of the site
        }





        public async Task<string> CreateToken(UserAccount assertion)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_signingKey);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                TokenIssuerName = _issuer,
                Subject = new ClaimsIdentity(await GetValidClaims(assertion)),
                SigningCredentials = new SigningCredentials(new  InMemorySymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature, SecurityAlgorithms.HmacSha512)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> CreateTokenMSCLient(UserAccount assertion)
        {
            var token = AppServiceLoginHandler.CreateToken(
                await GetValidClaims(assertion),
                _signingKey,
                _audience,
                _issuer,
                TimeSpan);
            return token.RawData;
        }




        private async Task<List<Claim>> GetValidClaims(UserAccount user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            };
            var userClaims = await _authManager.GetClaimsAsync(user.Id);
            var userRoles = await _authManager.GetRolesAsync(user.Id);
            claims.AddRange(userClaims);
            claims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
            return claims;
        }
    }
}