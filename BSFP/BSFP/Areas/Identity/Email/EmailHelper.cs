using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BSFP.Areas.Identity.Email
{
    public class EmailHelper
    {
        private readonly ILogger _logger;

        public bool SendConfirmationEmail(string userEmail, string confirmationLink)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                msg.Subject = "Confirm your email";
                msg.Body = confirmationLink;
                msg.From = new MailAddress("dajo.vandoninck99@gmail.com");
                msg.To.Add(userEmail);
                msg.IsBodyHtml = true;
                client.Host = "smtp.gmail.com";
                NetworkCredential basicauthenticationinfo = new NetworkCredential("dajo.vandoninck99@gmail.com", "Nederland");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

            }
            return false;

        }

        public bool SendEmailPasswordReset(string userEmail, string link)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();

            try
            {
                msg.Subject = "Wachtwoord herstellen";
                msg.Body = link;
                msg.From = new MailAddress("dajo.vandoninck99@gmail.com");
                msg.To.Add(userEmail);
                msg.IsBodyHtml = true;
                client.Host = "smtp.gmail.com";
                NetworkCredential basicauthenticationinfo = new NetworkCredential("dajo.vandoninck99@gmail.com", "Nederland");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

            }
            return false;
        }
    }
}
