using System;
using System.DirectoryServices.AccountManagement;

namespace RepositoryPatternExamples
{
    /// <summary>
    /// This class will contain methods for validating a user against the AD
    /// </summary>
    public static class UserInfo
    {

        #region Methods

        /// <summary>
        /// Validate username in the current domain
        /// </summary>
        /// <param name="userName">string for the username</param>
        /// <returns>bool value indicating a valid username</returns>
        public static bool ValidateUserName(string userName)
        {

            // make sure the username is not null
            if (string.IsNullOrEmpty(userName.Trim()))
            {
                return false;
            }

            // Create new principle context
            using (var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
            {

                // use find by identity to validate viewModel name
                using (var identity = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userName))
                {

                    return identity != null;
                }

            }

        }

        /// <summary>
        /// Gets the AD viewModel account information
        /// </summary>
        /// <param name="userName">username to lookup in the AD</param>
        /// <returns>UserPrincipal containing the AD account information</returns>
        public static UserPrincipal GetUser(string userName)
        {

            // make sure the username is not null
            if (string.IsNullOrEmpty(userName.Trim()))
            {
                return null;
            }

            // Create new principle context
            using (var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
            {

                // use find by identity to validate viewModel name
                return UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userName);

            }

        }

        /// <summary>
        /// Validate viewModel login credentials
        /// </summary>
        /// <param name="userName">string for the username</param>
        /// <param name="password">string for the password</param>
        /// <returns>bool value indicating a successful login</returns>
        public static bool ValidateLogin(string userName, string password)
        {

            // make sure the username is not null
            if (string.IsNullOrEmpty(userName.Trim()))
            {
                return false;
            }

            // make sure the password is not null
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            // Create new principle context
            using (var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
            {

                // validate credentials using the context
                return context.ValidateCredentials(userName, password);

            }

        }

        /// <summary>
        /// Remove the domain name from the username
        /// </summary>
        /// <param name="username">string for the username</param>
        /// <returns>username without the domain name</returns>
        public static string GetUsername(this string username)
        {
            string u = username.ToLower();
            int right = username.IndexOf(@"\");
            return right == -1 ? u : u.Substring(right + 1);
        }

        #endregion

    }
}
