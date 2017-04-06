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
    /// This class will handle the maintenance of the <see cref="PurchaseOrder"/> model
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IChanges" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class PurchaseOrderViewModel : WorkspaceViewModel, IDataErrorInfo, IChanges, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseOrderViewModel"/> class.
        /// </summary>
        /// <param name="model">model for the purchase order</param>
        /// <param name="purchaseOrderRepository">repository for database operations</param>
        /// <param name="taskManager">task manager to hold reference to running tasks</param>
        /// <param name="isNew">flag indicating if this is a new purchase order</param>
        /// <param name="businessPartners">The business partners.</param>
        /// <param name="manifests">The manifests.</param>
        public PurchaseOrderViewModel(PurchaseOrder model, IPurchaseOrderRepository purchaseOrderRepository, ITaskManager taskManager, bool isNew,
            ObservableCollectionExtended<BusinessPartner> businessPartners, ObservableCollectionExtended<Manifest> manifests)
        {
            this.Model = model;
            this.purchaseOrderRepository = purchaseOrderRepository;
            this.TaskManager = taskManager;
            this.IsNew = isNew;
            this.IsSelected = true;
            this.HasChanges = false;
            this.ContentId = model.OrderID.GetContentId(this.DisplayName);
            this.BusinessPartners = businessPartners;
            this.Manifests = manifests;
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
                this.SelectedSource = this.BusinessPartners.UnfilteredList.FirstOrDefault();
                this.SelectedDestination = this.BusinessPartners.UnfilteredList.FirstOrDefault();
                this.SelectedManifest = this.Manifests.UnfilteredList.FirstOrDefault();
            }
            else
            {
                this.SelectedSource = this.BusinessPartners.UnfilteredList
                    .FirstOrDefault((s) => s.CompanyID.Equals(this.SourceID));
                this.SelectedDestination = this.BusinessPartners.UnfilteredList
                    .FirstOrDefault((d) => d.CompanyID.Equals(this.DestinationID));
                this.SelectedManifest = this.Manifests.UnfilteredList
                    .FirstOrDefault((m) => m.ManifestID.Equals(this.ManifestID));
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
                    this.purchaseOrderRepository.Insert(this.Model);
                }
                else
                {
                    this.purchaseOrderRepository.Update(this.Model);
                }

            },
            TaskCreationOptions.LongRunning),
            Messages.PurchaseOrderSaving,
            () =>
            {

                // request to remove from parent workspace
                this.CloseCommand.Execute(this);

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "Saving purchase order",
            string.Format(Messages.PurchaseOrderSaveError, this.Model.ToString()),
            log,
            () =>
            {
                // send load notification to the all purchase order view model
                this.MessengerInstance.Send<NotificationMessage<AllPurchaseOrderViewModel>>(
                    new NotificationMessage<AllPurchaseOrderViewModel>(null, null));

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
        /// Purchase Order model
        /// </summary>
        public PurchaseOrder Model;

        /// <summary>
        /// purchase Order repository
        /// </summary>
        private IPurchaseOrderRepository purchaseOrderRepository;

        /// <summary>
        /// Gets or sets the business partners.
        /// </summary>
        /// <value>
        /// The business partners.
        /// </value>
        public ObservableCollectionExtended<BusinessPartner> BusinessPartners { get; set; }

        /// <summary>
        /// Gets or sets the manifests.
        /// </summary>
        /// <value>
        /// The manifests.
        /// </value>
        public ObservableCollectionExtended<Manifest> Manifests { get; set; }

        private BusinessPartner _selectedSource;

        /// <summary>
        /// Gets or sets the selected source.
        /// </summary>
        /// <value>
        /// The selected source.
        /// </value>
        public BusinessPartner SelectedSource
        {
            get
            {
                return _selectedSource;
            }
            set
            {

                if (_selectedSource == value)
                {
                    return;
                }

                _selectedSource = value;

                if (_selectedSource != null)
                {
                    this.SourceID = _selectedSource.CompanyID;
                }

                base.OnPropertyChanged("SelectedSource");

            }
        }

        private BusinessPartner _selectedDestination;

        /// <summary>
        /// Gets or sets the selected destination.
        /// </summary>
        /// <value>
        /// The selected destination.
        /// </value>
        public BusinessPartner SelectedDestination
        {
            get
            {
                return _selectedDestination;
            }
            set
            {

                if (_selectedDestination == value)
                {
                    return;
                }

                _selectedDestination = value;

                if (_selectedDestination != null)
                {
                    this.DestinationID = _selectedDestination.CompanyID;
                }

                base.OnPropertyChanged("SelectedDestination");

            }
        }

        private Manifest _selectedManifest;

        /// <summary>
        /// Gets or sets the selected manifest.
        /// </summary>
        /// <value>
        /// The selected manifest.
        /// </value>
        public Manifest SelectedManifest
        {
            get
            {
                return _selectedManifest;
            }
            set
            {

                if (_selectedManifest == value)
                {
                    return;
                }

                _selectedManifest = value;

                if (_selectedManifest != null)
                {
                    this.ManifestID = _selectedManifest.ManifestID;
                }

                base.OnPropertyChanged("SelectedManifest");

            }
        }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        public long OrderID
        {
            get
            {
                return Model.OrderID;
            }
            set
            {

                if (Model.OrderID == value)
                {
                    return;
                }

                Model.OrderID = value;

                base.OnPropertyChanged("OrderID");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>
        /// The order number.
        /// </value>
        public long OrderNumber
        {
            get
            {
                return Model.OrderNumber;
            }
            set
            {

                if (Model.OrderNumber == value)
                {
                    return;
                }

                Model.OrderNumber = value;

                base.OnPropertyChanged("OrderNumber");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the source identifier.
        /// </summary>
        /// <value>
        /// The source identifier.
        /// </value>
        public long SourceID
        {
            get
            {
                return Model.SourceID;
            }
            set
            {

                if (Model.SourceID == value)
                {
                    return;
                }

                Model.SourceID = value;

                base.OnPropertyChanged("SourceID");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the destination identifier.
        /// </summary>
        /// <value>
        /// The destination identifier.
        /// </value>
        public long DestinationID
        {
            get
            {
                return Model.DestinationID;
            }
            set
            {

                if (Model.DestinationID == value)
                {
                    return;
                }

                Model.DestinationID = value;

                base.OnPropertyChanged("DestinationID");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the manifest identifier.
        /// </summary>
        /// <value>
        /// The manifest identifier.
        /// </value>
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
        /// Gets or sets a value indicating whether payment was made.
        /// </summary>
        /// <value>
        ///   <c>true</c> if payment made; otherwise, <c>false</c>.
        /// </value>
        public bool PaymentMade
        {
            get
            {
                return Model.PaymentMade;
            }
            set
            {

                if (Model.PaymentMade == value)
                {
                    return;
                }

                Model.PaymentMade = value;

                base.OnPropertyChanged("PaymentMade");
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
                string msg = string.Format(Messages.PurchaseOrderDisplayName, this.IsNew ? "New" : displayName);
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
