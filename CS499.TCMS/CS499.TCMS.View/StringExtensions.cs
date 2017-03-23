using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS499.TCMS.View
{
    /// <summary>
    /// This class will contain extension methods for strings
    /// </summary>
    public static class StringExtensions
    {

        #region Methods

        /// <summary>
        /// Checks to see if the target string is contained in the
        /// source string
        /// </summary>
        /// <param name="source">original string</param>
        /// <param name="target">string to check for</param>
        /// <param name="stringComparison">string comparison type</param>
        /// <returns>true if the target string is contained in the source string, false otherwise</returns>
        public static bool Contains(this string source, string target, StringComparison stringComparison)
        {

            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            if (string.IsNullOrEmpty(target))
            {
                return false;
            }

            return source.IndexOf(target, stringComparison) != -1;

        }

        #endregion

    }
}
