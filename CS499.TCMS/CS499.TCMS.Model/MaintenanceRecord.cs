using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CS499.TCMS.Model
{
    /// <summary>
    /// Holds all relevant data for a maintenance record
    /// </summary>
    public class MaintenanceRecord : IModel
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="maintenanceID">unique identifier</param>
        /// <param name="vehicleID">identifier of the vehicle associated with this maintenance record</param>
        /// <param name="maintenanceDate">date of the most recent maintenance</param>
        /// <param name="maintenanceDescription">summary description of the maintenance performed</param>
        public MaintenanceRecord(long maintenanceID, long vehicleID, DateTime maintenanceDate, string maintenanceDescription)
        {
            this.MaintenanceID = maintenanceID;
            this.VehicleID = vehicleID;
            this.MaintenanceDate = maintenanceDate;
            this.MaintenanceDescription = maintenanceDescription;
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
                case "MaintenanceID":
                    error = this.ValidateMaintenanceID();
                    break;
                case "VehicleID":
                    error = this.ValidateVehicleID();
                    break;
                case "MaintenanceDate":
                    error = this.ValidateMaintenanceDate();
                    break;
                case "MaintenanceDescription":
                    error = this.ValidateMaintenanceDescription();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on MaintenanceRecord: " + propertyName);
                    break;
            }
            return error;
        }

        /// <summary>
        /// Validate the record ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateMaintenanceID()
        {
            if (this.MaintenanceID < 0)
                return Messages.InvalidID;
            return null;
        }

        /// <summary>
        /// Validate the vehicle ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateVehicleID()
        {
            if (this.VehicleID < 0)
                return Messages.InvalidID;
            return null;
        }

        /// <summary>
        /// Validate the date of maintenance
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateMaintenanceDate()
        {
            if (this.MaintenanceDate.CompareTo(DateTime.Now) > 0)
                return Messages.InvalidDate;
            return null;
        }

        /// <summary>
        /// Validate the maintenance description
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateMaintenanceDescription()
        {
            return IsEmpty(this.MaintenanceDescription) ? Messages.InvalidDescription : null;
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
                return this.MaintenanceID;
            }
            set
            {
                this.MaintenanceID = value;
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
            "MaintenanceID",
            "VehicleID",
            "MaintenanceDate",
            "MaintenanceDescription"
        };

        /// <summary>
        /// Unique identifier
        /// </summary>
        public long MaintenanceID { get; set; }
        /// <summary>
        /// Identifier of the vehicle asssociated with this maintenance record
        /// </summary>
        public long VehicleID { get; set; }
        /// <summary>
        /// Date the maintenance was performed
        /// </summary>
        public DateTime MaintenanceDate { get; set; }
        /// <summary>
        /// Summary description of the maintenance performed
        /// </summary>
        public string MaintenanceDescription { get; set; }

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