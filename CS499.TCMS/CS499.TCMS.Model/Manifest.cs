using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace CS499.TCMS.Model
{
    /// <summary>
    /// Holds all relevant data for a shipping manifest
    /// </summary>
    public class Manifest : IModel
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="manifestID">unique identifier</param>
        /// <param name="shipmentType">type of shipment, incoming or outgoing</param>
        /// <param name="vehicleID">identifier of the vehicle transporting this shipment</param>
        /// <param name="departureTime">date and time the shipment was sent out</param>
        /// <param name="eta">estimated time of arrival</param>
        /// <param name="arrived">flag indicating the shipment arrived at its destination</param>
        /// <param name="shippingCost">total cost of the shipment</param>
        /// <param name="employeeID">identifier of the employee assigned to this manifest</param>
        public Manifest(long manifestID, string shipmentType, long vehicleID, DateTime departureTime, DateTime eta, bool arrived, double shippingCost, long employeeID)
        {
            this.ManifestID = manifestID;
            this.ShipmentType = shipmentType;
            this.VehicleID = vehicleID;
            this.DepartureTime = departureTime;
            this.ETA = eta;
            this.Arrived = arrived;
            this.ShippingCost = shippingCost;
            this.EmployeeID = employeeID;
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
                case "ManifestID":
                    error = this.ValidateManifestID();
                    break;
                case "ShipmentType":
                    error = this.ValidateShipmentType();
                    break;
                case "VehicleID":
                    error = this.ValidateVehicleID();
                    break;
                case "DepartureTime":
                    error = this.ValidateDepartureTime();
                    break;
                case "ETA":
                    error = this.ValidateETA();
                    break;
                case "ShippingCost":
                    error = this.ValidateShippingCost();
                    break;
                case "EmployeeID":
                    error = this.ValidateEmployeeID();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on Manifest: " + propertyName);
                    break;
            }
            return error;
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
        /// Validate the shipment type
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateShipmentType()
        {
            return IsEmpty(this.ShipmentType) ? Messages.InvalidShipmentType : null;
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
        /// Validate the departure time
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateDepartureTime()
        {
            if (this.DepartureTime.CompareTo(DateTime.Now) > 0)
                return Messages.InvalidDate;
            return null;
        }

        /// <summary>
        /// Validate the ETA
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateETA()
        {
            if (this.ETA.CompareTo(this.DepartureTime) < 0)
                return Messages.InvalidDate;
            return null;
        }

        /// <summary>
        /// Validate the shipping cost
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateShippingCost()
        {
            if (this.ManifestID < 0.0)
                return Messages.InvalidValue;
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
            return string.Format("{0} Manifest #{1}", this.ShipmentType, this.ManifestID);
        }

        public override bool Equals(object obj)
        {
            if(obj is Manifest)
            {
                Manifest other = obj as Manifest;
                return this.ManifestID.Equals(other.ManifestID) &&
                    this.ShipmentType.Equals(other.ShipmentType) &&
                    this.ShippingCost.Equals(other.ShippingCost) &&
                    this.DepartureTime.Equals(other.DepartureTime) &&
                    this.ETA.Equals(other.ETA) &&
                    this.EmployeeID.Equals(other.EmployeeID) &&
                    this.VehicleID.Equals(other.VehicleID);
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
                return this.ManifestID;
            }
            set
            {
                this.ManifestID = value;
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
            "ManifestID",
            "ShipmentType",
            "VehicleID",
            "DepartureTime",
            "ETA",
            "ShippingCost",
            "EmployeeID"
        };

        /// <summary>
        /// Unique identifier
        /// </summary>
        public long ManifestID { get; set; }

        /// <summary>
        /// Type of shipment, incoming or outgoing
        /// </summary>
        public string ShipmentType { get; set; }

        /// <summary>
        /// Identifier of the vehicle transporting the shipment
        /// </summary>
        public long VehicleID { get; set; }

        /// <summary>
        /// Date and time the shipment was sent out
        /// </summary>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// Estimated time of arrival
        /// </summary>
        public DateTime ETA { get; set; }

        /// <summary>
        /// Flag indicating the shipment arrived at its destination
        /// </summary>
        public bool Arrived { get; set; }

        /// <summary>
        /// Total cost of the shipment
        /// </summary>
        public double ShippingCost { get; set; }

        /// <summary>
        /// Identifier of the employee assigned to this manifest
        /// </summary>
        public long EmployeeID { get; set; }

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