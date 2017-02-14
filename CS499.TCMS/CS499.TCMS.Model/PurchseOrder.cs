using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CS499.TCMS.Model
{
    /// <summary>
    /// Holds all relevant data for the purchase order
    /// </summary>
    public class PurchaseOrder : IModel
    {
        #region Constructor

        public PurchaseOrder(long orderID, long orderNumber, string destination)
        {
            this.OrderID = orderID;
            this.OrderNumber = orderNumber;
            this.Destination = destination;
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
                case "OrderNumber":
                    error = this.ValidateOrderNumber();
                    break;
                case "Destination":
                    error = this.ValidateDestination();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on MaintenanceRecordDetails: " + propertyName);
                    break;
            }
            return error;
        }

        /// <summary>
        /// Validate the order number
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateOrderNumber()
        {
            if (this.OrderNumber < 0)
                return Messages.InvalidID;
            else
                return null;
        }

        /// <summary>
        /// Validate the order destination
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateDestination()
        {
            return IsEmpty(this.Destination) ? Messages.InvalidCompany : null;
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
                return this.OrderID;
            }
            set
            {
                this.OrderID = value;
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
            "OrderNumber",
            "Destination",
        };

        /// <summary>
        /// Unique identifier
        /// </summary>
        public long OrderID { get; set; }
        /// <summary>
        /// Number associated with part, not related to identifier
        /// </summary>
        public long OrderNumber { get; set; }
        /// <summary>
        /// Summary description of the part
        /// </summary>
        public string Destination { get; set; }

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