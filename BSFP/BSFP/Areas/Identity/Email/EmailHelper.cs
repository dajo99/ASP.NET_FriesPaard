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


        public bool SendEmail(string userEmail, string url, string subject, string body)
        {
            MailMessage msg = new MailMessage();
            SmtpClient Smtp = new SmtpClient();
            bool affected = false;
            try
            {
                msg.Subject = subject;
                msg.IsBodyHtml = true;
                msg.Body = body + "<br/>" + url;
                msg.From = new System.Net.Mail.MailAddress("no_reply@bsfp2021.be");
                msg.To.Add(userEmail);
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
    }
}
