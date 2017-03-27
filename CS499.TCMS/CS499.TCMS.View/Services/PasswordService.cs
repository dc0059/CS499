using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWDTK_DOTNET451;

namespace CS499.TCMS.View.Services
{
    /// <summary>
    /// This class will handle the password hashing and password hash string validation
    /// </summary>
    public static class PasswordService
    {

        #region Methods

        /// <summary>
        /// Hash user password and write out the passphrase
        /// </summary>
        /// <param name="password">password to hash</param>
        /// <param name="passphrase">variable to store the passphrase</param>
        /// <returns>Base64 string of the password hash</returns>
        public static string HashPassword(string password, out string passphrase)
        {

            // generate salt
            byte[] salt = PWDTK.GetRandomSalt(256);

            // set passphrase
            passphrase = Convert.ToBase64String(salt);

            // hash password
            byte[] hash = PWDTK.PasswordToHash(salt, password, 10000);

            return Convert.ToBase64String(hash);

        }

        /// <summary>
        /// Validate user password with the exiting user hash
        /// </summary>
        /// <param name="password">password to validate</param>
        /// <param name="passphrase">passphrase from the existing user</param>
        /// <param name="userHash">hash string from the existing user</param>
        /// <returns>true if the password matches, false otherwise</returns>
        public static bool ValidatePassword(string password, string passphrase, string userHash)
        {

            // convert existing user passphrase to the salt
            byte[] salt = Convert.FromBase64String(passphrase);

            // convert existing user hash to the byte hash
            byte[] hash = Convert.FromBase64String(userHash);

            // compare password to the hash
            bool isValid = PWDTK.ComparePasswordToHash(salt, password, hash, 10000);

            return isValid;

        }

        #endregion

    }
}
