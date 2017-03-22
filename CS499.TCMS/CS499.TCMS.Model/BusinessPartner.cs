using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CS499.TCMS.Model
{
    /// <summary>
    /// This class will hold the relevant data for a user
    /// </summary>
    public class BusinessPartner : IModel
    {
 
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="companyID">unique identifier</param>
        /// <param name="companyName">name of the company</param>
        /// <param name="address">street address of the company</param>
        /// <param name="city">city of the company</param>
        /// <param name="state">state of the company</param>
        /// <param name="zipCode">zipCode of the company</param>
        /// <param name="phoneNumber">phone number of the company</param>
        public BusinessPartner(long companyID, string companyName, string address, string city, string state, int zipCode, string phoneNumber)
        {
            this.CompanyID = companyID;
            this.CompanyName = companyName;
            this.Address = address;
            this.City = city;
            this.State = state;
            this.ZipCode = zipCode;
            this.PhoneNumber = phoneNumber;
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
                case "CompanyID":
                    error = this.ValidateID();
                    break;
                case "CompanyName":
                    error = this.ValidateCompanyName();
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
                case "PhoneNumber":
                    error = this.ValidatePhone();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on BusinessPartner: " + propertyName);
                    break;
            }
            return error;
        }

        /// <summary>
        /// Validate the company ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateID()
        {
            if (this.CompanyID < 0)
                return Messages.InvalidID;
            return null;
        }

        /// <summary>
        /// Validate the company name
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateCompanyName()
        {
            return IsEmpty(this.CompanyName) ? Messages.InvalidCompany : null;
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
        private string ValidatePhone()
        {
            if (!IsValidPhoneNumber(this.PhoneNumber))
                return Messages.InvalidPhone;
            return null;
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
            return Regex.IsMatch(zip.ToString(), pattern);
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
            string pattern = @"^[1-9]{1}[0-9]{9}$";
            return Regex.IsMatch(number, pattern);
        }

        public override string ToString()
        {
            return string.Format("{0}", this.CompanyName);
        }

        public override bool Equals(object obj)
        {
            if(obj is BusinessPartner)
            {
                BusinessPartner other = obj as BusinessPartner;
                return this.CompanyID.Equals(other.CompanyID) &&
                    this.CompanyName.Equals(other.CompanyName) &&
                    this.Address.Equals(other.Address) &&
                    this.City.Equals(other.City) &&
                    this.State.Equals(other.State) &&
                    this.ZipCode.Equals(other.ZipCode) &&
                    this.PhoneNumber.Equals(other.PhoneNumber);
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
                return this.CompanyID;
            }
            set
            {
                this.CompanyID = value;
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
            "CompanyID",
            "CompanyName",
            "Address",
            "City",
            "State",
            "ZipCode",
            "PhoneNumber",
        };

        /// <summary>
        /// Unique indentifier
        /// </summary>
        public long CompanyID { get; set; }

        /// <summary>
        /// Name of the company
        /// </summary>
        public string CompanyName { get; set; }
        
        /// <summary>
        /// Street address of the company
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// City of the company
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// State of the company
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Zip code of the company
        /// </summary>
        public int ZipCode { get; set; }

        /// <summary>
        /// Phone number of the company
        /// </summary>
        public string PhoneNumber { get; set; }

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