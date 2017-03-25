using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS499.TCMS.View.Models
{
    /// <summary>
    /// This class will hold the relevant data for a user theme
    /// </summary>
    [Serializable()]
    public class UserTheme
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserTheme"/> class.
        /// </summary>
        /// <param name="themeID">The theme identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="baseColor">Color of the base.</param>
        /// <param name="accentColor">Color of the accent.</param>
        public UserTheme(long themeID, string userName, string baseColor, string accentColor)
        {
            this.ThemeID = themeID;
            this.UserName = userName;
            this.BaseColor = baseColor;
            this.AccentColor = accentColor;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the theme identifier.
        /// </summary>
        /// <value>
        /// The theme identifier.
        /// </value>
        public long ThemeID { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the color of the base.
        /// </summary>
        /// <value>
        /// The color of the base.
        /// </value>
        public string BaseColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the accent.
        /// </summary>
        /// <value>
        /// The color of the accent.
        /// </value>
        public string AccentColor { get; set; }

        #endregion

    }
}
