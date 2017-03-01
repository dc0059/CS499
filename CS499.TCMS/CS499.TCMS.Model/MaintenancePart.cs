using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CS499.TCMS.Model
{
    /// <summary>
    /// Holds all relevant data for maintenance parts
    /// </summary>
    public class MaintenancePart : IModel
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="mainPartID">unique identifier</param>
        /// <param name="quantity">quantity of the part</param>
        /// <param name="maintenanceID">identifier of the maintenance record the maintenance part is part of</param>
        public MaintenancePart(long mainPartID, int quantity, long maintenanceID, long partID)
        {
            this.MaintenancePartID = mainPartID;
            this.Quantity = quantity;
            this.MaintenanceID = maintenanceID;
            this.PartID = partID;
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
                case "MaintenancePartID":
                    error = this.ValidateMaintenancePartID();
                    break;
                case "Quantity":
                    error = this.ValidateQuantity();
                    break;
                case "MaintenanceID":
                    error = this.ValidateMaintenanceID();
                    break;
                case "PartID":
                    error = this.ValidatePartID();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on MaintenancePart: " + propertyName);
                    break;
            }
            return error;
        }

        /// <summary>
        /// Validate the maintenance part ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateMaintenancePartID()
        {
            if (this.MaintenancePartID < 0)
                return Messages.InvalidID;
            else
                return null;
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
        /// Validate the maintenance ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateMaintenanceID()
        {
            if (this.MaintenanceID < 0)
                return Messages.InvalidID;
            else
                return null;
        }

        /// <summary>
        /// Validate the part ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidatePartID()
        {
            if (this.PartID < 0)
                return Messages.InvalidID;
            else
                return null;
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
                return this.MaintenancePartID;
            }
            set
            {
                this.MaintenancePartID = value;
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
            "MaintenancePartID",
            "Quantity",
            "DetailID",
            "PartID"
        };

        /// <summary>
        /// Unique identifier
        /// </summary>
        public long MaintenancePartID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Identifier of the maintenance record associate
        /// </summary>
        public long MaintenanceID { get; set; }
        /// <summary>
        /// Identifier of the part this maintenance part is associated with
        /// </summary>
        public long PartID { get; set; }

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
