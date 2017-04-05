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
    /// This class will handle the maintenance of the <see cref="Manifest"/> model
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IChanges" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class ManifestViewModel : WorkspaceViewModel, IDataErrorInfo, IChanges, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestViewModel"/> class.
        /// </summary>
        /// <param name="model">model for the manifest</param>
        /// <param name="manifestRepository">repository for database operations</param>
        /// <param name="taskManager">task manager to hold reference to running tasks</param>
        /// <param name="isNew">flag indicating if this is a new manifest</param>
        /// <param name="vehicles">collection of all vehicles</param>
        /// <param name="users">collection of all users</param>
        public ManifestViewModel(Manifest model, IManifestRepository manifestRepository, ITaskManager taskManager, bool isNew, 
            ObservableCollectionExtended<Vehicle> vehicles, ObservableCollectionExtended<User> users)
        {
            this.Model = model;
            this.manifestRepository = manifestRepository;
            this.TaskManager = taskManager;
            this.IsNew = isNew;
            this.IsSelected = true;
            this.HasChanges = false;
            this.ContentId = model.EmployeeID.GetContentId(this.DisplayName);
            this.Vehicles = vehicles;
            this.Users = users;
            this.SetSelected(isNew);
            _shipmentTypes = new string[] { "Outgoing", "Incoming" };
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
                this.SelectedUser = this.Users.UnfilteredList.FirstOrDefault();
            }
            else
            {
                this.SelectedVehicle = this.Vehicles.UnfilteredList
                    .FirstOrDefault((v) => v.VehicleID.Equals(this.VehicleID));
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
                    this.manifestRepository.Insert(this.Model);
                }
                else
                {
                    this.manifestRepository.Update(this.Model);
                }

            },
            TaskCreationOptions.LongRunning),
            Messages.ManifestSaving,
            () => { },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "Saving manifest",
            string.Format(Messages.ManifestSaveError, this.Model.ToString()),
            log,
            () =>
            {
                // send load notification to the all manifest view model
                this.MessengerInstance.Send<NotificationMessage<AllManifestViewModel>>(
                    new NotificationMessage<AllManifestViewModel>(null, null));

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
        /// Manifest model
        /// </summary>
        public Manifest Model;

        /// <summary>
        /// Manifest repository
        /// </summary>
        private IManifestRepository manifestRepository;

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


        private string[] _shipmentTypes;

        /// <summary>
        /// Gets the shipment types.
        /// </summary>
        /// <value>
        /// The shipment types.
        /// </value>
        public string[] ShipmentTypes
        {
            get
            { 
                return _shipmentTypes;
            }
        }


        public long ManifestID
        {
            get
            {
                return Model.ManifestID;
            }
            set
            {

                if (Model.ManifestID == value)
                {
                    return;
                }

                Model.ManifestID = value;

                base.OnPropertyChanged("ManifestID");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the type of the shipment.
        /// </summary>
        /// <value>
        /// The type of the shipment.
        /// </value>
        public string ShipmentType
        {
            get
            {
                return Model.ShipmentType;
            }
            set
            {

                if (Model.ShipmentType == value)
                {
                    return;
                }

                Model.ShipmentType = value;

                base.OnPropertyChanged("ShipmentType");
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
        /// Gets or sets the departure time.
        /// </summary>
        /// <value>
        /// The departure time.
        /// </value>
        public DateTime DepartureTime
        {
            get
            {
                return Model.DepartureTime;
            }
            set
            {

                if (Model.DepartureTime == value)
                {
                    return;
                }

                Model.DepartureTime = value;

                base.OnPropertyChanged("DepartureTime");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the eta.
        /// </summary>
        /// <value>
        /// The eta.
        /// </value>
        public DateTime ETA
        {
            get
            {
                return Model.ETA;
            }
            set
            {

                if (Model.ETA == value)
                {
                    return;
                }

                Model.ETA = value;

                base.OnPropertyChanged("ETA");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this truck has arrived.
        /// </summary>
        /// <value>
        ///   <c>true</c> if arrived; otherwise, <c>false</c>.
        /// </value>
        public bool Arrived
        {
            get
            {
                return Model.Arrived;
            }
            set
            {

                if (Model.Arrived == value)
                {
                    return;
                }

                Model.Arrived = value;

                base.OnPropertyChanged("Arrived");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the shipping cost.
        /// </summary>
        /// <value>
        /// The shipping cost.
        /// </value>
        public double ShippingCost
        {
            get
            {
                return Model.ShippingCost;
            }
            set
            {

                if (Model.ShippingCost == value)
                {
                    return;
                }

                Model.ShippingCost = value;

                base.OnPropertyChanged("ShippingCost");
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
                string msg = string.Format(Messages.ManifestDisplayName, this.IsNew ? "New" : displayName);
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
