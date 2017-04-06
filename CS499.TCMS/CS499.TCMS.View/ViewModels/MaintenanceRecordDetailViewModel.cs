using CS499.TCMS.DataAccess.IRepositories;
using CS499.TCMS.Model;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Resources;
using CS499.TCMS.View.Services;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CS499.TCMS.View.ViewModels
{
    /// <summary>
    /// This class will handle the maintenance of the <see cref="MaintenanceRecordDetail"/> model
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IChanges" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class MaintenanceRecordDetailViewModel : WorkspaceViewModel, IDataErrorInfo, IChanges, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MaintenanceRecordDetailViewModel"/> class.
        /// </summary>
        /// <param name="model">model for the maintenance record detail</param>
        /// <param name="maintenanceRecordDetailRepository">repository for database operations</param>
        /// <param name="taskManager">task manager to hold reference to running tasks</param>
        /// <param name="isNew">flag indicating if this is a new maintenance record detail</param>
        /// <param name="maintenanceRecords">collection of all maintenance records</param>
        /// <param name="users">collection of all users</param>
        public MaintenanceRecordDetailViewModel(MaintenanceRecordDetail model, IMaintenanceRecordDetailRepository maintenanceRecordDetailRepository, ITaskManager taskManager, bool isNew,
            ObservableCollectionExtended<MaintenanceRecord> maintenanceRecords, ObservableCollectionExtended<User> users)
        {
            this.Model = model;
            this.maintenanceRecordDetailRepository = maintenanceRecordDetailRepository;
            this.TaskManager = taskManager;
            this.IsNew = isNew;
            this.IsSelected = true;
            this.HasChanges = false;
            this.ContentId = model.DetailID.GetContentId(this.DisplayName);
            this.MaintenanceRecords = maintenanceRecords;
            this.Users = users;
            this.SetSelected(isNew);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the selected.
        /// </summary>
        /// <param name="isNew">if set to <c>true</c> is new.</param>
        private void SetSelected(bool isNew)
        {

            if (isNew)
            {
                this.SelectedMaintenanceRecord = this.MaintenanceRecords.UnfilteredList.FirstOrDefault();
                this.SelectedUser = this.Users.UnfilteredList.FirstOrDefault();
            }
            else
            {
                this.SelectedMaintenanceRecord = this.MaintenanceRecords.UnfilteredList
                    .FirstOrDefault((r) => r.MaintenanceID.Equals(this.MaintenanceID));
                this.SelectedUser = this.Users.UnfilteredList
                    .FirstOrDefault((u) => u.EmployeeID.Equals(this.EmployeeID));
            }

        }

        /// <summary>
        /// Save ViewModel
        /// </summary>
        private void Save()
        {

            // start task to save viewModel information
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                // insert new viewModel or update current viewModel
                if (this.IsNew)
                {
                    this.maintenanceRecordDetailRepository.Insert(this.Model);
                }
                else
                {
                    this.maintenanceRecordDetailRepository.Update(this.Model);
                }

            },
            TaskCreationOptions.LongRunning),
            Messages.MaintenanceRecordDetailSaving,
            () =>
            {

                // request to remove from parent workspace
                this.CloseCommand.Execute(this);

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "Saving maintenance record detail",
            string.Format(Messages.MaintenanceRecordDetailSaveError, this.Model.ToString()),
            log,
            () =>
            {
                // send load notification to the all maintenanceRecordDetail view model
                this.MessengerInstance.Send<NotificationMessage<AllMaintenanceRecordDetailViewModel>>(
                    new NotificationMessage<AllMaintenanceRecordDetailViewModel>(null, null));

            });

        }

        /// <summary>
        /// Check for changes to the model
        /// </summary>
        /// <returns>true if there are changes</returns>
        bool IChanges.CheckForChanges()
        {
            return this.HasChanges;
        }

        /// <summary>
        /// Execute commands based on the key combination pressed
        /// </summary>
        /// <param name="e">key event args</param>
        void IKeyCommand.SendKeys(KeyEventArgs e)
        {

            if (!e.KeyboardDevice.IsKeyDown(Key.LeftCtrl))
                return;

            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) && e.Key == Key.S)
            {
                if (_commandSave.CanExecute(null))
                {
                    this.Save();
                }
            }

        }

        #endregion

        #region Properties

        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Maintenance Record Detail model
        /// </summary>
        public MaintenanceRecordDetail Model;

        /// <summary>
        /// Maintenance Record Detail repository
        /// </summary>
        private IMaintenanceRecordDetailRepository maintenanceRecordDetailRepository;

        /// <summary>
        /// Gets or sets the maintenance records.
        /// </summary>
        /// <value>
        /// The maintenance records.
        /// </value>
        public ObservableCollectionExtended<MaintenanceRecord> MaintenanceRecords { get; set; }

        private MaintenanceRecord _selectedMaintenanceRecord;

        /// <summary>
        /// Gets or sets the selected maintenanceRecord.
        /// </summary>
        /// <value>
        /// The selected maintenanceRecord.
        /// </value>
        public MaintenanceRecord SelectedMaintenanceRecord
        {
            get
            {
                return _selectedMaintenanceRecord;
            }
            set
            {

                if (_selectedMaintenanceRecord == value)
                {
                    return;
                }

                _selectedMaintenanceRecord = value;

                if (_selectedMaintenanceRecord != null)
                {
                    this.MaintenanceID = _selectedMaintenanceRecord.MaintenanceID;
                }

                base.OnPropertyChanged("SelectedMaintenanceRecord");

            }
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public ObservableCollectionExtended<User> Users { get; set; }

        private User _selectedUser;

        /// <summary>
        /// Gets or sets the selected user.
        /// </summary>
        /// <value>
        /// The selected user.
        /// </value>
        public User SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {

                if (_selectedUser == value)
                {
                    return;
                }

                _selectedUser = value;

                if (_selectedUser != null)
                {
                    this.EmployeeID = _selectedUser.EmployeeID;
                }

                base.OnPropertyChanged("SelectedUser");

            }
        }

        /// <summary>
        /// Gets or sets the detail identifier.
        /// </summary>
        /// <value>
        /// The detail identifier.
        /// </value>
        public long DetailID
        {
            get
            {
                return Model.DetailID;
            }
            set
            {

                if (Model.DetailID == value)
                {
                    return;
                }

                Model.DetailID = value;

                base.OnPropertyChanged("DetailID");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the maintenance identifier.
        /// </summary>
        /// <value>
        /// The maintenance identifier.
        /// </value>
        public long MaintenanceID
        {
            get
            {
                return Model.MaintenanceID;
            }
            set
            {

                if (Model.MaintenanceID == value)
                {
                    return;
                }

                Model.MaintenanceID = value;

                base.OnPropertyChanged("MaintenanceID");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public long EmployeeID
        {
            get
            {
                return Model.EmployeeID;
            }
            set
            {

                if (Model.EmployeeID == value)
                {
                    return;
                }

                Model.EmployeeID = value;

                base.OnPropertyChanged("EmployeeID");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the repair description.
        /// </summary>
        /// <value>
        /// The repair description.
        /// </value>
        public string RepairDescription
        {
            get
            {
                return Model.RepairDescription;
            }
            set
            {

                if (Model.RepairDescription == value)
                {
                    return;
                }

                Model.RepairDescription = value;

                base.OnPropertyChanged("RepairDescription");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the repair date.
        /// </summary>
        /// <value>
        /// The repair date.
        /// </value>
        public DateTime RepairDate
        {
            get
            {
                return Model.RepairDate;
            }
            set
            {

                if (Model.RepairDate == value)
                {
                    return;
                }

                Model.RepairDate = value;

                base.OnPropertyChanged("RepairDate");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Flag indicating this ViewModel is new
        /// </summary>
        public override bool IsNew
        {
            get
            {
                return base.IsNew;
            }
            set
            {
                base.IsNew = value;
            }
        }

        /// <summary>
        /// Display name
        /// </summary>
        public override string DisplayName
        {
            get
            {
                string displayName = Model == null ? string.Empty : Model.ToString();
                string msg = string.Format(Messages.MaintenanceRecordDetailDisplayName, this.IsNew ? "New" : displayName);
                return msg;
            }
            protected set
            {
                base.DisplayName = value;
            }
        }

        /// <summary>
        /// Display tool tip
        /// </summary>
        public override string DisplayToolTip
        {
            get
            {
                return this.DisplayName;
            }
            set
            {
                base.DisplayToolTip = value;
            }
        }

        /// <summary>
        /// Flag indicating this ViewModel is selected in the UI
        /// </summary>
        public override bool IsSelected
        {
            get
            {
                return base.IsSelected;
            }
            set
            {
                base.IsSelected = value;
            }
        }

        string IDataErrorInfo.Error
        {
            get { return (Model as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {

            get
            {
                return (Model as IDataErrorInfo)[propertyName];
            }

        }

        private ICommand _commandSave;

        /// <summary>
        /// Command to execute the save ViewModel
        /// </summary>
        public ICommand CommandSave
        {
            get
            {

                if (_commandSave == null)
                {
                    _commandSave = new RelayCommand(param =>
                    {
                        this.Save();
                    },
                        param => this.Model != null ? this.Model.IsValid : false);
                }

                return _commandSave;
            }
        }

        #endregion

    }
}
