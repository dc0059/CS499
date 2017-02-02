using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CS499.TCMS.Model
{
    /// <summary>
    /// This class will hold the relevant data for a user
    /// </summary>
    public class User : IModel
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="id">unique identifier</param>
        /// <param name="userName">username associated to user</param>
        /// <param name="firstName">first name of the user</param>
        /// <param name="lastName">last name of the user</param>
        /// <param name="emailAddress">email address of the user</param>
        /// <param name="isActive">flag indicating an active user</param>
        public User(long id, string userName, string firstName, string lastName, string emailAddress, bool isActive)
        {
            this.ID = id;
            this.UserName = userName;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAddress = emailAddress;
            this.IsActive = isActive;
        }

        #endregion

        #region Methods

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

                case "FirstName":
                    error = this.ValidateFirstName();
                    break;

                case "LastName":
                    error = this.ValidateLastName();
                    break;

                case "EmailAddress":
                    error = this.ValidateEmailAddress();
                    break;

                default:
                    Debug.Fail("Unexpected property being validated on User: " + propertyName);
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
            return !IsValidEmailAddress(this.EmailAddress) ? Messages.InvalidEmailAddress : null;
        }

        /// <summary>
        /// Validate the last name
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateLastName()
        {
            return IsEmpty(this.LastName) ? Messages.InvalidName : null;
        }

        /// <summary>
        /// Validate the first name
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateFirstName()
        {
            return IsEmpty(this.FirstName) ? Messages.InvalidName : null;
        }

        /// <summary>
        /// Validate the username
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateUserName()
        {
            return IsEmpty(this.UserName) ? Messages.InvalidUserName : null;
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

        /// <summary>
        /// Flag indicating the model passes the verification test
        /// </summary>
        bool IModel.IsValid
        {
            get
            {
                return this.IsValid;
            }
        }

        /// <summary>
        /// Unique identifier
        /// </summary>
        long IModel.ID
        {
            get
            {
                return this.ID;
            }
            set
            {
                this.ID = value;
            }
        }

        /// <summary>
        /// Flag indicating the model passes the verification test
        /// </summary>
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

        /// <summary>
        /// Array with the property names to validate
        /// </summary>
        static readonly string[] ValidatedProperties =
        {
            "UserName",
            "FirstName",
            "LastName",
            "EmailAddress"
        };

        /// <summary>
        /// Unique indentifier
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Username associated to a single user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// First name of the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email address of the user
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Flag indicating the user is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Error message for the who class (not implemented)
        /// </summary>
        string IDataErrorInfo.Error
        {
            get { return null; }
        }

        /// <summary>
        /// Error message for the individual property
        /// </summary>
        /// <param name="propertyName">name of the property</param>
        /// <returns>string indicating the validation error, null otherwise</returns>
        string IDataErrorInfo.this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }

        #endregion

    }
}
