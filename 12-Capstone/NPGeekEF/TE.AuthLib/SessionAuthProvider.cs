using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using TE.AuthLib.DAL;

namespace TE.AuthLib
{
    /// <summary>
    /// An implementation of the IAuthProvider that saves data within session.
    /// </summary>
    public class SessionAuthProvider : IAuthProvider
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserDAO userDAO;
        public static string SessionKey = "Auth_User";
        public static string SessionRoleKey = "User_Role";

        public SessionAuthProvider(IHttpContextAccessor contextAccessor, IUserDAO userDAO)
        {
            this.contextAccessor = contextAccessor;
            this.userDAO = userDAO;
        }

        /// <summary>
        /// Gets at the session attached to the http request.
        /// </summary>
        ISession Session => contextAccessor.HttpContext.Session;

        /// <summary>
        /// Returns true if the user is logged in.
        /// </summary>
        public bool IsLoggedIn => !String.IsNullOrEmpty(Session.GetString(SessionKey));

        /// <summary>
        /// Signs the user in and saves their username in session.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string username, string password)
        {
            User user = userDAO.GetUser(username);
            HashProvider hashProvider = new HashProvider();                        
            
            if (user != null && hashProvider.VerifyPasswordMatch(user.Password, password, user.Salt))
            {                
                Session.SetString(SessionKey, user.Username);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Logs the user out by clearing their session data.
        /// </summary>
        public void Logout()
        {
            Session.Remove(SessionKey);
            //Session.Clear();
        }

        /// <summary>
        /// Changes the current user's password.
        /// </summary>
        /// <param name="existingPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ChangePassword(string existingPassword, string newPassword)
        {
            HashProvider hashProvider = new HashProvider();
            User user = GetCurrentUser();
            
            // Confirm existing password match
            if (user != null && hashProvider.VerifyPasswordMatch(user.Password, existingPassword, user.Salt))
            {
                // Hash new password
                HashedPassword newHash = hashProvider.HashPassword(newPassword);
                user.Password = newHash.Password;
                user.Salt = newHash.Salt;

                // Save into the db
                userDAO.UpdateUser(user);

                return true;
            }

            return false;
        }

        public void UpdateTempPref(string tempPref)
        {
            User user = GetCurrentUser();

            //Check if the new temp pref is different than the stored temp pref
            if (user.TempPref != tempPref)
            {
                // Set the user object preference to the passed temp preference
                user.TempPref = tempPref;

                // Update the user in the database
                userDAO.UpdateUser(user);
            }

            return;
        }

        /// <summary>
        /// Gets the user using the current username in session.
        /// </summary>
        /// <returns></returns>
        public User GetCurrentUser()
        {
            string username = Session.GetString(SessionKey);

            if (!String.IsNullOrEmpty(username))
            {
                return userDAO.GetUser(username);
            }
            
            return null;
        }

        /// <summary>
        /// Creates a new user and saves their username in session.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public void Register(string username, string password, string role)
        {
            HashProvider hashProvider = new HashProvider();
            HashedPassword passwordHash = hashProvider.HashPassword(password);

            User user = new User
            {
                Username = username,
                Password = passwordHash.Password,
                Salt = passwordHash.Salt,
                Role = role
            };

            userDAO.CreateUser(user);
            Session.SetString(SessionKey, user.Username);            
        }

        /// <summary>
        /// Checks to see if the user has a given role.
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool UserHasRole(string[] roles)
        {
            User user = GetCurrentUser();
            return (user != null) && 
                roles.Any(r => r.ToLower() == user.Role.ToLower());
        }
    }
}
