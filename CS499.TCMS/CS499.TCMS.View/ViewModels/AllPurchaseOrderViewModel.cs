using CS499.TCMS.DataAccess.IRepositories;
using CS499.TCMS.Model;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Resources;
using CS499.TCMS.View.Services;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CS499.TCMS.View.Models;

namespace CS499.TCMS.View.ViewModels
{
    /// <summary>
    /// This class will handle the maintenance of the <see cref="PurchaseOrder"/>
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class AllPurchaseOrderViewModel : WorkspaceViewModel, IKeyCommand, IFilterable
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AllPurchaseOrderViewModel"/> class.
        /// </summary>
        /// <param name="dialog">Dialog service to show messages from ViewModel</param>
        /// <param name="taskManager">Task manager to hold reference to running tasks</param>
        /// <param name="purchaseOrderRepository">The purchase order repository.</param>
        /// <param name="businessPartnerRepository">The business partner repository.</param>
        /// <param name="manifestRepository">The manifest repository.</param>
        public AllPurchaseOrderViewModel(IDialogService dialog, ITaskManager taskManager, IPurchaseOrderRepository purchaseOrderRepository,
            IBusinessPartnerRepository businessPartnerRepository, IManifestRepository manifestRepository)
        {
            this.dialog = dialog;
            this.TaskManager = taskManager;
            this.IsSelected = true;
            this.ViewModels = new ObservableCollectionExtended<PurchaseOrderViewModel>(new List<PurchaseOrderViewModel>());
            this.BusinessPartners = new ObservableCollectionExtended<BusinessPartner>(new List<BusinessPartner>());
            this.Manifests = new ObservableCollectionExtended<Manifest>(new List<Manifest>());
            this.ViewModels.PageSize = 10;
            this.BusinessPartners.PageSize = 100000;
            this.Manifests.PageSize = 100000;
            this.DisplayName = Messages.AllPurchaseOrderDisplayName;
            this.DisplayToolTip = Messages.AllPurchaseOrderDisplayToolTip;
            this.purchaseOrderRepository = purchaseOrderRepository;
            this.businessPartnerRepository = businessPartnerRepository;
            this.manifestRepository = manifestRepository;
            this.LoadBusinessPartners();
            this.LoadManifests();
            this.Load();
            this.MessengerInstance.Register<NotificationMessage<AllPurchaseOrderViewModel>>(this, (n) => this.Load(n));
            this.MessengerInstance.Register<NotificationMessage<AllBusinessPartnerViewModel>>(this, (n) => this.LoadBusinessPartners(n));
            this.MessengerInstance.Register<NotificationMessage<AllManifestViewModel>>(this, (n) => this.LoadManifests(n));
            this.SearchType = "contains";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void Load(NotificationMessage<AllPurchaseOrderViewModel> notificationMessage)
        {
            this.Load();
        }

        /// <summary>
        /// Load list of models
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void LoadBusinessPartners(NotificationMessage<AllBusinessPartnerViewModel> notificationMessage)
        {
            this.LoadBusinessPartners();
        }

        /// <summary>
        /// Load list of models
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void LoadManifests(NotificationMessage<AllManifestViewModel> notificationMessage)
        {
            this.LoadManifests();
        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void Load()
        {

            List<PurchaseOrder> ViewModels = null;

            // set loading flag
            this.loading = true;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                ViewModels = purchaseOrderRepository.GetAll().ToList();

                // wait for other tasks to complete
                while (this.loadingBusinessPartners || this.loadingManifests)
                {
                    Task.Delay(500);
                }

            },
            TaskCreationOptions.LongRunning),
            Messages.AllPurchaseOrderLoading,
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

                // set loading flag
                this.loading = false;

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "loading purchase orders",
             Messages.AllPurchaseOrderLoadError,
             log);

        }

        /// <summary>
        /// Add each ViewModel to the collection
        /// </summary>
        /// <param name="purchaseOrders">list of models</param>
        private void Set(List<PurchaseOrder> purchaseOrders)
        {

            // clear current list
            this.ViewModels.ClearAll();

            // loop through each model and add e ViewModel to the collection
            foreach (var model in purchaseOrders)
            {
                PurchaseOrderViewModel viewModel = new PurchaseOrderViewModel(model, this.purchaseOrderRepository, this.TaskManager, false,
                    this.BusinessPartners, this.Manifests);
                this.ViewModels.AddItem(viewModel);
            }

        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void LoadBusinessPartners()
        {

            List<BusinessPartner> models = null;

            // set loading flag 
            this.loadingBusinessPartners = true;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                models = businessPartnerRepository.GetAll().ToList();

            },
            TaskCreationOptions.LongRunning),
            Messages.AllBusinessPartnerLoading,
            () =>
            {

                if (models == null)
                {
                    return;
                }

                // set models
                this.Set(models);

                // refresh the list
                this.BusinessPartners.Refresh();

                // set loading flag 
                this.loadingBusinessPartners = false;

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "loading business partners",
             Messages.AllBusinessPartnerLoadError,
             log);

        }

        /// <summary>
        /// Add each Model to the collection
        /// </summary>
        /// <param name="businessPartners">list of models</param>
        private void Set(List<BusinessPartner> businessPartners)
        {

            // clear current list
            this.BusinessPartners.ClearAll();

            // loop through each model and add to the collection
            foreach (var model in businessPartners)
            {
                this.BusinessPartners.AddItem(model);
            }

        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void LoadManifests()
        {

            List<Manifest> models = null;

            // set loading flag
            this.loadingManifests = true;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                models = manifestRepository.GetAll().ToList();

            },
            TaskCreationOptions.LongRunning),
            Messages.AllManifestLoading,
            () =>
            {

                if (models == null)
                {
                    return;
                }

                // set models
                this.Set(models);

                // refresh the list
                this.Manifests.Refresh();

                // set loading flag
                this.loadingManifests = false;

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "loading manifests",
             Messages.AllManifestLoadError,
             log);

        }

        /// <summary>
        /// Add each Model to the collection
        /// </summary>
        /// <param name="manifests">list of models</param>
        private void Set(List<Manifest> manifests)
        {

            // clear current list
            this.Manifests.ClearAll();

            // loop through each model and add to the collection
            foreach (var model in manifests)
            {
                this.Manifests.AddItem(model);
            }

        }

        /// <summary>
        /// Create new instance of the ViewModel
        /// </summary>
        private void New()
        {

            // create new model
            PurchaseOrder model = new PurchaseOrder(0, 0, 0, 0, 0, false);

            // create new ViewModel
            PurchaseOrderViewModel viewModel = new PurchaseOrderViewModel(model, this.purchaseOrderRepository, this.TaskManager, true,
                this.BusinessPartners, this.Manifests);

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
        private void SendViewModel(PurchaseOrderViewModel viewModel)
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
            PurchaseOrderViewModel viewModel = this.SelectedViewModel;

            // Ask viewModel to confirm the deletion
            string msg = string.Format(Messages.DeleteMessage, "purchase order", viewModel.Model.ToString());
            MessageDialogResult result = await dialog.Dialog.ShowMessageAsync(dialog.ViewModel, Messages.TitleApp, msg,
                    MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Negative)
            {
                return;
            }

            // start task to remove viewModel
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                this.purchaseOrderRepository.Delete(viewModel.Model);

            },
            TaskCreationOptions.LongRunning),
            Messages.AllPurchaseOrderDeleting,
            () =>
            {

                this.Load();

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "deleting purchase order",
            string.Format(Messages.AllPurchaseOrderDeleteError, viewModel.DisplayName),
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
        /// Search for the purchaseOrder based on the search text
        /// </summary>
        private void SearchForText()
        {

            this.SelectedViewModel = this.ViewModels.Search(this.SearchType, this.SearchText,
                "OrderID", "OrderNumber", "SelectedSource", "SelectedDestination", "ManifestID");

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

            // set search type
            this.SearchType = "equals";

            // set search text
            this.SearchText = filter.FilterText;

            // apply filter
            this.ViewModels.Search(this.SearchType, this.SearchText, filter.PropertyName);

        }

        /// <summary>
        /// Send message to open AllPurchaseItemViewModel and apply filter
        /// </summary>
        private void OpenLink()
        {
            AllPurchaseItemViewModel viewModel = null;

            // start task to create ViewModel
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                // create ViewModel
                viewModel = WorkspaceFactory.Create<AllPurchaseItemViewModel>(this.dialog, this.TaskManager, 
                    Factory.Create<IPurchaseItemRepository>(),
                    Factory.Create<IPurchaseOrderRepository>(), 
                    Factory.Create<IPartRepository>());

                // wait for ViewModel to finish loading
                while (viewModel.IsLoading)
                {
                    Task.Delay(500);
                }

            },
            TaskCreationOptions.LongRunning),
            string.Format(Messages.OpenLink, Messages.AllPurchaseOrderDisplayName),
            () =>
            {

                // send message to open
                this.MessengerInstance.Send(
                    new Filter("SelectedPurchaseOrder", this.SelectedViewModel.OrderNumber.ToString(), viewModel));

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "Opening link",
            Messages.OpenLinkError,
            log);

        }

        #endregion

        #region Properties

        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Purchase Order repository
        /// </summary>
        private IPurchaseOrderRepository purchaseOrderRepository;

        /// <summary>
        /// The business partner repository
        /// </summary>
        private IBusinessPartnerRepository businessPartnerRepository;

        /// <summary>
        /// The manifest repository
        /// </summary>
        private IManifestRepository manifestRepository;

        /// <summary>
        /// Dialog service for showing messages from the ViewModel
        /// </summary>
        private IDialogService dialog;

        /// <summary>
        /// Flag indicating the purchase orders are loading
        /// </summary>
        private bool loading = false;

        /// <summary>
        /// Flag indicating the manifests are loading
        /// </summary>
        private bool loadingManifests = false;

        /// <summary>
        /// Flag indicating the business partners are loading
        /// </summary>
        private bool loadingBusinessPartners = false;

        /// <summary>
        /// Flag indicating the ViewModel is still loading data
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return this.loadingBusinessPartners || this.loadingManifests || this.loading;
            }
        }

        /// <summary>
        /// Gets or sets the business partners.
        /// </summary>
        /// <value>
        /// The business partners.
        /// </value>
        public ObservableCollectionExtended<BusinessPartner> BusinessPartners { get; set; }

        /// <summary>
        /// Gets or sets the purchaseOrders.
        /// </summary>
        /// <value>
        /// The purchaseOrders.
        /// </value>
        public ObservableCollectionExtended<Manifest> Manifests { get; set; }

        /// <summary>
        /// Selected ViewModel
        /// </summary>
        public PurchaseOrderViewModel SelectedViewModel { get; set; }

        /// <summary>
        /// Collection of ViewModels
        /// </summary>
        public ObservableCollectionExtended<PurchaseOrderViewModel> ViewModels { get; set; }

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

        private ICommand _commandOpenLink;

        /// <summary>
        /// Command to execute open link
        /// </summary>
        public ICommand CommandOpenLink
        {
            get
            {

                if (_commandOpenLink == null)
                {
                    _commandOpenLink = new RelayCommand(param =>
                    {
                        this.OpenLink();
                    });
                }

                return _commandOpenLink;
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
