using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CS499.TCMS.Model
{
    /// <summary>
    /// Holds all relevant data for maintenance record details
    /// </summary>
    public class MaintenanceRecordDetails : IModel
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="detailID">unique identifier</param>
        /// <param name="maintenanceID">identifier of the maintenance record associated with this detail</param>
        /// <param name="employeeID">identifier of the employee associated with this detail</param>
        /// <param name="repairDescription">summary description of the repairs performed</param>
        /// <param name="repairDate">date the repair was performed</param>
        public MaintenanceRecordDetails
            (long detailID, long maintenanceID, long employeeID, string repairDescription, DateTime repairDate)
        {
            this.DetailID = detailID;
            this.MaintenanceID = maintenanceID;
            this.EmployeeID = employeeID;
            this.RepairDescription = repairDescription;
            this.RepairDate = repairDate;
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
                case "DetailID":
                    error = this.ValidateDetailID();
                    break;
                case "MaintenanceID":
                    error = this.ValidateMaintenanceID();
                    break;
                case "EmployeeID":
                    error = this.ValidateEmployeeID();
                    break;
                case "RepairDescription":
                    error = this.ValidateRepairDescription();
                    break;
                case "RepairDate":
                    error = this.ValidateRepairDate();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on MaintenanceRecordDetails: " + propertyName);
                    break;
            }
            return error;
        }

        /// <summary>
        /// Validate the detail ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateDetailID()
        {
            if (this.DetailID < 0)
                return Messages.InvalidID;
            return null;
        }

        /// <summary>
        /// Validate the maintenance record ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateMaintenanceID()
        {
            if (this.MaintenanceID < 0)
                return Messages.InvalidID;
            return null;
        }

        /// <summary>
        /// Validate the employee ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateEmployeeID()
        {
            if (this.EmployeeID < 0)
                return Messages.InvalidID;
            else
                return null;
        }

        /// <summary>
        /// Validate the repair date
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateRepairDate()
        {
            if (this.RepairDate.CompareTo(DateTime.Now) > 0)
                return Messages.InvalidDate;
            else
                return null;
        }

        /// <summary>
        /// Validate the repairs description
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateRepairDescription()
        {
            return IsEmpty(this.RepairDescription) ? Messages.InvalidDescription : null;
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
                return this.DetailID;
            }
            set
            {
                this.DetailID = value;
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
            "DetailID",
            "MaintenanceID",
            "EmployeeID",
            "RepairDescription",
            "RepairDate"
        };

        /// <summary>
        /// Unique identifier
        /// </summary>
        public long DetailID { get; set; }

        /// <summary>
        /// Identifier of the maintenance record associated with this detail
        /// </summary>
        public long MaintenanceID { get; set; }

        /// <summary>
        /// Identifier of the employee associated with this detail
        /// </summary>
        public long EmployeeID { get; set; }

        /// <summary>
        /// Summary description of the repairs performed
        /// </summary>
        public string RepairDescription { get; set; }

        /// <summary>
        /// Date the repair was performed
        /// </summary>
        public DateTime RepairDate { get; set; }

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