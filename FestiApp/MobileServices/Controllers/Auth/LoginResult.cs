using Newtonsoft.Json;

namespace FestiMS.Controllers.Auth
{
    internal class LoginResult
    {
        public LoginResult()
        {
        }

        [JsonProperty(PropertyName = "authenticationToken")]
        public string AuthenticationToken { get; set; }
        [JsonProperty(PropertyName = "user")]
        public LoginResultUser User { get; set; }
    }

    internal class LoginResultUser
    {
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
    }
}