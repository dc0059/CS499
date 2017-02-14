using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CS499.TCMS.Model
{
    /// <summary>
    /// Holds all relevant data for a shipment
    /// </summary>
    public class Shipment : IModel
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="shipmentID">unique identifier</param>
        /// <param name="shipmentType">incoming or outgoing shipment</param>
        /// <param name="sourceCompany">name of the company sending the shipment</param>
        /// <param name="sourceAddress">address of the company sending the shipment</param>
        /// <param name="destinationCompany">name of the company receiving the shipment</param>
        /// <param name="destinationAddress">address of the company receiving the shipment</param>
        /// <param name="truckID">identifier of the vehicle associated with this shipment</param>
        /// <param name="manifestID">identifier of the shipment manifest</param>
        /// <param name="purchaseID">identifier of the purchase</param>
        /// <param name="shippingCost">total cost of the shipment</param>
        public Shipment(long shipmentID, string shipmentType, string sourceCompany, string sourceAddress, string destinationCompany, string destinationAddress, long truckID,
            long manifestID, long purchaseID, double shippingCost)
        {
            this.ShipmentID = shipmentID;
            this.ShipmentType = shipmentType;
            this.SourceCompany = sourceCompany;
            this.SourceAddress = sourceAddress;
            this.DestinationCompany = destinationCompany;
            this.DestinationAddress = destinationAddress;
            this.TruckID = truckID;
            this.ManifestID = manifestID;
            this.PurchaseID = purchaseID;
            this.ShippingCost = shippingCost;
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
                case "ShipmentType":
                    error = this.ValidateShipmentType();
                    break;
                case "SourceCompany":
                    error = this.ValidateSourceCompany();
                    break;
                case "SourceAddress":
                    error = this.ValidateSourceAddress();
                    break;
                case "DestinationCompany":
                    error = this.ValidateSourceCompany();
                    break;
                case "DestinationAddress":
                    error = this.ValidateSourceAddress();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on Shipment: " + propertyName);
                    break;
            }
            return error;
        }

        /// <summary>
        /// Validate the shipment type
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateShipmentType()
        {
            return IsEmpty(this.ShipmentType) ? Messages.InvalidShipmentType : null;
        }

        /// <summary>
        /// Validate the source company
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateSourceCompany()
        {
            return IsEmpty(this.SourceCompany) ? Messages.InvalidCompany : null;
        }

        /// <summary>
        /// Validate the source address
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateSourceAddress()
        {
            return IsEmpty(this.SourceAddress) ? Messages.InvalidAddress : null;
        }

        /// <summary>
        /// Validate the source company
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateDestinationCompany()
        {
            return IsEmpty(this.DestinationCompany) ? Messages.InvalidCompany : null;
        }

        /// <summary>
        /// Validate the source address
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateDestinationAddress()
        {
            return IsEmpty(this.DestinationAddress) ? Messages.InvalidAddress : null;
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
                return this.ShipmentID;
            }
            set
            {
                this.ShipmentID = value;
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
            "ShipmentType",
            "SourceCompany",
            "SourceAddress",
            "DestinationCompany",
            "DestinationAddress",
            "ShippingCost"
        };

        /// <summary>
        /// unique identifier
        /// </summary>
        long ShipmentID { get; set; }
        /// <summary>
        /// unique identifier
        /// </summary>
        string ShipmentType { get; set; }
        /// <summary>
        /// unique identifier
        /// </summary>
        string SourceCompany { get; set; }
        /// <summary>
        /// unique identifier
        /// </summary>
        string SourceAddress { get; set; }
        /// <summary>
        /// unique identifier
        /// </summary>
        string DestinationCompany { get; set; }
        /// <summary>
        /// unique identifier
        /// </summary>
        string DestinationAddress { get; set; }
        /// <summary>
        /// unique identifier
        /// </summary>
        long TruckID { get; set; }
        /// <summary>
        /// unique identifier
        /// </summary>
        long ManifestID { get; set; }
        /// <summary>
        /// unique identifier
        /// </summary>
        long PurchaseID { get; set; }
        /// <summary>
        /// unique identifier
        /// </summary>
        double ShippingCost { get; set; }

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