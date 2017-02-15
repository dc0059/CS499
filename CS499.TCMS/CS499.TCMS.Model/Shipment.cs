using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;

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
        /// <param name="vehicleID">identifier for the vehicle carrying shipment</param>
        /// <param name="departureTime">time the shipment was sent out</param>
        /// <param name="eta">estimated time of arrival</param>
        /// <param name="arrived">flag indicating the shipment has arrived</param>
        /// <param name="drivers">list of drivers assigned to the shipment</param>
        /// <param name="manifestID">identifier of the shipment manifest</param>
        /// <param name="purchaseID">identifier of the purchase order</param>
        /// <param name="shippingCost">total cost of the shipment</param>
        public Shipment(long shipmentID, string shipmentType, string sourceCompany, string sourceAddress, string destinationCompany, string destinationAddress, long vehicleID,
            DateTime departureTime, DateTime eta, bool arrived, List<User> drivers, long manifestID, long purchaseID, double shippingCost)
        {
            this.ShipmentID = shipmentID;
            this.ShipmentType = shipmentType;
            this.SourceCompany = sourceCompany;
            this.SourceAddress = sourceAddress;
            this.DestinationCompany = destinationCompany;
            this.DestinationAddress = destinationAddress;
            this.VehicleID = vehicleID;
            this.DepartureTime = departureTime;
            this.ETA = eta;
            this.Arrived = arrived;
            this.Drivers = drivers;
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
                case "VehicleID":
                    error = this.ValidateVehicleID();
                    break;
                case "DepartureTime":
                    error = this.ValidateDepartureTime();
                    break;
                case "ManifestID":
                    error = this.ValidateManifestID();
                    break;
                case "PurchaseID":
                    error = this.ValidatePurchaseID();
                    break;
                case "ShippingCost":
                    error = this.ValidateShippingCost();
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

        private string ValidateVehicleID()
        {
            if (this.VehicleID < 0)
                return Messages.InvalidID;
            return null;
        }

        private string ValidateDepartureTime()
        {
            if (this.DepartureTime.CompareTo(DateTime.Now) > 0)
                return Messages.InvalidDate;
            return null;
        }

        private string ValidateManifestID()
        {
            if (this.ManifestID < 0)
                return Messages.InvalidID;
            return null;
        }

        private string ValidatePurchaseID()
        {
            if (this.PurchaseID < 0)
                return Messages.InvalidID;
            return null;
        }

        private string ValidateShippingCost()
        {
            if (this.ShippingCost < 0.0)
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
            "VehicleID",
            "DepartureTime",
            "ManifestID",
            "PurchaseID",
            "ShippingCost"
        };

        /// <summary>
        /// unique identifier
        /// </summary>
        public long ShipmentID { get; set; }
        /// <summary>
        /// Incoming or outgoing shipment
        /// </summary>
        public string ShipmentType { get; set; }
        /// <summary>
        /// Name of the company sending out the shipment
        /// </summary>
        public string SourceCompany { get; set; }
        /// <summary>
        /// Address of the company sending out the shipment
        /// </summary>
        public string SourceAddress { get; set; }
        /// <summary>
        /// Name of the company receiving the shipment
        /// </summary>
        public string DestinationCompany { get; set; }
        /// <summary>
        /// Address of the company receiving the shipment
        /// </summary>
        public string DestinationAddress { get; set; }
        /// <summary>
        /// Identifier for the vehicle carrying the shipment
        /// </summary>
        public long VehicleID { get; set; }
        /// <summary>
        /// Time the shipment was sent out
        /// </summary>
        public DateTime DepartureTime { get; set; }
        /// <summary>
        /// Estimated time of arrival
        /// </summary>
        public DateTime ETA { get; set; }
        /// <summary>
        /// Flag indicating the shipment arrived
        /// </summary>
        public bool Arrived { get; set; }
        /// <summary>
        /// List of drivers assigned to the shipment
        /// </summary>
        public List<User> Drivers { get; set; }
        /// <summary>
        /// Identifier for the shipment manifest
        /// </summary>
        public long ManifestID { get; set; }
        /// <summary>
        /// Identifier for the purchase order
        /// </summary>
        public long PurchaseID { get; set; }
        /// <summary>
        /// Total cost of the shipment
        /// </summary>
        public double ShippingCost { get; set; }

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