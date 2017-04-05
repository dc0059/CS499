using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWDTK_DOTNET451;
using System.Security;
using System.Runtime.InteropServices;

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

        /// <summary>
        /// Determines whether [is equal to] [the specified SS2].
        /// </summary>
        /// <remarks>Special thanks to Nikola from stack overflow
        ///  <see cref="http://stackoverflow.com/a/23183092"/></remarks>
        /// <param name="ss1">The SS1.</param>
        /// <param name="ss2">The SS2.</param>
        /// <returns>
        ///   <c>true</c> if [is equal to] [the specified SS2]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEqualTo(this SecureString ss1, SecureString ss2)
        {
            IntPtr bstr1 = IntPtr.Zero;
            IntPtr bstr2 = IntPtr.Zero;

            if (ss1 == null || ss2 == null)
            {
                return false;
            }

            try
            {
                bstr1 = Marshal.SecureStringToBSTR(ss1);
                bstr2 = Marshal.SecureStringToBSTR(ss2);
                int length1 = Marshal.ReadInt32(bstr1, -4);
                int length2 = Marshal.ReadInt32(bstr2, -4);

                if (length1 == 0 || length2 == 0)
                {
                    return false;
                }

                if (length1 == length2)
                {
                    for (int x = 0; x < length1; ++x)
                    {
                        byte b1 = Marshal.ReadByte(bstr1, x);
                        byte b2 = Marshal.ReadByte(bstr2, x);
                        if (b1 != b2) return false;
                    }
                }
                else return false;
                return true;
            }
            finally
            {
                if (bstr2 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr2);
                if (bstr1 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr1);
            }
        }

        /// <summary>
        /// To the unsecured string.
        /// </summary>
        /// <param name="ss">The ss.</param>
        /// <returns></returns>
        public static string ToUnsecuredString(this SecureString ss)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(ss);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        #endregion

    }
}
