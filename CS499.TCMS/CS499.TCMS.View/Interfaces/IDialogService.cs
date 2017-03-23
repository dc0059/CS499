using MahApps.Metro.Controls.Dialogs;

namespace CS499.TCMS.View.Interfaces
{
    /// <summary>
    /// This interface will define a dialog service interface for
    /// showing message dialogs from a view model
    /// </summary>
    public interface IDialogService
    {
        
        #region Methods

        /// <summary>
        /// Open file dialog without startup path
        /// </summary>
        /// <param name="fileFilter">file filter</param>
        /// <returns>string to the file path</returns>
        string OpenFileDialog(string fileFilter);

        /// <summary>
        /// Open file dialog with startup path
        /// </summary>
        /// <param name="startUpPath">string for the startup path</param>
        /// <param name="fileFilter">file filter</param>
        /// <returns>string to the file path</returns>
        string OpenFileDialog(string startUpPath, string fileFilter);

        /// <summary>
        /// Save file dialog without startup path
        /// </summary>        
        /// <param name="fileFilter">file filter</param>
        /// <returns>string to the file path</returns>
        string SaveFileDialog(string fileFilter);

        /// <summary>
        /// Save file dialog with startup path
        /// </summary>
        /// <param name="startUpPath">string for the startup path</param>
        /// <param name="fileFilter">file filter</param>
        /// <returns>string to the file path</returns>
        string SaveFileDialog(string startUpPath, string fileFilter);

        #endregion

        #region Properties

        /// <summary>
        /// Dialog coordinator that will show messages
        /// </summary>
        IDialogCoordinator Dialog { get; set; }

        /// <summary>
        /// ViewModel of the MainWindow
        /// </summary>
        object ViewModel { get; set; }

        #endregion

    }
}