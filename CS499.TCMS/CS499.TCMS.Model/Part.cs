using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CS499.TCMS.Model
{
    /// <summary>
    /// Holds all relevant data for maintenance parts
    /// </summary>
    public class Part : IModel
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="partID">unique identifier</param>
        /// <param name="partNumber">number of the part</param>
        /// <param name="partDescription">summary description of the part</param>
        /// <param name="quantity">quantity of the part</param>
        /// <param name="unitMeasurement">the unit of measurement associated with the part</param>
        public Part(long partID, long partNumber, string partDescription, int quantity, string unitMeasurement)
        {
            this.PartID = partID;
            this.PartNumber = partNumber;
            this.PartDescription = partDescription;
            this.Quantity = quantity;
            this.UnitMeasurement = unitMeasurement;
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
                case "PartNumber":
                    error = this.ValidatePartNumber();
                    break;
                case "PartDescription":
                    error = this.ValidatePartDescription();
                    break;
                case "Quantity":
                    error = this.ValidateQuantity();
                    break;
                case "UnitMeasurement":
                    error = this.ValidateUnitMeasurement();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on MaintenanceRecordDetails: " + propertyName);
                    break;
            }
            return error;
        }

        /// <summary>
        /// Validate the part number
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidatePartNumber()
        {
            if (this.PartNumber < 0)
                return Messages.InvalidID;
            else
                return null;
        }

        /// <summary>
        /// Validate the part description
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidatePartDescription()
        {
            return IsEmpty(this.PartDescription) ? Messages.InvalidDescription : null;
        }

        /// <summary>
        /// Validate the quantity
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateQuantity()
        {
            if (this.Quantity < 0)
                return Messages.InvalidValue;
            return null;
        }

        /// <summary>
        /// Validate the unit of measurement
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateUnitMeasurement()
        {
            return IsEmpty(this.UnitMeasurement) ? Messages.InvalidUnit : null;
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
                return this.PartID;
            }
            set
            {
                this.PartID = value;
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
            "PartID",
            "PartNumber",
            "PartDescription",
            "Quantity",
            "UnitMeasurement"
        };

        /// <summary>
        /// Unique identifier
        /// </summary>
        public long PartID { get; set; }
        /// <summary>
        /// Number associated with part, not related to identifier
        /// </summary>
        public long PartNumber { get; set; }
        /// <summary>
        /// Summary description of the part
        /// </summary>
        public string PartDescription { get; set; }
        /// <summary>
        /// Quantity of the part
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// The unit of measurement associated with the part
        /// </summary>
        public string UnitMeasurement { get; set; }

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
