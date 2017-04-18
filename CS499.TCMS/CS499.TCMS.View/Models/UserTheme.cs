using System;

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
        public UserTheme()
        {
            this.AccentColor = "Emerald";
            this.BaseColor = "BaseDark";
            this.AvalonTheme = AvalonThemes.Visual_Studio_2013_Dark;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserTheme"/> class.
        /// </summary>
        /// <param name="themeID">The theme identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="baseColor">Color of the base.</param>
        /// <param name="accentColor">Color of the accent.</param>
        /// <param name="avalonTheme">Avalon theme</param>
        public UserTheme(long themeID, string userName, string baseColor, string accentColor, AvalonThemes avalonTheme)
            : this()
        {
            this.ThemeID = themeID;
            this.UserName = userName;
            this.BaseColor = baseColor;
            this.AccentColor = accentColor;
            this.AvalonTheme = avalonTheme;
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

        /// <summary>
        /// Gets or sets the avalon theme.
        /// </summary>
        /// <value>
        /// The avalon theme.
        /// </value>
        public AvalonThemes AvalonTheme { get; set; }

        #endregion

    }

    /// <summary>
    /// AvalonDock themes
    /// </summary>
    public enum AvalonThemes
    {
        Generic = 0,
        Aero,
        Metro,
        Visual_Studio_2010,
        Visual_Studio_2013_Blue,
        Visual_Studio_2013_Dark,
        Visual_Studio_2013_Light
    }

}
