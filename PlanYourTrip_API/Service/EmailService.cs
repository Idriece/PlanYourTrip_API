using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
namespace PlanYourTrip_API.Service
{
    public class EmailService : IIdentityMessageService
    {
        [HttpPost]
        public async Task SendAsync(IdentityMessage message)
        {
            // Credentials:
            var credentialUserName = "planyourtrip.project@gmail.com";
            var sentFrom = "planyourtrip.project@gmail.com";
            var pwd = "Planyourtrip@123";

            // Configure the client:
            System.Net.Mail.SmtpClient client =
                new System.Net.Mail.SmtpClient("smtp.gmail.com", Convert.ToInt32(587));

            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Creatte the credentials:
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = true;
            client.Credentials = credentials;

            // Create the message:
            var mail =
                new System.Net.Mail.MailMessage(sentFrom, message.Destination);

            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;
            // Send:
            await client.SendMailAsync(mail);
        }
    }
}
