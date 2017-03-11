using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CS499.TCMS.Model
{
    /// <summary>
    /// Holds all relevant data for an employee payroll
    /// </summary>
    public class Payroll : IModel
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="payrollID">unique identifier</param>
        /// <param name="employeeID">identifier of the employee associated with this payment</param>
        /// <param name="paymentDate">date the payment was made</param>
        /// <param name="payment">amount of the payment</param>
        public Payroll(long payrollID, long employeeID, DateTime paymentDate, double payment)
        {
            this.PayrollID = payrollID;
            this.EmployeeID = employeeID;
            this.PaymentDate = paymentDate;
            this.Payment = payment;
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
                case "PayrollID":
                    error = this.ValidatePayrollID();
                    break;
                case "EmployeeID":
                    error = this.ValidateEmployeeID();
                    break;
                case "PaymentDate":
                    error = this.ValidatePaymentDate();
                    break;
                case "Payment":
                    error = this.ValidatePayment();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on Payroll: " + propertyName);
                    break;
            }
            return error;
        }

        /// <summary>
        /// Validate the payroll ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidatePayrollID()
        {
            if (this.PayrollID < 0)
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
            return null;
        }

        /// <summary>
        /// Validate the payment date
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidatePaymentDate()
        {
            if (this.PaymentDate.CompareTo(DateTime.Now) > 0)
                return Messages.InvalidDate;
            return null;
        }

        /// <summary>
        /// Validate the payment amount
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidatePayment()
        {
            if (this.Payment < 0.0)
                return Messages.InvalidValue;
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
                return this.PayrollID;
            }
            set
            {
                this.PayrollID = value;
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
            "PayrollID",
            "EmployeeID",
            "PaymentDate",
            "Payment"
        };

        /// <summary>
        /// Unique identifier
        /// </summary>
        public long PayrollID { get; set; }

        /// <summary>
        /// Identifier of the employee associated with this payment
        /// </summary>
        public long EmployeeID { get; set; }

        /// <summary>
        /// Date the payment was made
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Amount of the payment
        /// </summary>
        public double Payment { get; set; }

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