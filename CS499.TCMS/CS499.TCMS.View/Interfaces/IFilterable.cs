using CS499.TCMS.View.Models;

namespace CS499.TCMS.View.Interfaces
{
    /// <summary>
    /// This interface will be used to apply filters to ViewModels
    /// </summary>
    public interface IFilterable
    {

        #region Methods

        /// <summary>
        /// Apply filter
        /// </summary>
        /// <param name="filter">the filter</param>
        void ApplyFilter(Filter filter);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        Filter Filter { get; set; }

        #endregion

    }
}
