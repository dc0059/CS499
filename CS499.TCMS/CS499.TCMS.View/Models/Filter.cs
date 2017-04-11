using CS499.TCMS.View.Interfaces;

namespace CS499.TCMS.View.Models
{
    /// <summary>
    /// This class will contain filter information for a ViewModel
    /// </summary>
    public class Filter
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property to filter</param>
        /// <param name="filterText">Text to filter</param>
        /// <param name="viewModel">ViewModel to apply the filter on</param>
        public Filter(string propertyName, string filterText, IFilterable viewModel)
        {
            this.PropertyName = propertyName;
            this.FilterText = filterText;
            this.ViewModel = viewModel;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Name of the property to filter
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Text to filter
        /// </summary>
        public string FilterText { get; set; }

        /// <summary>
        /// ViewModel to apply the filter on
        /// </summary>
        public IFilterable ViewModel { get; set; }

        #endregion

    }
}
