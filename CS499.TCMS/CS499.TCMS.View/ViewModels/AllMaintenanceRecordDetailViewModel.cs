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
    /// This class will handle the maintenance of the <see cref="MaintenanceRecordDetailViewModel"/>
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class AllMaintenanceRecordDetailViewModel : WorkspaceViewModel, IKeyCommand, IFilterable
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AllMaintenanceRecordDetailViewModel"/> class.
        /// </summary>
        /// <param name="dialog">Dialog service to show messages from ViewModel</param>
        /// <param name="taskManager">Task manager to hold reference to running tasks</param>
        /// <param name="maintenanceRecordDetailRepository">The maintenanceRecordDetail repository.</param>
        /// <param name="maintenanceRecordRepository">The maintenance record repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public AllMaintenanceRecordDetailViewModel(IDialogService dialog, ITaskManager taskManager,
            IMaintenanceRecordDetailRepository maintenanceRecordDetailRepository, IMaintenanceRecordRepository maintenanceRecordRepository,
            IUserRepository userRepository)
        {
            this.dialog = dialog;
            this.TaskManager = taskManager;
            this.IsSelected = true;
            this.ViewModels = new ObservableCollectionExtended<MaintenanceRecordDetailViewModel>(new List<MaintenanceRecordDetailViewModel>());
            this.MaintenanceRecords = new ObservableCollectionExtended<MaintenanceRecord>(new List<MaintenanceRecord>());
            this.Users = new ObservableCollectionExtended<User>(new List<User>());
            this.ViewModels.PageSize = 10;
            this.MaintenanceRecords.PageSize = 100000;
            this.Users.PageSize = 100000;
            this.DisplayName = Messages.AllMaintenanceRecordDetailDisplayName;
            this.DisplayToolTip = Messages.AllMaintenanceRecordDetailDisplayToolTip;
            this.maintenanceRecordDetailRepository = maintenanceRecordDetailRepository;
            this.maintenanceRecordRepository = maintenanceRecordRepository;
            this.userRepository = userRepository;
            this.LoadMaintenanceRecords();
            this.LoadUsers();
            this.Load();
            this.MessengerInstance.Register<NotificationMessage<AllMaintenanceRecordDetailViewModel>>(this, (n) => this.Load(n));
            this.MessengerInstance.Register<NotificationMessage<AllMaintenanceRecordViewModel>>(this, (n) => this.LoadMaintenanceRecords(n));
            this.MessengerInstance.Register<NotificationMessage<AllUserViewModel>>(this, (n) => this.LoadUsers(n));
            this.SearchType = "contains";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void Load(NotificationMessage<AllMaintenanceRecordDetailViewModel> notificationMessage)
        {
            this.Load();
        }

        /// <summary>
        /// Load list of models
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void LoadMaintenanceRecords(NotificationMessage<AllMaintenanceRecordViewModel> notificationMessage)
        {
            this.LoadMaintenanceRecords();
        }

        /// <summary>
        /// Load list of models
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void LoadUsers(NotificationMessage<AllUserViewModel> notificationMessage)
        {
            this.LoadUsers();
        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void Load()
        {

            List<MaintenanceRecordDetail> ViewModels = null;

            // set loading flag
            this.loading = true;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                ViewModels = maintenanceRecordDetailRepository.GetAll().ToList();

                // wait for other tasks to complete
                while (this.loadingRecords || this.loadingUsers)
                {
                    Task.Delay(500);
                }

            },
            TaskCreationOptions.LongRunning),
            Messages.AllMaintenanceRecordDetailLoading,
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
             "loading maintenance record details",
             Messages.AllMaintenanceRecordDetailLoadError,
             log);

        }

        /// <summary>
        /// Add each ViewModel to the collection
        /// </summary>
        /// <param name="maintenanceRecordDetails">list of models</param>
        private void Set(List<MaintenanceRecordDetail> maintenanceRecordDetails)
        {

            // clear current list
            this.ViewModels.ClearAll();

            // loop through each model and add e ViewModel to the collection
            foreach (var model in maintenanceRecordDetails)
            {
                MaintenanceRecordDetailViewModel viewModel = new MaintenanceRecordDetailViewModel(model, this.maintenanceRecordDetailRepository, this.TaskManager, false,
                    this.MaintenanceRecords, this.Users);
                this.ViewModels.AddItem(viewModel);
            }

        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void LoadMaintenanceRecords()
        {

            List<MaintenanceRecord> models = null;

            // set loading flag
            this.loadingRecords = true;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                models = maintenanceRecordRepository.GetAll().ToList();

            },
            TaskCreationOptions.LongRunning),
            Messages.AllMaintenanceRecordLoading,
            () =>
            {

                if (models == null)
                {
                    return;
                }

                // set models
                this.Set(models);

                // refresh the list
                this.MaintenanceRecords.Refresh();

                // set loading flag
                this.loadingRecords = false;

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "loading maintenance records",
             Messages.AllMaintenanceRecordLoadError,
             log);

        }

        /// <summary>
        /// Add each Model to the collection
        /// </summary>
        /// <param name="maintenanceRecords">list of models</param>
        private void Set(List<MaintenanceRecord> maintenanceRecords)
        {

            // clear current list
            this.MaintenanceRecords.ClearAll();

            // loop through each model and add to the collection
            foreach (var model in maintenanceRecords)
            {
                this.MaintenanceRecords.AddItem(model);
            }

        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void LoadUsers()
        {

            List<User> models = null;

            // set loading flag
            this.loadingUsers = true;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                models = userRepository.GetAll().ToList();

            },
            TaskCreationOptions.LongRunning),
            Messages.AllUserLoading,
            () =>
            {

                if (models == null)
                {
                    return;
                }

                // set models
                this.Set(models);

                // refresh the list
                this.Users.Refresh();

                // set loading flag
                this.loadingUsers = false;

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "loading users",
             Messages.AllUserLoadError,
             log);

        }

        /// <summary>
        /// Add each Model to the collection
        /// </summary>
        /// <param name="users">list of models</param>
        private void Set(List<User> users)
        {

            // clear current list
            this.Users.ClearAll();

            // loop through each model and add to the collection
            foreach (var model in users)
            {
                this.Users.AddItem(model);
            }

        }

        /// <summary>
        /// Create new instance of the ViewModel
        /// </summary>
        private void New()
        {

            // create new model
            MaintenanceRecordDetail model = new MaintenanceRecordDetail(0, 0, 0, string.Empty, DateTime.Now);

            // create new ViewModel
            MaintenanceRecordDetailViewModel viewModel = new MaintenanceRecordDetailViewModel(model, this.maintenanceRecordDetailRepository, this.TaskManager, true,
                this.MaintenanceRecords, this.Users);

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
        private void SendViewModel(MaintenanceRecordDetailViewModel viewModel)
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
            MaintenanceRecordDetailViewModel viewModel = this.SelectedViewModel;

            // Ask viewModel to confirm the deletion
            string msg = string.Format(Messages.DeleteMessage, "maintenance record detail", viewModel.Model.ToString());
            MessageDialogResult result = await dialog.Dialog.ShowMessageAsync(dialog.ViewModel, Messages.TitleApp, msg,
                    MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Negative)
            {
                return;
            }

            // start task to remove viewModel
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                this.maintenanceRecordDetailRepository.Delete(viewModel.Model);

            },
            TaskCreationOptions.LongRunning),
            Messages.AllMaintenanceRecordDetailDeleting,
            () =>
            {

                this.Load();

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "deleting maintenance record detail",
            string.Format(Messages.AllMaintenanceRecordDetailDeleteError, viewModel.DisplayName),
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
        /// Search for the maintenanceRecordDetail based on the search text
        /// </summary>
        private void SearchForText()
        {

            this.SelectedViewModel = this.ViewModels.Search(this.SearchType, this.SearchText,
                "DetailID", "MaintenanceID", "SelectedUser", "RepairDescription", "RepairDate");

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
        /// Send message to open AllMaintenancePartViewModel and apply filter
        /// </summary>
        private void OpenLink()
        {
            AllMaintenancePartViewModel viewModel = null;

            // start task to create ViewModel
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                // create ViewModel
                viewModel = WorkspaceFactory.Create<AllMaintenancePartViewModel>(this.dialog, this.TaskManager, 
                    Factory.Create<IMaintenancePartRepository>(),
                    Factory.Create<IMaintenanceRecordDetailRepository>(), 
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
                    new Filter("DetailID", this.SelectedViewModel.DetailID.ToString(), viewModel));

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
        /// Maintenance Record Detail repository
        /// </summary>
        private IMaintenanceRecordDetailRepository maintenanceRecordDetailRepository;

        /// <summary>
        /// The maintenance Record repository
        /// </summary>
        private IMaintenanceRecordRepository maintenanceRecordRepository;

        /// <summary>
        /// The user repository
        /// </summary>
        private IUserRepository userRepository;

        /// <summary>
        /// Dialog service for showing messages from the ViewModel
        /// </summary>
        private IDialogService dialog;

        /// <summary>
        /// Flag indicating the maintenance record details are loading
        /// </summary>
        private bool loading = false;

        /// <summary>
        /// Flag indicating the maintenance records are loading
        /// </summary>
        private bool loadingRecords = false;

        /// <summary>
        /// Flag indicating the users are loading
        /// </summary>
        private bool loadingUsers = false;

        /// <summary>
        /// Flag indicating the ViewModel is still loading data
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return this.loadingRecords || this.loadingUsers || this.loading;
            }
        }

        /// <summary>
        /// Selected ViewModel
        /// </summary>
        public MaintenanceRecordDetailViewModel SelectedViewModel { get; set; }

        /// <summary>
        /// Collection of ViewModels
        /// </summary>
        public ObservableCollectionExtended<MaintenanceRecordDetailViewModel> ViewModels { get; set; }

        /// <summary>
        /// Gets or sets the maintenance Records.
        /// </summary>
        /// <value>
        /// The maintenance Records.
        /// </value>
        public ObservableCollectionExtended<MaintenanceRecord> MaintenanceRecords { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public ObservableCollectionExtended<User> Users { get; set; }

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
