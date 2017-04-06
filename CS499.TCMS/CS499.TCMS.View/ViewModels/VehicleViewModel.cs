using CS499.TCMS.DataAccess.IRepositories;
using CS499.TCMS.Model;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Resources;
using CS499.TCMS.View.Services;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CS499.TCMS.View.ViewModels
{
    /// <summary>
    /// This class will handle the maintenance of the <see cref="Vehicle"/> model
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IChanges" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class VehicleViewModel : WorkspaceViewModel, IDataErrorInfo, IChanges, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleViewModel"/> class.
        /// </summary>
        /// <param name="model">model for the vehicle</param>
        /// <param name="vehicleRepository">repository for database operations</param>
        /// <param name="taskManager">task manager to hold reference to running tasks</param>
        /// <param name="isNew">flag indicating if this is a new vehicle</param>
        public VehicleViewModel(Vehicle model, IVehicleRepository vehicleRepository, ITaskManager taskManager, bool isNew)
        {
            this.Model = model;
            this.vehicleRepository = vehicleRepository;
            this.TaskManager = taskManager;
            this.IsNew = isNew;
            this.IsSelected = true;
            this.HasChanges = false;
            this.ContentId = model.VehicleID.GetContentId(this.DisplayName);
        }

        #endregion

        #region Methods

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
                    this.vehicleRepository.Insert(this.Model);
                }
                else
                {
                    this.vehicleRepository.Update(this.Model);
                }

            },
            TaskCreationOptions.LongRunning),
            Messages.VehicleSaving,
            () =>
            {

                // request to remove from parent workspace
                this.CloseCommand.Execute(this);

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "Saving vehicle",
            string.Format(Messages.VehicleSaveError, this.Model.ToString()),
            log,
            () =>
            {
                // send load notification to the all vehicle view model
                this.MessengerInstance.Send<NotificationMessage<AllVehicleViewModel>>(
                    new NotificationMessage<AllVehicleViewModel>(null, null));

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
        /// <param name="e">key event Args</param>
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
        /// vehicle model
        /// </summary>
        public Vehicle Model;

        /// <summary>
        /// vehicle repository
        /// </summary>
        private IVehicleRepository vehicleRepository;

        /// <summary>
        /// Gets the vehicle types.
        /// </summary>
        /// <value>
        /// The vehicle types.
        /// </value>
        public string[] VehicleTypes
        {

            get
            {
                return Enums.GetHumanizedValues<Enums.TruckMaxCapacity>();
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
        /// Gets or sets the brand.
        /// </summary>
        /// <value>
        /// The brand.
        /// </value>
        public string Brand
        {
            get
            {
                return Model.Brand;
            }
            set
            {

                if (Model.Brand == value)
                {
                    return;
                }

                Model.Brand = value;

                base.OnPropertyChanged("Brand");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int Year
        {
            get
            {
                return Model.Year;
            }
            set
            {

                if (Model.Year == value)
                {
                    return;
                }

                Model.Year = value;

                base.OnPropertyChanged("Year");
                this.HasChanges = true;
                
            }
        }

        /// <summary>
        /// Gets or sets the vehicle model.
        /// </summary>
        /// <value>
        /// The vehicle model.
        /// </value>
        public string VehicleModel
        {
            get
            {
                return Model.Model;
            }
            set
            {

                if (Model.Model == value)
                {
                    return;
                }

                Model.Model = value;

                base.OnPropertyChanged("VehicleModel");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the type of the vehicle.
        /// </summary>
        /// <value>
        /// The type of the vehicle.
        /// </value>
        public Enums.TruckMaxCapacity VehicleType
        {
            get
            {
                return Model.VehicleType;
            }
            set
            {

                if (Model.VehicleType == value)
                {
                    return;
                }

                Model.VehicleType = value;

                base.OnPropertyChanged("VehicleType");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the capacity.
        /// </summary>
        /// <value>
        /// The capacity.
        /// </value>
        public int Capacity
        {
            get
            {
                return Model.Capacity;
            }
            set
            {

                if (Model.Capacity == value)
                {
                    return;
                }

                Model.Capacity = value;

                base.OnPropertyChanged("Capacity");
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
                string msg = string.Format(Messages.VehicleDisplayName, this.IsNew ? "New" : displayName);
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
