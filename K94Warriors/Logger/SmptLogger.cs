using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace K94Warriors.Logger
{
    
    public class SmptLogger
    {
        public void Send(EmailMessage email)
        {
            MailMessage mail = new MailMessage(email.From, email.To, email.Subject, email.Body);
            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            var client = new SmtpClient
            {
                Port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]),
                Host = ConfigurationManager.AppSettings["SmtpHost"],
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };
            // credentials required?

            client.Send(mail);
        }
    }
}