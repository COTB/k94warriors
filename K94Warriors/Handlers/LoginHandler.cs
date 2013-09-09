using System;
using System.Web;
using K94Warriors.Data;
using K94Warriors.Models;

namespace K94Warriors.Handlers
{
    /// <summary>
    ///     Handles login functionality.
    /// </summary>
    public class LoginHandler
    {
        // The user repository
        // The session is logged in key
        private const string _sessionLoggedInKey = "__IsLoggedIn_";
        private readonly IRepository<User> _userRepository;

        /// <summary>
        ///     The constructor.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when userRepository is null.</exception>
        public LoginHandler(IRepository<User> userRepository)
        {
            // Sanitize
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            // Set fields
            _userRepository = userRepository;
        }

        /// <summary>
        ///     Attempts to login a user.
        /// </summary>
        /// <param name="session">The user's session.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>true if login was successful, false otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when session is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when username or password is null, empty, or white space.</exception>
        public bool Login(HttpSessionStateBase session, string username, string password)
        {
            // Sanitize
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("cannot be null, empty, or white space", "username");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("cannot be null, empty, or white space", "password");
            }

            // Try and find the user by username and password
            var user = (User) null;
                // TODO: need a password! --> _userRepository.GetAll().FirstOrDefault(i => i.Email == username && i.Password == password);

            // If no user the login was unsuccessful
            if (user == null)
            {
                return false;
            }

            // Login was successful
            session[_sessionLoggedInKey] = user;
            return true;
        }

        /// <summary>
        ///     Attempts to logout a user.
        /// </summary>
        /// <param name="session">The user's session.</param>
        /// <returns>true if logout was successful, false otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when session is null.</exception>
        public bool Logout(HttpSessionStateBase session)
        {
            // Sanitize
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            // Get object from session
            object result = session[_sessionLoggedInKey];
            // Check if we found something
            if (result == null)
            {
                // Nope
                return false;
            }

            // Log 'em out
            session.Remove(_sessionLoggedInKey);
            return true;
        }

        /// <summary>
        ///     Checks whether the user is logged in.
        /// </summary>
        /// <param name="session">The user's session.</param>
        /// <returns>true if the user is logged in, false otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when session is null.</exception>
        public bool IsLoggedIn(HttpSessionStateBase session)
        {
            // Sanitize
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            // If we have a value, they're logged in
            return (session[_sessionLoggedInKey] as User) != null;
        }

        /// <summary>
        ///     Gets the user if the user is logged in.
        /// </summary>
        /// <param name="session">The user's session.</param>
        /// <returns>The user, or null if the user is not logged in.</returns>
        public User GetUser(HttpSessionStateBase session)
        {
            return session[_sessionLoggedInKey] as User;
        }
    }
}