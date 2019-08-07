
namespace FestiMS.Controllers.Auth
{
    public class ResetPasswordPoco
    {
        public string User { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}