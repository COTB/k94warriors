using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
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

            using (var client = new SmtpClient())
            {
                client.Send(mail);
            }
        }
    }
}