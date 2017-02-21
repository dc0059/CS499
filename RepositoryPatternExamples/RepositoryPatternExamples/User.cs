using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RepositoryPatternExamples
{
    /// <summary>
    /// This class will hold the relevant data for a user
    /// </summary>
    public class User : ModelBase, IDataErrorInfo, IModel
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userID">unique identifier</param>
        /// <param name="userName">username associated to the person in the AD</param>
        /// <param name="emailAddress">email address of the viewModel</param>
        public User(Int64 userID, string userName, string emailAddress)
        {
            this.UserID = userID;
            this.UserName = userName;
            this.EmailAddress = emailAddress;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks equality based on the username
        /// </summary>
        /// <param name="obj">User model</param>
        /// <returns>true if they equal, false otherwise</returns>
        public override bool Equals(object obj)
        {

            if (obj is User)
            {
                return this.UserName.Equals((obj as User).UserName, StringComparison.OrdinalIgnoreCase);
            }

            return false;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", this.EmailAddress, this.UserName);
        }

        /// <summary>
        /// Get validation errors
        /// </summary>
        /// <param name="propertyName">name of the property to validate</param>
        /// <returns>string for the error found if any where found</returns>
        private string GetValidationError(string propertyName)
        {

            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                case "UserName":
                    error = this.ValidateUserName();
                    break;

                case "EmailAddress":
                    error = this.ValidateEmailAddress();
                    break;

                default:
                    Debug.Fail("Unexpected property being validated on Model: " + propertyName);
                    break;
            }

            return error;

        }

        /// <summary>
        /// Validate the email address
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateEmailAddress()
        {
            return !IsValidEmailAddress(this.EmailAddress) ? ErrorMessages.InvalidEmailAddress : null;
        }

        /// <summary>
        /// Validate the username
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateUserName()
        {
            return !UserInfo.ValidateUserName(this.UserName) ? ErrorMessages.InvalidUserName : null;
        }

        /// <summary>
        /// Check to make sure the value is not null 
        /// or empty
        /// </summary>
        /// <param name="value">string value to test</param>
        /// <returns>bool value if the string is null or empty</returns>
        private bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Validates an email address
        /// </summary>
        /// <param name="email">string for the email</param>
        /// <returns>bool value indicating if the email is valid</returns>
        private bool IsValidEmailAddress(string email)
        {

            if (this.IsEmpty(email))
                return false;


            // This regex pattern came from: http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        #endregion

        #region Properties

        public long ID
        {
            get { return this.UserID; }
        }

        public bool IsValid
        {
            get
            {
                foreach (var p in ValidatedProperties)
                {
                    if (GetValidationError(p) != null)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        static readonly string[] ValidatedProperties =
        {
            "UserName",
            "EmailAddress"
        };

        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }

        string IDataErrorInfo.Error
        {
            get { return null; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }

        #endregion

    }
}
