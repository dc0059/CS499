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
        /// <param name="repairID">unique identifier</param>
        /// <param name="employeeID">identifier of the employee associated with this maintenance record</param>
        /// <param name="repairDescription">summary description of the repairs performed</param>
        public MaintenanceRecordDetails
            (long repairID, long employeeID, string repairDescription)
        {
            this.RepairID = repairID;
            this.EmployeeID = employeeID;
            this.RepairDescription = repairDescription;
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
                case "EmployeeID":
                    error = this.ValidateEmployeeID();
                    break;
                case "RepairDescription":
                    error = this.ValidateRepairDescription();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on MaintenanceRecordDetails: " + propertyName);
                    break;
            }
            return error;
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
                return this.RepairID;
            }
            set
            {
                this.RepairID = value;
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
            "EmployeeID",
            "RepairDescription"
        };

        /// <summary>
        /// Unique identifier
        /// </summary>
        public long RepairID { get; set; }
        /// <summary>
        /// Identifier of the employee associated with this maintenance record
        /// </summary>
        public long EmployeeID { get; set; }
        /// <summary>
        /// Summary description of the repairs performed
        /// </summary>
        public string RepairDescription { get; set; }

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