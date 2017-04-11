using CS499.TCMS.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
