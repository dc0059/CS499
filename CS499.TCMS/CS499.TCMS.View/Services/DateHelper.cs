using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS499.TCMS.View.Services
{
    /// <summary>
    /// This class contains helper methods for dates
    /// </summary>
    public static class DateHelper
    {

        #region Methods

        /// <summary>
        /// Calculates the years of service
        /// </summary>
        /// <remarks>Special thanks to LukeH on StackOverflow <seealso cref="http://stackoverflow.com/a/3055445"/></remarks>
        /// <param name="doe">start date</param>
        /// <returns>string containing the years, months, and days of service</returns>
        public static string ToYearsOfService(this DateTime doe)
        {
            DateTime today = DateTime.Today;

            int months = today.Month - doe.Month;
            int years = today.Year - doe.Year;

            if (today.Day < doe.Day)
            {
                months--;
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            int days = (today - doe.AddMonths((years * 12) + months)).Days;

            return string.Format("{0} year{1}, {2} month{3} and {4} day{5}",
                                 years, (years == 1) ? "" : "s",
                                 months, (months == 1) ? "" : "s",
                                 days, (days == 1) ? "" : "s");

        }

        #endregion
    }
}
