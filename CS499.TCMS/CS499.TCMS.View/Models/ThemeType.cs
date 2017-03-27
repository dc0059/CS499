using System.Windows.Media;

namespace CS499.TCMS.View.Models
{
    /// <summary>
    /// This class will hold accent name/brush information
    /// </summary>
    public class ThemeType
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeType"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="borderColorBrush">The border color brush.</param>
        /// <param name="colorBrush">The color brush.</param>
        public ThemeType(string name, Brush borderColorBrush, Brush colorBrush)
        {
            this.Name = name;
            this.BorderColorBrush = borderColorBrush;
            this.ColorBrush = colorBrush;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the border color brush.
        /// </summary>
        /// <value>
        /// The border color brush.
        /// </value>
        public Brush BorderColorBrush { get; set; }

        /// <summary>
        /// Gets or sets the color brush.
        /// </summary>
        /// <value>
        /// The color brush.
        /// </value>
        public Brush ColorBrush { get; set; }

        #endregion

    }
}
