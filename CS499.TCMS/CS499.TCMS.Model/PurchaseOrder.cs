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

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="orderID">unique identifier</param>
        /// <param name="orderNumber">number associated with the order, not related to identifier</param>
        /// <param name="sourceID">identifier of the source company</param>
        /// <param name="destinationID">identifier of the destination company</param>
        /// <param name="manifestID">identifier of the shipping manifest this purchase order belongs to</param>
        /// <param name="paymentMade">flag indicating whether the payment has been made or received</param>
        public PurchaseOrder(long orderID, long orderNumber, long sourceID, long destinationID, long manifestID, bool paymentMade)
        {
            this.OrderID = orderID;
            this.OrderNumber = orderNumber;
            this.SourceID = sourceID;
            this.DestinationID = destinationID;
            this.ManifestID = manifestID;
            this.PaymentMade = paymentMade;
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
                case "OrderID":
                    error = this.ValidateOrderID();
                    break;
                case "OrderNumber":
                    error = this.ValidateOrderNumber();
                    break;
                case "SourceID":
                    error = this.ValidateSourceID();
                    break;
                case "DestinationID":
                    error = this.ValidateDestinationID();
                    break;
                case "ManifestID":
                    error = this.ValidateManifestID();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on PurchaseOrder: " + propertyName);
                    break;
            }
            return error;
        }

        /// <summary>
        /// Validate the order ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateOrderID()
        {
            if (this.OrderID < 0)
                return Messages.InvalidID;
            return null;
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
        /// Validate the source ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateSourceID()
        {
            if (this.SourceID < 0)
                return Messages.InvalidID;
            return null;
        }

        /// <summary>
        /// Validate the destination ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateDestinationID()
        {
            if (this.DestinationID < 0)
                return Messages.InvalidID;
            return null;
        }

        /// <summary>
        /// Validate the manifest ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateManifestID()
        {
            if (this.ManifestID < 0)
                return Messages.InvalidID;
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

        public override string ToString()
        {
            return string.Format("Order #{0} from {1} to {2}", this.OrderNumber, this.SourceID, this.DestinationID);
        }

        public override bool Equals(object obj)
        {
            if(obj is PurchaseOrder)
            {
                PurchaseOrder other = obj as PurchaseOrder;
                return this.OrderID.Equals(other.OrderID) &&
                    this.OrderNumber.Equals(other.OrderNumber) &&
                    this.SourceID.Equals(other.SourceID) &&
                    this.DestinationID.Equals(other.DestinationID) &&
                    this.ManifestID.Equals(other.ManifestID) &&
                    this.PaymentMade.Equals(other.PaymentMade);
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
            "OrderID",
            "OrderNumber",
            "SourceID",
            "DestinationID",
            "ManifestID"
        };

        /// <summary>
        /// Unique identifier
        /// </summary>
        public long OrderID { get; set; }
        /// <summary>
        /// Number associated with order, not related to identifier
        /// </summary>
        public long OrderNumber { get; set; }
        /// <summary>
        /// Identifier of the source company
        /// </summary>
        public long SourceID { get; set; }
        /// <summary>
        /// Identifier of the destination company
        /// </summary>
        public long DestinationID { get; set; }
        /// <summary>
        /// Identifier of the shipping manifest this purchase order belongs to
        /// </summary>
        public long ManifestID { get; set; }

        /// <summary>
        /// Flag indicating whether payment has been made or received
        /// </summary>
        public bool PaymentMade { get; set; }

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