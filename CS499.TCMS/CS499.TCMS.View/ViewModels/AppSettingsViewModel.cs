using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Resources;
using CS499.TCMS.View.Services;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace CS499.TCMS.View.ViewModels
{
    /// <summary>
    /// This class will handle the app settings
    /// </summary>
    public class AppSettingsViewModel : WorkspaceViewModel
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsViewModel"/> class.
        /// </summary>
        /// <param name="taskManager">The task manager.</param>
        public AppSettingsViewModel(ITaskManager taskManager)
        {
            this.TaskManager = taskManager;
            this.DisplayName = Messages.AppSettingsDisplayName;
            this.DisplayToolTip = Messages.AppSettingsDisplayToolTip;
            base.IsVisible = true;
            base.IsSelected = true;
            base.IsNew = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opens the log.
        /// </summary>
        private void OpenLog()
        {

            try
            {

                Process.Start(CoreAssembly.GetLogFileLocation());

            }
            catch (Exception ex)
            {

                log.Error(Messages.AppSettingsFailedToOpenLog, ex);

            }

        }

        /// <summary>
        /// Opens the about.
        /// </summary>
        private void OpenAbout()
        {
            AssemblyInformationViewModel viewModel = new AssemblyInformationViewModel(this.TaskManager);

            this.MessengerInstance.Send<NotificationMessage<WorkspaceViewModel>>(
               new NotificationMessage<WorkspaceViewModel>(viewModel, null));

        }

        #endregion

        #region Properties

        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Returns a visibility value for the object
        /// </summary>
        public override bool IsVisible
        {
            get
            {
                return base.IsVisible;
            }
            set
            {

                if (base.IsVisible == value)
                    return;

                base.IsVisible = value;
                base.OnPropertyChanged("IsVisible");

            }
        }

        private ICommand _commandOpenLog;

        /// <summary>
        /// Gets the command open log.
        /// </summary>
        /// <value>
        /// The command open log.
        /// </value>
        public ICommand CommandOpenLog
        {
            get
            {

                if (_commandOpenLog == null)
                {
                    _commandOpenLog = new RelayCommand(param =>
                    {
                        this.OpenLog();
                    });
                }

                return _commandOpenLog;
            }
        }

        private ICommand _commandAbout;

        /// <summary>
        /// Gets the command about.
        /// </summary>
        /// <value>
        /// The command about.
        /// </value>
        public ICommand CommandAbout
        {
            get
            {

                if (_commandAbout == null)
                {
                    _commandAbout = new RelayCommand(param =>
                    {
                        this.OpenAbout();
                    });
                }

                return _commandAbout;
            }
        }

        #endregion

    }

}
