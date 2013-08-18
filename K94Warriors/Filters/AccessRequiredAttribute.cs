using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using K94Warriors.Handlers;
using K94Warriors.Models;
using K94Warriors.Enums;

namespace K94Warriors.Filters
{
    /// <summary>
    /// Enforces that the user is logged in and the appropriate user type before granting access.
    /// </summary>
    public class AccessRequiredAttribute : ActionFilterAttribute
    {
        // The login handler
        private static readonly LoginHandler _loginHandler = new LoginHandler(RepoResolver.GetRepository<User>());
        // The not authorized URL
        private readonly string _notAuthorizedUrl = null;
        // The user types
        private readonly UserTypeEnum[] _userTypes = null;
        

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="notAuthorizedUrl">The not authorized URL to redirect to when the user is not authorized.</param>
        /// <param name="userTypes">The user types which are authorized to access the current controller action.</param>
        /// <exception cref="System.ArgumentException">Thrown when notAuthorizedUrl is null, empty, or white space.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when userTypes is null.</exception>
        public AccessRequiredAttribute(string notAuthorizedUrl, params UserTypeEnum[] userTypes)
        {
            // Sanitize
            if (string.IsNullOrWhiteSpace(notAuthorizedUrl))
            {
                throw new ArgumentException("cannot be null, empty, or white space", "notAuthorizedUrl");
            }
            if (userTypes == null)
            {
                throw new ArgumentNullException("userTypes");
            }

            // Set fields
            _notAuthorizedUrl = notAuthorizedUrl;
            _userTypes = userTypes;
        }

        /// <summary>
        /// Occurs just before the Action is executed.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Ensure user is logged in
            if (_loginHandler.IsLoggedIn(filterContext.HttpContext.Session))
            {
                // Nope
                filterContext.HttpContext.Response.Redirect(_notAuthorizedUrl, true);
                return;
            }

            // Get the user
            var user = _loginHandler.GetUser(filterContext.HttpContext.Session);

            // Logged in, ensure user is an appropriately authorized type
            for (int i = 0; i < _userTypes.Length; i++)
            {
                if ((int)_userTypes[i] == user.UserTypeID)
                {
                    // yup
                    return;
                }
            }

            // Not authorized
            filterContext.HttpContext.Response.Redirect(_notAuthorizedUrl, true);
        }
    }
}