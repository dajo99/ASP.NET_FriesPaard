using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BSFP.Areas.Identity.Email
{
    public class EmailHelper
    {


        public bool SendConfirmationEmail(string userEmail, string url, string subject, string body)
        {
            MailMessage msg = new MailMessage();
            SmtpClient Smtp = new SmtpClient();
            bool affected = false;
            try
            {
                msg.Subject = subject;
                msg.Body = body + Environment.NewLine + url;
                msg.From = new System.Net.Mail.MailAddress("no_reply@bsfp2021.be");
                msg.To.Add(userEmail);
                msg.IsBodyHtml = true;
                Smtp.UseDefaultCredentials = false;
                Smtp.Credentials = new NetworkCredential("no_reply@bsfp2021.be", "Nederlandroel99@"); ;
                Smtp.Host = "smtp.mijnhostingpartner.nl";
                Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                Smtp.Send(msg);
                affected = true;
            }
            catch(Exception ex)
            {
                affected = false;
                throw new Exception(ex.ToString());
            }

            return affected;
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
            catch 
            {
                return false;
            }
            
        }
    }
}
