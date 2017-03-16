using CS499.TCMS.View.Resources;
using System;

namespace CS499.TCMS.View.Services
{
    public static class ContentIdHelper
    {

        /// <summary>
        /// Converts the Int64 number to string format for the ContentId of
        /// the AvalonDock container
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="constant">string constant for the parent ViewModel</param>
        /// <returns>formatted string ContentId</returns>
        public static string GetContentId(this Int64 value, string constant)
        {
            return string.Format(Messages.ContentId, constant, value.ToString());
        }

    }
}
