using FestiMS.Manager;
using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using FestiDB.Domain;

namespace FestiMS.Util
{
    public class EmailService : IIdentityMessageService
    {
        private readonly AuthManager _authmanager;

        public EmailService(AuthManager authmanager)
        {
            _authmanager = authmanager;
        }
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasyncAsync(message);
        }

        private async Task<SendGrid.Response> configSendGridasyncAsync(IdentityMessage message)
        {
            var apiKey = "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"; //ConfigurationManager.AppSettings["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"];//Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("FestiSpec@festi.com", "FestiSpec");
            var subject = message.Subject;
            var to = new EmailAddress(message.Destination, "Inspecteur");
            var plainTextContent = message.Body;
            var htmlContent = message.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var t = await client.SendEmailAsync(msg);
            return t;
            }

        public async Task SendChangePasswordMailAsync(Inspector item)
        {
            string token = await _authmanager.GeneratePasswordResetTokenAsync(item.UserAccount.Id);
            token = System.Web.HttpUtility.UrlEncode(token);
            var message = new IdentityMessage();
            //string url = $@"""https://festispa.z6.web.core.windows.net/reset-password?user={item.UserAccount.Id};token={token}""";
            string href = "<br /> Welkom bij FestiSpec, voordat u aan de slag kunt gaan moet u uw wachtwoord kiezen.<br />"+ $" <a href = "+ $"https://festispa.z6.web.core.windows.net/reset-password?user={item.UserAccount.Id}&token={token}" + ">"+"Kies uw wachtwoord hier</a><br /> FestiSpec wij zetten de puntjes op de F  </body> </html>";
            //string href = $"<a href={url}>Reset wachtwoord hier</a>  </body> </html>";
            message.Body = $"<html><body>Beste {item.FirstName}, {href}";
            message.Subject = "Wachtwoord reset";
            message.Destination = item.Email;
            await configSendGridasyncAsync(message);
        }
    }
        
}