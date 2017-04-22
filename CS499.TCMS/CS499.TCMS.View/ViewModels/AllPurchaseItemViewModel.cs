using CS499.TCMS.DataAccess.IRepositories;
using CS499.TCMS.Model;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Models;
using CS499.TCMS.View.Resources;
using CS499.TCMS.View.Services;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CS499.TCMS.View.ViewModels
{
    /// <summary>
    /// This class will handle the maintenance of the <see cref="PurchaseItemViewModel"/>
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class AllPurchaseItemViewModel : WorkspaceViewModel, IKeyCommand, IFilterable
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AllPurchaseItemViewModel"/> class.
        /// </summary>
        /// <param name="dialog">Dialog service to show messages from ViewModel</param>
        /// <param name="taskManager">Task manager to hold reference to running tasks</param>
        /// <param name="purchaseItemRepository">The purchase item repository.</param>
        /// <param name="purchaseOrderRepository">The purchaseOrder repository.</param>
        /// <param name="partRepository">The part repository.</param>
        public AllPurchaseItemViewModel(IDialogService dialog, ITaskManager taskManager,
            IPurchaseItemRepository purchaseItemRepository, IPurchaseOrderRepository purchaseOrderRepository,
            IPartRepository partRepository)
        {
            this.dialog = dialog;
            this.TaskManager = taskManager;
            this.IsSelected = true;
            this.ViewModels = new ObservableCollectionExtended<PurchaseItemViewModel>(new List<PurchaseItemViewModel>());
            this.Orders = new ObservableCollectionExtended<PurchaseOrder>(new List<PurchaseOrder>());
            this.Parts = new ObservableCollectionExtended<Part>(new List<Part>());
            this.ViewModels.PageSize = 10;
            this.Orders.PageSize = 100000;
            this.Parts.PageSize = 100000;
            this.DisplayName = Messages.AllPurchaseItemDisplayName;
            this.DisplayToolTip = Messages.AllPurchaseItemDisplayToolTip;
            this.purchaseItemRepository = purchaseItemRepository;
            this.purchaseOrderRepository = purchaseOrderRepository;
            this.partRepository = partRepository;
            this.LoadPurchaseOrders();
            this.LoadParts();
            this.Load();
            this.MessengerInstance.Register<NotificationMessage<AllPurchaseItemViewModel>>(this, (n) => this.Load(n));
            this.MessengerInstance.Register<NotificationMessage<AllPurchaseOrderViewModel>>(this, (n) => this.LoadPurchaseOrders(n));
            this.MessengerInstance.Register<NotificationMessage<AllPartViewModel>>(this, (n) => this.LoadParts(n));
            this.SearchType = "contains";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void Load(NotificationMessage<AllPurchaseItemViewModel> notificationMessage)
        {
            this.Load();
        }

        /// <summary>
        /// Load list of models
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void LoadPurchaseOrders(NotificationMessage<AllPurchaseOrderViewModel> notificationMessage)
        {
            this.LoadPurchaseOrders();
        }

        /// <summary>
        /// Load list of models
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void LoadParts(NotificationMessage<AllPartViewModel> notificationMessage)
        {
            this.LoadParts();
        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void Load()
        {

            List<PurchaseItem> ViewModels = null;

            // set loading flag
            this.loading = true;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                ViewModels = purchaseItemRepository.GetAll().ToList();

                // wait for other tasks to complete
                while (this.loadingPurchaseOrders || this.loadingParts)
                {
                    Task.Delay(500);
                }

            },
            TaskCreationOptions.LongRunning),
            Messages.AllPurchaseItemLoading,
            () =>
            {

                if (ViewModels == null)
                {
                    return;
                }

                // set ViewModels
                this.Set(ViewModels);

                // refresh the list
                this.ViewModels.Refresh();

                // apply filter
                if (this.Filter != null)
                {
                    (this as IFilterable).ApplyFilter(this.Filter);
                }

                // set loading flag
                this.loading = false;

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "loading purchase item",
             Messages.AllPurchaseItemLoadError,
             log);

        }

        /// <summary>
        /// Add each ViewModel to the collection
        /// </summary>
        /// <param name="purchaseItem">list of models</param>
        private void Set(List<PurchaseItem> purchaseItem)
        {

            // clear current list
            this.ViewModels.ClearAll();

            // loop through each model and add e ViewModel to the collection
            foreach (var model in purchaseItem)
            {
                PurchaseItemViewModel viewModel = new PurchaseItemViewModel(model, this.purchaseItemRepository, this.TaskManager, false,
                    this.Orders, this.Parts, this.partRepository);
                this.ViewModels.AddItem(viewModel);
            }

        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void LoadPurchaseOrders()
        {

            List<PurchaseOrder> models = null;

            // set loading flag
            this.loadingPurchaseOrders = true;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                models = purchaseOrderRepository.GetAll().ToList();

            },
            TaskCreationOptions.LongRunning),
            Messages.AllPurchaseOrderLoading,
            () =>
            {

                if (models == null)
                {
                    return;
                }

                // set models
                this.Set(models);

                // refresh the list
                this.Orders.Refresh();

                // set loading flag
                this.loadingPurchaseOrders = false;

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "loading purchase orders",
             Messages.AllPurchaseOrderLoadError,
             log);

        }

        /// <summary>
        /// Add each Model to the collection
        /// </summary>
        /// <param name="purchaseOrders">list of models</param>
        private void Set(List<PurchaseOrder> purchaseOrders)
        {

            // clear current list
            this.Orders.ClearAll();

            // loop through each model and add to the collection
            foreach (var model in purchaseOrders)
            {
                this.Orders.AddItem(model);
            }

        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void LoadParts()
        {

            List<Part> models = null;

            // set loading flag
            this.loadingParts = true;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                models = partRepository.GetAll().ToList();

            },
            TaskCreationOptions.LongRunning),
            Messages.AllPartLoading,
            () =>
            {

                if (models == null)
                {
                    return;
                }

                // set models
                this.Set(models);

                // refresh the list
                this.Parts.Refresh();

                // set loading flag
                this.loadingParts = false;

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "loading parts",
             Messages.AllPartLoadError,
             log);

        }

        /// <summary>
        /// Add each Model to the collection
        /// </summary>
        /// <param name="parts">list of models</param>
        private void Set(List<Part> parts)
        {

            // clear current list
            this.Parts.ClearAll();

            // loop through each model and add to the collection
            foreach (var model in parts)
            {
                this.Parts.AddItem(model);
            }

        }

        /// <summary>
        /// Create new instance of the ViewModel
        /// </summary>
        private void New()
        {

            // create new model
            PurchaseItem model = new PurchaseItem(0, 0, 0, 0, string.Empty);

            // create new ViewModel
            PurchaseItemViewModel viewModel = new PurchaseItemViewModel(model, this.purchaseItemRepository, this.TaskManager, true,
                this.Orders, this.Parts, this.partRepository);

            // set selected order number to filter
            if (this.Filter != null)
            {
                viewModel.SelectedPurchaseOrder = this.Orders.FirstOrDefault(
                    (p) => p.OrderNumber == Convert.ToInt64(this.Filter.FilterText));
            }

            // send ViewModel
            this.SendViewModel(viewModel);

        }

        /// <summary>
        /// Edit instance of the ViewModel
        /// </summary>
        private void Edit()
        {

            // send selected view model
            this.SendViewModel(this.SelectedViewModel);

        }

        /// <summary>
        /// send message to the mainwindowViewModel to add the ViewModel to the collection
        /// </summary>
        /// <param name="viewModel">ViewModel</param>
        private void SendViewModel(PurchaseItemViewModel viewModel)
        {
            this.MessengerInstance.Send<NotificationMessage<WorkspaceViewModel>>(
                new NotificationMessage<WorkspaceViewModel>(viewModel, null));
        }

        /// <summary>
        /// Delete instance of the ViewModel
        /// </summary>
        private async void Delete()
        {

            // get selected ViewModel
            PurchaseItemViewModel viewModel = this.SelectedViewModel;

            // Ask viewModel to confirm the deletion
            string msg = string.Format(Messages.DeleteMessage, "purchase item", viewModel.Model.ToString());
            MessageDialogResult result = await dialog.Dialog.ShowMessageAsync(dialog.ViewModel, Messages.TitleApp, msg,
                    MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Negative)
            {
                return;
            }

            // start task to remove viewModel
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                // increment quantity in stock
                this.partRepository.UpdateQuantityInStock(-viewModel.Model.Quantity, viewModel.Model.PartID);

                // delete model
                this.purchaseItemRepository.Delete(viewModel.Model);

            },
            TaskCreationOptions.LongRunning),
            Messages.AllPurchaseItemDeleting,
            () =>
            {

                this.MessengerInstance.Send<NotificationMessage<AllPurchaseItemViewModel>>(null, null);

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "deleting purchase item",
            string.Format(Messages.AllPurchaseItemDeleteError, viewModel.DisplayName),
            log);

        }

        /// <summary>
        /// Request the ViewModel be closed
        /// </summary>
        public override void OnRequestClose()
        {

            // unregister ViewModel
            this.MessengerInstance.Unregister(this);
            base.OnRequestClose();
        }

        /// <summary>
        /// Search for the manifest based on the search text
        /// </summary>
        private void SearchForText()
        {

            this.SelectedViewModel = this.ViewModels.Search(this.SearchType, this.SearchText,
                "ItemID", "SelectedPurchaseOrder", "Quantity", "SelectedPart", "PartStatus");

        }

        /// <summary>
        /// Execute commands based on the key combination pressed
        /// </summary>
        /// <param name="e">key event args</param>
        void IKeyCommand.SendKeys(KeyEventArgs e)
        {

            if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) && e.Key == Key.N)
            {
                this.New();
            }
            else if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) && e.Key == Key.E)
            {
                if (this.CanEditOrDelete)
                {
                    this.Edit();
                }
            }
            else if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) && e.Key == Key.D)
            {
                if (this.CanEditOrDelete)
                {
                    this.Delete();
                }
            }
            else if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrEmpty(this.SearchText))
                {
                    this.SearchForText();
                }
            }

        }

        /// <summary>
        /// Apply filter
        /// </summary>
        /// <param name="filter">the filter</param>
        void IFilterable.ApplyFilter(Filter filter)
        {

            // remember filter
            this.Filter = filter;

            // set search type
            this.SearchType = "equals";

            // set search text
            this.SearchText = filter.FilterText;

            // apply filter
            this.ViewModels.Search(this.SearchType, this.SearchText, filter.PropertyName);

        }

        #endregion

        #region Properties

        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Purchase Item repository
        /// </summary>
        private IPurchaseItemRepository purchaseItemRepository;

        /// <summary>
        /// The purchase order repository
        /// </summary>
        private IPurchaseOrderRepository purchaseOrderRepository;

        /// <summary>
        /// The part repository
        /// </summary>
        private IPartRepository partRepository;

        /// <summary>
        /// Dialog service for showing messages from the ViewModel
        /// </summary>
        private IDialogService dialog;

        /// <summary>
        /// Flag indicating the purchase items are loading
        /// </summary>
        private bool loading = false;

        /// <summary>
        /// Flag indicating the purchase orders are loading
        /// </summary>
        private bool loadingPurchaseOrders = false;

        /// <summary>
        /// Flag indicating the parts are loading
        /// </summary>
        private bool loadingParts = false;

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        public Filter Filter { get; set; }

        /// <summary>
        /// Flag indicating the ViewModel is still loading data
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return this.loadingPurchaseOrders || this.loadingParts || this.loading;
            }
        }

        /// <summary>
        /// Selected ViewModel
        /// </summary>
        public PurchaseItemViewModel SelectedViewModel { get; set; }

        /// <summary>
        /// Collection of ViewModels
        /// </summary>
        public ObservableCollectionExtended<PurchaseItemViewModel> ViewModels { get; set; }

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public ObservableCollectionExtended<PurchaseOrder> Orders { get; set; }

        /// <summary>
        /// Gets or sets the parts.
        /// </summary>
        /// <value>
        /// The parts.
        /// </value>
        public ObservableCollectionExtended<Part> Parts { get; set; }

        private string _searchType;

        /// <summary>
        /// Search type
        /// </summary>
        public string SearchType
        {
            get
            {
                return _searchType;
            }
            set
            {

                if (_searchType == value)
                {
                    return;
                }

                _searchType = value;
                base.OnPropertyChanged("SearchType");

            }
        }


        private string _searchText;

        /// <summary>
        /// String to find in the list
        /// </summary>
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {

                if (_searchText == value)
                {
                    return;
                }

                if (string.IsNullOrEmpty(value))
                {
                    this.ViewModels.Refresh();
                }

                _searchText = value;

                base.OnPropertyChanged("SearchText");

            }
        }

        /// <summary>
        /// Search watermark
        /// </summary>
        public string SearchWatermark
        {
            get { return Messages.SearchWatermark; }
        }

        /// <summary>
        /// Flag indicating if the selected ViewModel can be edited or deleted
        /// </summary>
        public bool CanEditOrDelete
        {
            get
            {
                return this.SelectedViewModel != null;
            }
        }

        private ICommand _commandNew;

        /// <summary>
        /// Command to execute ViewModel creation
        /// </summary>
        public ICommand CommandNew
        {
            get
            {

                if (_commandNew == null)
                {
                    _commandNew = new RelayCommand(param =>
                    {
                        this.New();
                    });
                }

                return _commandNew;
            }
        }

        private ICommand _commandEdit;

        /// <summary>
        /// Command to execute ViewModel edit
        /// </summary>
        public ICommand CommandEdit
        {
            get
            {

                if (_commandEdit == null)
                {
                    _commandEdit = new RelayCommand(param =>
                    {
                        this.Edit();
                    },
                        param => this.CanEditOrDelete);
                }

                return _commandEdit;
            }
        }

        private ICommand _commandDelete;

        /// <summary>
        /// Command to execute ViewModel delete
        /// </summary>
        public ICommand CommandDelete
        {
            get
            {

                if (_commandDelete == null)
                {
                    _commandDelete = new RelayCommand(param =>
                    {
                        this.Delete();
                    },
                        param => this.CanEditOrDelete);
                }

                return _commandDelete;
            }
        }

        private ICommand _commandSearch;

        /// <summary>
        /// Command to search the ViewModel collection for the search text
        /// </summary>
        public ICommand CommandSearch
        {
            get
            {

                if (_commandSearch == null)
                {
                    _commandSearch = new RelayCommand(param =>
                    {
                        this.SearchForText();
                    },
                        param => !string.IsNullOrEmpty(this.SearchText));
                }

                return _commandSearch;
            }
        }

        /// <summary>
        /// Returns e selected prop for the object
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

        #endregion

    }
}
