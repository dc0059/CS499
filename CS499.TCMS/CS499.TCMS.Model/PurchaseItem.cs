using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CS499.TCMS.Model
{
    public class PurchaseItem : IModel
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="itemID">unique identifier</param>
        /// <param name="orderID">identifier of the purchase order this item is part of</param>
        /// <param name="quantity">quantity of the item</param>
        /// <param name="partID">identifier of the part this item is associated with</param>
        public PurchaseItem (long itemID, long orderID, int quantity, long partID)
        {
            this.ItemID = itemID;
            this.OrderID = orderID;
            this.Quantity = quantity;
            this.PartID = partID;
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
                case "ItemID":
                    error = this.ValidateItemID();
                    break;
                case "OrderID":
                    error = this.ValidateOrderID();
                    break;
                case "Quantity":
                    error = this.ValidateQuantity();
                    break;
                case "PartID":
                    error = this.ValidatePartID();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on PurchaseItem: " + propertyName);
                    break;
            }
            return error;
        }



        /// <summary>
        /// Validate the purchase item ID
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateItemID()
        {
            if (this.ItemID < 0)
                return Messages.InvalidID;
            return null;
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
        /// Validate the quantity
        /// </summary>
        /// <returns>string for the error</returns>
        private string ValidateQuantity()
        {
            if (this.Quantity <= 0)
                return Messages.InvalidValue;
            else
                return null;
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
                return this.ItemID;
            }
            set
            {
                this.ItemID = value;
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
            "ItemID",
            "OrderID",
            "Quantity",
            "PartID"
        };

        /// <summary>
        /// Unique identifier
        /// </summary>
        public long ItemID { get; set; }
        /// <summary>
        /// Identifier of the purchase order this item is part of
        /// </summary>
        public long OrderID { get; set; }
        /// <summary>
        /// Quantity of the item
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Identifier of the part this item is associated with
        /// </summary>
        public long PartID { get; set; }

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
