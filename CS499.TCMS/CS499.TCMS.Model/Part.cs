using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CS499.TCMS.Model
{
    /// <summary>
    /// Holds all relevant data for an inventory part
    /// </summary>
    public class Part : IModel
    {
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="partID">unique identifier</param>
        /// <param name="description">description of the item</param>
        /// <param name="partNumber">number of the item, not related to the identifier</param>
        /// <param name="price">price per unit of the item</param>
        /// <param name="weight">weight of the item in pounds</param>
        /// <param name="quantity">quantity of the item in stock</param>
        public Part(long partID, string description, long partNumber, double price, double weight, int quantity)
        {
            this.PartID = partID;
            this.PartDescription = description;
            this.PartNumber = partNumber;
            this.PartPrice = price;
            this.PartWeight = weight;
            this.QuantityInStock = quantity;
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
                case "PartID":
                    error = this.ValidatePartID();
                    break;
                case "PartNumber":
                    error = this.ValidatePartNumber();
                    break;
                case "PartDescription":
                    error = this.ValidatePartDescription();
                    break;
                case "PartPrice":
                    error = this.ValidatePartPrice();
                    break;
                case "PartWeight":
                    error = this.ValidatePartWeight();
                    break;
                case "QuantityInStock":
                    error = this.ValidateQuantityInStock();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on Part: " + propertyName);
                    break;
            }
            return error;
        }

        /// <summary>
        /// Validate the part ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidatePartID()
        {
            if (this.PartID < 0)
                return Messages.InvalidID;
            return null;
        }

        /// <summary>
        /// Validate the part number
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidatePartNumber()
        {
            if (this.PartNumber < 0)
                return Messages.InvalidID;
            return null;
        }

        /// <summary>
        /// Validate the part description
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidatePartDescription()
        {
            return IsEmpty(this.PartDescription) ? Messages.InvalidDescription : null;
        }

        /// <summary>
        /// Validate the part price
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidatePartPrice()
        {
            if (this.PartPrice < 0.0)
                return Messages.InvalidValue;
            return null;
        }

        /// <summary>
        /// Validate the part weight
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidatePartWeight()
        {
            if (this.PartWeight < 0.0)
                return Messages.InvalidValue;
            return null;
        }

        /// <summary>
        /// Validate the quantity in stock
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateQuantityInStock()
        {
            if (this.QuantityInStock < 0)
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

        public override string ToString()
        {
            return string.Format("Part #{0}: {1}", this.PartNumber, this.PartDescription);
        }

        public override bool Equals(object obj)
        {
            if(obj is Part)
            {
                Part other = obj as Part;
                return this.PartID.Equals(other.PartID) &&
                    this.PartNumber.Equals(other.PartNumber) &&
                    this.PartPrice.Equals(other.PartPrice) &&
                    this.PartWeight.Equals(other.PartWeight) &&
                    this.PartDescription.Equals(other.PartDescription) &&
                    this.QuantityInStock.Equals(other.QuantityInStock);
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
                return this.PartID;
            }
            set
            {
                this.PartID = value;
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
            "PartID",
            "PartDescription",
            "PartNumber",
            "PartPrice",
            "PartWeight",
            "QuantityInStock"
        };
         
        /// <summary>
        /// Unique identifier
        /// </summary>
        public long PartID { get; set; }

        /// <summary>
        /// Description of the item
        /// </summary>
        public string PartDescription { get; set; }

        /// <summary>
        /// Number of the item, not related to the identifier
        /// </summary>
        public long PartNumber { get; set; }

        /// <summary>
        /// Price per unit of the item
        /// </summary>
        public double PartPrice { get; set; }

        /// <summary>
        /// Weight of the item in pounds
        /// </summary>
        public double PartWeight { get; set; }

        /// <summary>
        /// Quantity of the item in stock
        /// </summary>
        public int QuantityInStock { get; set; }

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