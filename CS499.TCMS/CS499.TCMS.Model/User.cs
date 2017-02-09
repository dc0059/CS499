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
        /// <param name="middleName">middle name of the user</param>
        /// <param name="lastName">last name of the user</param>
        /// <param name="address">street address of the user</param>
        /// <param name="city">city of the user</param>
        /// <param name="state">state of the user</param>
        /// <param name="zipCode">zipCode of the user</param>
        /// <param name="homePhone">home phone number of the user</param>
        /// <param name="cellPhone">cell phone number of the user</param>
        /// <param name="payRate">pay rate of the user</param>
        /// <param name="jobDescription">job description of the user</param>
        /// <param name="isActive">flag indicating an active user</param>
        public User(long id, string userName, string firstName, string middleName, string lastName, string address, string city, string state, int zipCode,
            string homePhone, string cellPhone, int payRate, string jobDescription, bool isActive)
        {
            this.ID = id;
            this.UserName = userName;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.Address = address;
            this.City = city;
            this.State = state;
            this.ZipCode = zipCode;
            this.HomePhone = homePhone;
            this.CellPhone = cellPhone;
            this.PayRate = payRate;
            this.JobDescription = jobDescription;
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

                case "MiddleName":
                    error = this.ValidateMiddleName();
                    break;

                case "LastName":
                    error = this.ValidateLastName();
                    break;

                case "Address":
                    error = this.ValidateAddress();
                    break;

                case "City":
                    error = this.ValidateCity();
                    break;

                case "State":
                    error = this.ValidateState();
                    break;

                case "ZipCode":
                    error = this.ValidateZipCode();
                    break;

                case "HomePhone":
                    error = this.ValidateHomePhone();
                    break;

                case "CellPhone":
                    error = this.ValidateCellPhone();
                    break;

                case "PayRate":
                    error = this.ValidatePayRate();
                    break;

                case "JobDescription":
                    error = this.ValidateJobDescription();
                    break;

                default:
                    Debug.Fail("Unexpected property being validated on User: " + propertyName);
                    break;
            }

            return error;

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
        /// Validate the middle name
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateMiddleName()
        {
            return IsEmpty(this.MiddleName) ? Messages.InvalidName : null;
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
        /// Validate the street address
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateAddress()
        {
            return IsEmpty(this.LastName) ? Messages.InvalidAddress : null;
        }

        /// <summary>
        /// Validate the city name
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateCity()
        {
            return IsEmpty(this.LastName) ? Messages.InvalidCity : null;
        }

        /// <summary>
        /// Validate the state name
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateState()
        {
            return IsEmpty(this.LastName) ? Messages.InvalidState : null;
        }

        /// <summary>
        /// Validate the zip code
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateZipCode()
        {
            return IsEmpty(this.LastName) ? Messages.InvalidZip : null;
        }

        /// <summary>
        /// Validate the home phone number
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateHomePhone()
        {
            return IsEmpty(this.LastName) ? Messages.InvalidPhone : null;
        }

        /// <summary>
        /// Validate the cell phone number
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateCellPhone()
        {
            return IsEmpty(this.LastName) ? Messages.InvalidPhone : null;
        }

        /// <summary>
        /// Validate the last name
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidatePayRate()
        {
            return IsEmpty(this.LastName) ? Messages.InvalidPay : null;
        }

        /// <summary>
        /// Validate the last name
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateJobDescription()
        {
            return IsEmpty(this.LastName) ? Messages.InvalidDescription : null;
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
            "MiddleName",
            "LastName",
            "Address",
            "City",
            "State",
            "ZipCode",
            "HomePhone",
            "CellPhone",
            "PayRate",
            "JobDescription"
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
        /// Middle name of the user
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Street address of the user
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// City of the user
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// State of the user
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Zip code of the user
        /// </summary>
        public int ZipCode { get; set; }

        /// <summary>
        /// Home phone number of the user
        /// </summary>
        public string HomePhone { get; set; }

        /// <summary>
        /// Cell phone number of the user
        /// </summary>
        public string CellPhone { get; set; }

        /// <summary>
        /// Pay rate of the user
        /// </summary>
        public int PayRate { get; set; }

        /// <summary>
        /// Job description of the user
        /// </summary>
        public string JobDescription { get; set; }

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
