using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using K94Warriors.ViewModels.EmailNotifications;

namespace K94Warriors.Email
{
    /// <summary>
    ///     Emails messages over SMTP.
    /// </summary>
    public class SmtpMailer : IMailer
    {
        /// <summary>
        ///     Send an e-mail message.
        /// </summary>
        /// <param name="from">The from email address.</param>
        /// <param name="to">The to email address.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <exception cref="System.ArgumentException">Thrown when any parameter is null, empty, or white space.</exception>
        public async Task Send(string from, IList<string> to, string subject, string body)
        {
            await new Task(() =>
                {
                    // Sanitize
                    if (string.IsNullOrWhiteSpace(from))
                    {
                        throw new ArgumentException("cannot be null, empty, or white space", "from");
                    }
                    if (!to.Any())
                    {
                        throw new ArgumentException("must provide at least one recipient address", "to");
                    }
                    if (string.IsNullOrWhiteSpace(subject))
                    {
                        throw new ArgumentException("cannot be null, empty, or white space", "subject");
                    }
                    if (string.IsNullOrWhiteSpace(body))
                    {
                        throw new ArgumentException("cannot be null, empty, or white space", "body");
                    }

                    // Create the mail message
                    using (var mailMessage = new MailMessage {From = new MailAddress(from), Subject = subject, Body = body})
                    {
                        foreach (var address in to)
                            mailMessage.To.Add(address);

                        mailMessage.BodyEncoding = Encoding.UTF8;
                        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                        // New up the SMTP client
                        using (var smtpClient = new SmtpClient())
                        {
                            smtpClient.Send(mailMessage);
                        }
                    }
                });
        }

        public async Task Send(string from, string to, string subject, string body)
        {
            await Send(from, new[] {to}, subject, body);
        }

        public async Task Send(EmailViewModel viewModel)
        {
            await Send(viewModel.From, viewModel.To, viewModel.Subject, viewModel.Body);
        }
    }
}