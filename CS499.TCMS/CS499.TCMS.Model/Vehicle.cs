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
        public Vehicle(long id, string brand, int year, string model, string vehicleType)
        {
            this.ID = id;
            this.Brand = brand;
            this.Year = year;
            this.Model = model;
            this.VehicleType = vehicleType;
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
                case "Brand":
                    error = this.ValidateBrand();
                    break;
                case "Model":
                    error = this.ValidateModel();
                    break;
                case "VehicleType":
                    error = this.ValidateVehicleType();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on Vehicle: " + propertyName);
                    break;
            }
            return error;
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
        /// Array with property names to validate
        /// </summary>
        static readonly string[] ValidatedProperties =
        {
            "Brand",
            "Model",
            "VehicleType"
        };

        /// <summary>
        /// Unique identifier
        /// </summary>
        public long ID { get; set; }
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