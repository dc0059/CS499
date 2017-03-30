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
    /// This class will handle the maintenance of the <see cref="MaintenancePart"/> model
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IChanges" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class MaintenancePartViewModel : WorkspaceViewModel, IDataErrorInfo, IChanges, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MaintenancePartViewModel"/> class.
        /// </summary>
        /// <param name="model">model for the maintenance part</param>
        /// <param name="maintenancePartRepository">repository for database operations</param>
        /// <param name="taskManager">task manager to hold reference to running tasks</param>
        /// <param name="isNew">flag indicating if this is a new maintenance part</param>
        /// <param name="maintenanceRecordDetails">The purchase orders.</param>
        /// <param name="parts">The parts.</param>
        public MaintenancePartViewModel(MaintenancePart model, IMaintenancePartRepository maintenancePartRepository, ITaskManager taskManager, bool isNew,
            ObservableCollectionExtended<MaintenanceRecordDetail> maintenanceRecordDetails, ObservableCollectionExtended<Part> parts)
        {
            this.Model = model;
            this.maintenancePartRepository = maintenancePartRepository;
            this.TaskManager = taskManager;
            this.IsNew = isNew;
            this.IsSelected = true;
            this.HasChanges = false;
            this.ContentId = model.MaintenancePartID.GetContentId(this.DisplayName);
            this.MaintenanceRecordDetails = maintenanceRecordDetails;
            this.Parts = parts;
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
                this.SelectedMaintenanceRecordDetail = this.MaintenanceRecordDetails.UnfilteredList.FirstOrDefault();
                this.SelectedPart = this.Parts.UnfilteredList.FirstOrDefault();
            }
            else
            {
                this.SelectedMaintenanceRecordDetail = this.MaintenanceRecordDetails.UnfilteredList
                    .FirstOrDefault((i) => i.DetailID.Equals(this.DetailID));
                this.SelectedPart = this.Parts.UnfilteredList
                    .FirstOrDefault((p) => p.PartID.Equals(this.PartID));
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
                    this.maintenancePartRepository.Insert(this.Model);
                }
                else
                {
                    this.maintenancePartRepository.Update(this.Model);
                }

            },
            TaskCreationOptions.LongRunning),
            Messages.MaintenancePartSaving,
            () => { },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "Saving maintenance part",
            string.Format(Messages.MaintenancePartSaveError, this.Model.ToString()),
            log,
            () =>
            {
                // send load notification to the all maintenance part view model
                this.MessengerInstance.Send<NotificationMessage<AllMaintenancePartViewModel>>(
                    new NotificationMessage<AllMaintenancePartViewModel>(null, null));

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
        /// Maintenance Part model
        /// </summary>
        public MaintenancePart Model;

        /// <summary>
        /// Maintenance Part repository
        /// </summary>
        private IMaintenancePartRepository maintenancePartRepository;

        /// <summary>
        /// Gets or sets the maintenance record details.
        /// </summary>
        /// <value>
        /// The maintenance record details.
        /// </value>
        public ObservableCollectionExtended<MaintenanceRecordDetail> MaintenanceRecordDetails { get; set; }

        private MaintenanceRecordDetail _selectedMaintenanceRecordDetail;

        /// <summary>
        /// Gets or sets the selected maintenance record detail.
        /// </summary>
        /// <value>
        /// The selected maintenance record detail.
        /// </value>
        public MaintenanceRecordDetail SelectedMaintenanceRecordDetail
        {
            get
            {
                return _selectedMaintenanceRecordDetail;
            }
            set
            {

                if (_selectedMaintenanceRecordDetail == value)
                {
                    return;
                }

                _selectedMaintenanceRecordDetail = value;

                if (_selectedMaintenanceRecordDetail != null)
                {
                    this.DetailID = _selectedMaintenanceRecordDetail.DetailID;
                }

                base.OnPropertyChanged("SelectedMaintenanceRecordDetail");

            }
        }

        /// <summary>
        /// Gets or sets the parts.
        /// </summary>
        /// <value>
        /// The parts.
        /// </value>
        public ObservableCollectionExtended<Part> Parts { get; set; }

        private Part _selectedPart;

        /// <summary>
        /// Gets or sets the selected part.
        /// </summary>
        /// <value>
        /// The selected part.
        /// </value>
        public Part SelectedPart
        {
            get
            {
                return _selectedPart;
            }
            set
            {

                if (_selectedPart == value)
                {
                    return;
                }

                _selectedPart = value;

                if (_selectedPart != null)
                {
                    this.PartID = _selectedPart.PartID;
                }

                base.OnPropertyChanged("SelectedPart");

            }
        }

        /// <summary>
        /// Gets or sets the maintenance part identifier.
        /// </summary>
        /// <value>
        /// The maintenance part identifier.
        /// </value>
        public long MaintenancePartID
        {
            get
            {
                return Model.MaintenancePartID;
            }
            set
            {

                if (Model.MaintenancePartID == value)
                {
                    return;
                }

                Model.MaintenancePartID = value;

                base.OnPropertyChanged("MaintenancePartID");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity
        {
            get
            {
                return Model.Quantity;
            }
            set
            {

                if (Model.Quantity == value)
                {
                    return;
                }

                Model.Quantity = value;

                base.OnPropertyChanged("Quantity");
                this.HasChanges = true;

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
        /// Gets or sets the part identifier.
        /// </summary>
        /// <value>
        /// The part identifier.
        /// </value>
        public long PartID
        {
            get
            {
                return Model.PartID;
            }
            set
            {

                if (Model.PartID == value)
                {
                    return;
                }

                Model.PartID = value;

                base.OnPropertyChanged("PartID");
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
                string msg = string.Format(Messages.MaintenancePartDisplayName, this.IsNew ? "New" : displayName);
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
