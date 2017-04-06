using System;

namespace CS499.TCMS.View.Services
{
    /// <summary>
    /// This class will allow a description of a property to be added
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class DescriptionNameAttribute : Attribute
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="description">description of the property</param>
        public DescriptionNameAttribute(string description)
        {
            this.Description = description;
        }

        /// <summary>
        /// Add sort order to the property description
        /// </summary>
        /// <param name="description">description of the property</param>
        /// <param name="sortOrder">sort order of the property</param>
        public DescriptionNameAttribute(string description, int sortOrder)
            : this(description)
        {
            this.SortOrder = sortOrder;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Description of property
        /// </summary>
        public string Description
        {
            get;
            private set;
        }

        /// <summary>
        /// Sort order of property
        /// </summary>
        public int SortOrder
        {
            get;
            private set;
        }

        #endregion

    }
}
