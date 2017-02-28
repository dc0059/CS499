using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CS499.TCMS.Model
{
    /// <summary>
    /// Holds all relevant data for a vehicle
    /// </summary>
    public class Vehicle : IModel
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="id">unique identifier</param>
        /// <param name="brand">brand name of the vehicle</param>
        /// <param name="year">year of the vehicle</param>
        /// <param name="model">model of the vehicle</param>
        /// <param name="vehicleType">type of vehicle</param>
        /// <param name="capacity">weight capacity of the vehicle</param>
        public Vehicle(long vehicleID, string brand, int year, string model, string vehicleType, int capacity)
        {
            this.VehicleID = vehicleID;
            this.Brand = brand;
            this.Year = year;
            this.Model = model;
            this.VehicleType = vehicleType;
            this.Capacity = capacity;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get validation errors
        /// </summary>
        /// <param name="propertyName">name of the property to validate</param>
        /// <returns>string for the error found if any</returns>
        private string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;
            string error = null;
            switch (propertyName)
            {
                case "VehicleID":
                    error = this.ValidateVehicleID();
                    break;
                case "Brand":
                    error = this.ValidateBrand();
                    break;
                case "Year":
                    error = this.ValidateYear();
                    break;
                case "Model":
                    error = this.ValidateModel();
                    break;
                case "VehicleType":
                    error = this.ValidateVehicleType();
                    break;
                case "Capacity":
                    error = this.ValidateCapacity();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on Vehicle: " + propertyName);
                    break;
            }
            return error;
        }

        /// <summary>
        /// Validate the equipment ID
        /// </summary>
        /// <returns></returns>
        private string ValidateVehicleID()
        {
            if (this.VehicleID < 0)
                return Messages.InvalidID;
            return null;
        }

        /// <summary>
        /// Validate the brand name
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateBrand()
        {
            return IsEmpty(this.Brand) ? Messages.InvalidBrand : null;
        }

        /// <summary>
        /// Validate the model name
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateModel()
        {
            return IsEmpty(this.Model) ? Messages.InvalidModel : null;
        }

        /// <summary>
        /// Validate the year
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateYear()
        {
            if (!IsValidYear(this.Year))
                return Messages.InvalidYear;
            return null;
        }

        /// <summary>
        /// Validate the vehicle type
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateVehicleType()
        {
            return IsEmpty(this.VehicleType) ? Messages.InvalidVehicleType : null;
        }

        /// <summary>
        /// Check to make sure the string is not null or empty
        /// </summary>
        /// <param name="value">string value to test</param>
        /// <returns>bool value if the string is null or empty</returns>
        private bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Check to make sure the year is in the correct format
        /// </summary>
        /// <param name="year"></param>
        /// <returns>bool value if the value is in the correct format</returns>
        private bool IsValidYear(int year)
        {
            if (year < 0)
                return false;
            // Year pattern must be exactly 4 digits
            string pattern = @"^[0-9]{4}$";
            return Regex.IsMatch(year.ToString(), pattern);
        }

        /// <summary>
        /// Validate the capacity
        /// </summary>
        /// <returns></returns>
        private string ValidateCapacity()
        {
            if (this.Capacity < 0)
                return Messages.InvalidID;
            return null;
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
                return this.VehicleID;
            }
            set
            {
                this.VehicleID = value;
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
        /// Array with property names to validate
        /// </summary>
        static readonly string[] ValidatedProperties =
        {
            "VehicleID",
            "Brand",
            "Year",
            "Model",
            "VehicleType",
            "Capacity"
        };

        /// <summary>
        /// Unique identifier
        /// </summary>
        public long VehicleID { get; set; }
        /// <summary>
        /// Brand name of the vehicle
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// Year of the vehicle
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Model of the vehicle
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Type of vehicle
        /// </summary>
        public string VehicleType { get; set; }
        /// <summary>
        /// Weight capacity of the vehicle
        /// </summary>
        public int Capacity { get; set; }

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