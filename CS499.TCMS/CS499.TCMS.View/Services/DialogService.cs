using CS499.TCMS.View.Interfaces;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;

namespace CS499.TCMS.View.Services
{
    /// <summary>
    /// This class will handle showing a dialog from the ViewModels
    /// </summary>
    public class DialogService : IDialogService
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dialog">DialogCoordiator</param>
        /// <param name="viewModel">ViewModel</param>
        public DialogService(IDialogCoordinator dialog, object viewModel)
        {
            this.Dialog = dialog;
            this.ViewModel = viewModel;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Open file dialog without startup path
        /// </summary>
        /// <param name="fileFilter">file filter</param>
        /// <returns>string to the file path</returns>
        public string OpenFileDialog(string fileFilter)
        {
            return OpenFileDialog(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileFilter);
        }

        /// <summary>
        /// Open file dialog with startup path
        /// </summary>
        /// <param name="startUpPath">string for the startup path</param>
        /// <param name="fileFilter">file filter</param>
        /// <returns>string to the file path</returns>
        public string OpenFileDialog(string startUpPath, string fileFilter)
        {
            string selectedFile = string.Empty;

            // create dialog
            var dialog = new OpenFileDialog()
            {
                Multiselect = false,
                InitialDirectory = startUpPath == null ? null : startUpPath,
                Title = "Select file",
                Filter = fileFilter,
                FilterIndex = 0,
                RestoreDirectory = true
            };

            // get result
            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                selectedFile = dialog.FileName;
            }

            return selectedFile;
        }

        /// <summary>
        /// Save file dialog without startup path
        /// </summary>        
        /// <param name="fileFilter">file filter</param>
        /// <returns>string to the file path</returns>
        public string SaveFileDialog(string fileFilter)
        {
            return this.SaveFileDialog(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileFilter);
        }

        /// <summary>
        /// Save file dialog with startup path
        /// </summary>
        /// <param name="startUpPath">string for the startup path</param>
        /// <param name="fileFilter">file filter</param>
        /// <returns>string to the file path</returns>
        public string SaveFileDialog(string startUpPath, string fileFilter)
        {

            string selectedFile = string.Empty;

            // create dialog
            var dialog = new SaveFileDialog()
            {
                InitialDirectory = startUpPath == null ? null : startUpPath,
                Title = "Save file",
                Filter = fileFilter,
                FilterIndex = 0,
                RestoreDirectory = true
            };

            // get result
            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                selectedFile = dialog.FileName;
            }

            return selectedFile;

        }

        #endregion

        #region Properties

        /// <summary>
        /// Dialog coordinator that will show messages
        /// </summary>
        public IDialogCoordinator Dialog { get; set; }

        /// <summary>
        /// ViewModel of the MainWindow
        /// </summary>
        public object ViewModel { get; set; }

        #endregion

    }

}
