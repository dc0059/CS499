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
    /// This class will handle the maintenance of the <see cref="PurchaseItem"/> model
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IChanges" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class PurchaseItemViewModel : WorkspaceViewModel, IDataErrorInfo, IChanges, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseItemViewModel"/> class.
        /// </summary>
        /// <param name="model">model for the purchase item</param>
        /// <param name="purchaseItemRepository">repository for database operations</param>
        /// <param name="taskManager">task manager to hold reference to running tasks</param>
        /// <param name="isNew">flag indicating if this is a new purchase item</param>
        /// <param name="purchaseOrders">The purchase orders.</param>
        /// <param name="parts">The parts.</param>
        /// <param name="partRepository">The part repository.</param>
        public PurchaseItemViewModel(PurchaseItem model, IPurchaseItemRepository purchaseItemRepository, ITaskManager taskManager, bool isNew,
            ObservableCollectionExtended<PurchaseOrder> purchaseOrders, ObservableCollectionExtended<Part> parts,
            IPartRepository partRepository)
        {
            this.Model = model;
            this.purchaseItemRepository = purchaseItemRepository;
            this.TaskManager = taskManager;
            this.IsNew = isNew;
            this.IsSelected = true;
            this.HasChanges = false;
            _partStatuses = new string[] { "Shipped", "On Back Order", "No Longer Available"};
            this.ContentId = model.ItemID.GetContentId(this.DisplayName);
            this.Orders = purchaseOrders;
            this.Parts = parts;
            this.partRepository = partRepository;
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
                this.SelectedPurchaseOrder = this.Orders.UnfilteredList.FirstOrDefault();
                this.SelectedPart = this.Parts.UnfilteredList.FirstOrDefault();
            }
            else
            {
                this.SelectedPurchaseOrder = this.Orders.UnfilteredList
                    .FirstOrDefault((p) => p.OrderID.Equals(this.OrderID));
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
                    this.purchaseItemRepository.Insert(this.Model);
                }
                else
                {
                    this.purchaseItemRepository.Update(this.Model);
                }

                // decrement quantity on stock
                this.SelectedPart.QuantityInStock -= this.Quantity;

                // update part 
                this.partRepository.Update(this.SelectedPart);

            },
            TaskCreationOptions.LongRunning),
            Messages.PurchaseItemSaving,
            () =>
            {

                // request to remove from parent workspace
                this.CloseCommand.Execute(this);

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "Saving purchase item",
            string.Format(Messages.PurchaseItemSaveError, this.Model.ToString()),
            log,
            () =>
            {
                // send load notification to the all purchase item view model
                this.MessengerInstance.Send<NotificationMessage<AllPurchaseItemViewModel>>(
                    new NotificationMessage<AllPurchaseItemViewModel>(null, null));

                // send load notification to the all part view model
                this.MessengerInstance.Send<NotificationMessage<AllPartViewModel>>(
                    new NotificationMessage<AllPartViewModel>(null, null));


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
        /// Purchase Item model
        /// </summary>
        public PurchaseItem Model;

        /// <summary>
        /// Purchase Item repository
        /// </summary>
        private IPurchaseItemRepository purchaseItemRepository;

        /// <summary>
        /// The part repository
        /// </summary>
        private IPartRepository partRepository;

        /// <summary>
        /// Gets or sets the purchase orders.
        /// </summary>
        /// <value>
        /// The purchase orders.
        /// </value>
        public ObservableCollectionExtended<PurchaseOrder> Orders { get; set; }

        private PurchaseOrder _selectedPurchaseOrder;

        /// <summary>
        /// Gets or sets the selected purchase order.
        /// </summary>
        /// <value>
        /// The selected purchase order.
        /// </value>
        public PurchaseOrder SelectedPurchaseOrder
        {
            get
            {
                return _selectedPurchaseOrder;
            }
            set
            {

                if (_selectedPurchaseOrder == value)
                {
                    return;
                }

                _selectedPurchaseOrder = value;

                if (_selectedPurchaseOrder != null)
                {
                    this.OrderID = _selectedPurchaseOrder.OrderID;
                }

                base.OnPropertyChanged("SelectedPurchaseOrder");

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
                base.OnPropertyChanged("MaxQuantity");

            }
        }

        /// <summary>
        /// Gets or sets the maximum quantity.
        /// </summary>
        /// <value>
        /// The maximum quantity.
        /// </value>
        public int MaxQuantity
        {
            get
            {
                if (this.SelectedPart != null)
                {
                    return this.SelectedPart.QuantityInStock;
                }

                return 0;

            }
        }

        private string[] _partStatuses;

        /// <summary>
        /// Gets the part statuses.
        /// </summary>
        /// <value>
        /// The part statuses.
        /// </value>
        public string[] PartStatuses
        {
            get
            {
                return _partStatuses;
            }
        }


        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        public long ItemID
        {
            get
            {
                return Model.ItemID;
            }
            set
            {

                if (Model.ItemID == value)
                {
                    return;
                }

                Model.ItemID = value;

                base.OnPropertyChanged("ItemID");
                this.HasChanges = true;

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
        /// Gets or sets the part status.
        /// </summary>
        /// <value>
        /// The part status.
        /// </value>
        public string PartStatus
        {
            get
            {
                return Model.PartStatus;
            }
            set
            {

                if (Model.PartStatus == value)
                {
                    return;
                }

                Model.PartStatus = value;

                base.OnPropertyChanged("PartStatus");
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
                string msg = string.Format(Messages.PurchaseItemDisplayName, this.IsNew ? "New" : displayName);
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
