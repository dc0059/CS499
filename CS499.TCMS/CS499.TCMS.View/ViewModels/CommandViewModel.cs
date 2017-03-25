using CS499.TCMS.ViewModels;
using System;
using System.Windows.Input;

namespace CS499.TCMS.View.ViewModels
{
    /// <summary>
    /// Represents an actionable viewModel displayed by a View
    /// </summary>
    public class CommandViewModel : ViewModelBase
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandViewModel"/> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="displayToolTip">The display tool tip.</param>
        /// <param name="command">The command.</param>
        /// <param name="iconPath">The icon path.</param>
        /// <exception cref="System.ArgumentNullException">command</exception>
        public CommandViewModel(string displayName, string displayToolTip, ICommand command, string iconPath)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            base.DisplayName = displayName;
            base.DisplayToolTip = displayToolTip;
            this.Command = command;
            this.Icon = iconPath;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public ICommand Command { get; private set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string Icon { get; set; }

        #endregion

    }
}