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
    /// This class will handle the maintenance of the <see cref="MaintenanceRecord"/> model
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IChanges" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class MaintenanceRecordViewModel : WorkspaceViewModel, IDataErrorInfo, IChanges, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MaintenanceRecordViewModel"/> class.
        /// </summary>
        /// <param name="model">model for the maintenance record</param>
        /// <param name="maintenanceRecordRepository">repository for database operations</param>
        /// <param name="taskManager">task manager to hold reference to running tasks</param>
        /// <param name="isNew">flag indicating if this is a new maintenance record</param>
        /// <param name="vehicles">collection of all vehicles</param>
        public MaintenanceRecordViewModel(MaintenanceRecord model, IMaintenanceRecordRepository maintenanceRecordRepository, ITaskManager taskManager, bool isNew,
            ObservableCollectionExtended<Vehicle> vehicles)
        {
            this.Model = model;
            this.maintenanceRecordRepository = maintenanceRecordRepository;
            this.TaskManager = taskManager;
            this.IsNew = isNew;
            this.IsSelected = true;
            this.HasChanges = false;
            this.ContentId = model.MaintenanceID.GetContentId(this.DisplayName);
            this.Vehicles = vehicles;
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
                this.SelectedVehicle = this.Vehicles.UnfilteredList.FirstOrDefault();
            }
            else
            {
                this.SelectedVehicle = this.Vehicles.UnfilteredList
                    .FirstOrDefault((v) => v.VehicleID.Equals(this.VehicleID));
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
                    this.maintenanceRecordRepository.Insert(this.Model);
                }
                else
                {
                    this.maintenanceRecordRepository.Update(this.Model);
                }

            },
            TaskCreationOptions.LongRunning),
            Messages.MaintenanceRecordSaving,
            () =>
            {

                // request to remove from parent workspace
                this.CloseCommand.Execute(this);

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "Saving maintenance record",
            string.Format(Messages.MaintenanceRecordSaveError, this.Model.ToString()),
            log,
            () =>
            {
                // send load notification to the all maintenance record view model
                this.MessengerInstance.Send<NotificationMessage<AllMaintenanceRecordViewModel>>(
                    new NotificationMessage<AllMaintenanceRecordViewModel>(null, null));

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
        /// Maintenance Record model
        /// </summary>
        public MaintenanceRecord Model;

        /// <summary>
        /// Maintenance Record repository
        /// </summary>
        private IMaintenanceRecordRepository maintenanceRecordRepository;

        /// <summary>
        /// Gets or sets the vehicles.
        /// </summary>
        /// <value>
        /// The vehicles.
        /// </value>
        public ObservableCollectionExtended<Vehicle> Vehicles { get; set; }

        private Vehicle _selectedVehicle;

        /// <summary>
        /// Gets or sets the selected vehicle.
        /// </summary>
        /// <value>
        /// The selected vehicle.
        /// </value>
        public Vehicle SelectedVehicle
        {
            get
            {
                return _selectedVehicle;
            }
            set
            {

                if (_selectedVehicle == value)
                {
                    return;
                }

                _selectedVehicle = value;

                if (_selectedVehicle != null)
                {
                    this.VehicleID = _selectedVehicle.VehicleID;
                }

                base.OnPropertyChanged("SelectedVehicle");

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
        /// Gets or sets the vehicle identifier.
        /// </summary>
        /// <value>
        /// The vehicle identifier.
        /// </value>
        public long VehicleID
        {
            get
            {
                return Model.VehicleID;
            }
            set
            {

                if (Model.VehicleID == value)
                {
                    return;
                }

                Model.VehicleID = value;

                base.OnPropertyChanged("VehicleID");
                this.HasChanges = true;
            }
        }

        /// <summary>
        /// Gets or sets the maintenance date.
        /// </summary>
        /// <value>
        /// The maintenance date.
        /// </value>
        public DateTime MaintenanceDate
        {
            get
            {
                return Model.MaintenanceDate;
            }
            set
            {

                if (Model.MaintenanceDate == value)
                {
                    return;
                }

                Model.MaintenanceDate = value;

                base.OnPropertyChanged("MaintenanceDate");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the maintenance description.
        /// </summary>
        /// <value>
        /// The maintenance description.
        /// </value>
        public string MaintenanceDescription
        {
            get
            {
                return Model.MaintenanceDescription;
            }
            set
            {

                if (Model.MaintenanceDescription == value)
                {
                    return;
                }

                Model.MaintenanceDescription = value;

                base.OnPropertyChanged("MaintenanceDescription");
                this.HasChanges = true;

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
                string msg = string.Format(Messages.MaintenanceRecordDisplayName, this.IsNew ? "New" : displayName);
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
                        param => this.Model != null ? this.Model.IsValid && this.HasChanges : false);
                }

                return _commandSave;
            }
        }
        #endregion

    }
}
