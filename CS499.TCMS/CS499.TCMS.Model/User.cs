﻿using System;
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
        /// <param name="employeeID">unique identifier</param>
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
        /// <param name="email">email address of the user</param>
        /// <param name="payRate">pay rate of the user</param>
        /// <param name="employmentDate">Date the user started working at the company</param>
        /// <param name="jobID">identifier of the user's current job</param>
        /// <param name="homeStore">store the user is assigned to</param>
        /// <param name="jobDescription">job description of the user</param>
        /// <param name="isActive">flag indicating an active user</param>
        /// <param name="hashKey">hashed password</param>
        /// <param name="passphrase">passphrase used to encrypt password</param>
        public User(long employeeID, string userName, string firstName, string middleName, string lastName, string address, string city, string state, int zipCode,
            string homePhone, string cellPhone, string email, double payRate, DateTime employmentDate, Enums.AccessLevel accessLevel, string homeStore, string jobDescription, bool isActive,
            string hashKey, string passphrase)
        {
            this.EmployeeID = employeeID;
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
            this.EmailAddress = email;
            this.PayRate = payRate;
            this.EmploymentDate = employmentDate;
            this.AccessLevel = accessLevel;
            this.HomeStore = homeStore;
            this.JobDescription = jobDescription;
            this.IsActive = isActive;
            this.HashKey = hashKey;
            this.Passphrase = passphrase;
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
                case "EmployeeID":
                    error = this.ValidateID();
                    break;
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
                case "EmailAddress":
                    error = this.ValidateEmail();
                    break;
                case "PayRate":
                    error = this.ValidatePayRate();
                    break;
                case "HomeStore":
                    error = this.ValidateHomeStore();
                    break;
                case "JobDescription":
                    error = this.ValidateJobDescription();
                    break;
                case "HashKey":
                    error = this.ValidateHashKey();
                    break;
                case "Passphrase":
                    error = this.ValidatePassphrase();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on User: " + propertyName);
                    break;
            }
            return error;
        }

        /// <summary>
        /// Validate the user ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateID()
        {
            if (this.EmployeeID < 0)
                return Messages.InvalidID;
            return null;
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
        /// Validate the first name
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateFirstName()
        {
            return IsEmpty(this.FirstName) ? Messages.InvalidName : null;
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
        /// Validate the last name
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateLastName()
        {
            return IsEmpty(this.LastName) ? Messages.InvalidName : null;
        }

        /// <summary>
        /// Validate the street address
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateAddress()
        {
            return IsEmpty(this.Address) ? Messages.InvalidAddress : null;
        }

        /// <summary>
        /// Validate the city name
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateCity()
        {
            return IsEmpty(this.City) ? Messages.InvalidCity : null;
        }

        /// <summary>
        /// Validate the state name
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateState()
        {
            return IsEmpty(this.State) ? Messages.InvalidState : null;
        }

        /// <summary>
        /// Validate the zip code
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateZipCode()
        {
            if (!IsValidZipCode(this.ZipCode))
                return Messages.InvalidZip;
            return null;
        }

        /// <summary>
        /// Validate the home phone number
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateHomePhone()
        {
            if (!IsValidPhoneNumber(this.HomePhone))
                return Messages.InvalidPhone;
            return null;
        }

        /// <summary>
        /// Validate the cell phone number
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateCellPhone()
        {
            if (!IsValidPhoneNumber(this.CellPhone))
                return Messages.InvalidPhone;
            return null;
        }

        /// <summary>
        /// Validate the email address
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateEmail()
        {
            if (!IsValidEmailAddress(this.EmailAddress))
                return Messages.InvalidEmailAddress;
            return null;
        }

        /// <summary>
        /// Validate the pay rate
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidatePayRate()
        {
            if (this.PayRate <= 0.0)
                return Messages.InvalidValue;
            return null;
        }

        /// <summary>
        /// Validate the home store
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateHomeStore()
        {
            return IsEmpty(this.HomeStore) ? Messages.InvalidStore : null;
        }

        /// <summary>
        /// Validate the job description
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateJobDescription()
        {
            return IsEmpty(this.JobDescription) ? Messages.InvalidDescription : null;
        }

        /// <summary>
        /// Validate the hash key
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateHashKey()
        {
            return IsEmpty(this.HashKey) ? Messages.InvalidHashKey : null;
        }

        /// <summary>
        /// Validate the passphrase
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidatePassphrase()
        {
            return IsEmpty(this.Passphrase) ? Messages.InvalidPassphrase : null;
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
        /// Checks a zip code for a valid pattern
        /// </summary>
        /// <param name="zip">integer for the zip code</param>
        /// <returns></returns>
        private bool IsValidZipCode(int zip)
        {
            if (zip < 0)
                return false;
            // Zip code pattern must have exactly 5 digits
            string pattern = @"^[0-9]{5}$";
            return Regex.IsMatch(zip.ToString(), pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Checks a phone number for a valid pattern (10 digits long, does not start with 0)
        /// </summary>
        /// <param name="number">string for the phone number</param>
        /// <returns>bool value indicating if the phone number is valid</returns>
        private bool IsValidPhoneNumber(string number)
        {
            if (this.IsEmpty(number))
                return false;
            // Phone number must have exactly 10 digits
            string pattern = @"^[1-9]{1}[0-9]{9}$";
            return Regex.IsMatch(number, pattern, RegexOptions.IgnoreCase);
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

        public override string ToString()
        {
            return string.Format("{0} {1}", this.FirstName, this.LastName);
        }

        public override bool Equals(object obj)
        {
            if (obj is User)
            {
                User other = obj as User;
                return this.EmployeeID.Equals(other.EmployeeID) &&
                    this.UserName.Equals(other.UserName) &&
                    this.FirstName.Equals(other.FirstName) &&
                    this.MiddleName.Equals(other.MiddleName) &&
                    this.LastName.Equals(other.LastName) &&
                    this.Address.Equals(other.Address) &&
                    this.City.Equals(other.City) &&
                    this.State.Equals(other.State) &&
                    this.ZipCode.Equals(other.ZipCode) &&
                    this.HomePhone.Equals(other.HomePhone) &&
                    this.CellPhone.Equals(other.CellPhone) &&
                    this.EmailAddress.Equals(other.EmailAddress) &&
                    this.PayRate.Equals(other.PayRate) &&
                    this.EmploymentDate.Equals(other.EmploymentDate) &&
                    this.AccessLevel.Equals(other.AccessLevel) &&
                    this.HomeStore.Equals(other.HomeStore) &&
                    this.JobDescription.Equals(other.JobDescription) &&
                    this.Passphrase.Equals(other.Passphrase) &&
                    this.HashKey.Equals(other.HashKey);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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
                return this.EmployeeID;
            }
            set
            {
                this.EmployeeID = value;
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
            "EmployeeID",
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
            "EmailAddress",
            "PayRate",
            "HomeStore",
            "JobDescription",
            "HashKey",
            "Passphrase"
        };

        /// <summary>
        /// Unique indentifier
        /// </summary>
        public long EmployeeID { get; set; }

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
        /// Email address of the user
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Pay rate of the user
        /// </summary>
        public double PayRate { get; set; }

        /// <summary>
        /// Time the user has worked at the company
        /// </summary>
        public DateTime EmploymentDate { get; set; }

        /// <summary>
        /// Identifier of the user's current job
        /// </summary>
        public Enums.AccessLevel AccessLevel { get; set; }

        /// <summary>
        /// Store location the user is assigned to
        /// </summary>
        public string HomeStore { get; set; }

        /// <summary>
        /// Job description of the user
        /// </summary>
        public string JobDescription { get; set; }

        /// <summary>
        /// Flag indicating the user is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Hashed password
        /// </summary>
        public string HashKey { get; set; }

        /// <summary>
        /// Passphrase used to encrypt password
        /// </summary>
        public string Passphrase { get; set; }

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