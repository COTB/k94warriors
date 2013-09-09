using System;
using System.Web.Mvc;
using K94Warriors.Email;

namespace K94Warriors.Filters
{
    /// <summary>
    ///     Emails unhandled exceptions to appropriate users
    /// </summary>
    public class EmailErrorAttribute : HandleErrorAttribute
    {
        // The SMTP mailer
        // The from email address
        private const string _subjectFormat = "K94WARRIORS: Exception of type {0} occurred";
        private readonly string _from;
        private readonly SmtpMailer _smtpMailer;
        // The to email address
        private readonly string _to;
        // THe subject format

        /// <summary>
        ///     The constructor.
        /// </summary>
        /// <param name="smtpMailer">The SMTP mailer to use.</param>
        /// <param name="from">The from email address.</param>
        /// <param name="to">The to email address.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when smtpMailer is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when from or to is null, empty, or white space.</exception>
        public EmailErrorAttribute(SmtpMailer smtpMailer, string from, string to)
        {
            // Sanitize
            if (smtpMailer == null)
            {
                throw new ArgumentNullException("smtpMailer");
            }
            if (string.IsNullOrWhiteSpace(from))
            {
                throw new ArgumentException("cannot be null, empty, or white space", "from");
            }
            if (string.IsNullOrWhiteSpace(to))
            {
                throw new ArgumentException("cannot be null, empty, or white space", "to");
            }

            // Set fields
            _smtpMailer = smtpMailer;
            _from = from;
            _to = to;
        }

        /// <summary>
        ///     Fired when an exception occurs.
        /// </summary>
        /// <param name="filterContext">The exception context.</param>
        public override void OnException(ExceptionContext filterContext)
        {
            // Get the exception
            Exception ex = filterContext.Exception;

            // Log it
            try
            {
                _smtpMailer.Send(_from, _to, string.Format(_subjectFormat, ex.GetType()), ex.ToString());
            }
            catch
            {
                // Exception handler CANNOT raise an exception!
            }

            // Set exception handled
            filterContext.ExceptionHandled = true;
        }
    }
}