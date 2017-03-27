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

namespace CS499.TCMS.View.ViewModels
{
    /// <summary>
    /// This class will handle the maintenance of the <see cref="ManifestViewModel"/>
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class AllManifestViewModel : WorkspaceViewModel, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AllManifestViewModel"/> class.
        /// </summary>
        /// <param name="dialog">Dialog service to show messages from ViewModel</param>
        /// <param name="taskManager">Task manager to hold reference to running tasks</param>
        /// <param name="manifestRepository">The manifest repository.</param>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public AllManifestViewModel(IDialogService dialog, ITaskManager taskManager,
            IManifestRepository manifestRepository, IVehicleRepository vehicleRepository, 
            IUserRepository userRepository)
        {
            this.dialog = dialog;
            this.TaskManager = taskManager;
            this.IsSelected = true;
            this.ViewModels = new ObservableCollectionExtended<ManifestViewModel>(new List<ManifestViewModel>());
            this.Vehicles = new ObservableCollectionExtended<Vehicle>(new List<Vehicle>());
            this.Users = new ObservableCollectionExtended<User>(new List<User>());
            this.ViewModels.PageSize = 10;
            this.Vehicles.PageSize = 10;
            this.Users.PageSize = 10;
            this.DisplayName = Messages.AllManifestDisplayName;
            this.DisplayToolTip = Messages.AllManifestDisplayToolTip;
            this.manifestRepository = manifestRepository;
            this.vehicleRepository = vehicleRepository;
            this.userRepository = userRepository;
            this.Load();
            this.LoadVehicles();
            this.LoadUsers();
            this.MessengerInstance.Register<NotificationMessage<AllManifestViewModel>>(this, (n) => this.Load(n));
            this.MessengerInstance.Register<NotificationMessage<AllVehicleViewModel>>(this, (n) => this.LoadVehicles(n));
            this.MessengerInstance.Register<NotificationMessage<AllUserViewModel>>(this, (n) => this.LoadUsers(n));
            this.SearchType = "contains";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void Load(NotificationMessage<AllManifestViewModel> notificationMessage)
        {
            this.Load();
        }

        /// <summary>
        /// Load list of models
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void LoadVehicles(NotificationMessage<AllVehicleViewModel> notificationMessage)
        {
            this.LoadVehicles();
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

            List<Manifest> ViewModels = null;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                ViewModels = manifestRepository.GetAll().ToList();

            },
            TaskCreationOptions.LongRunning),
            Messages.AllManifestLoading,
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

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "loading manifests",
             Messages.AllManifestLoadError,
             log);

        }

        /// <summary>
        /// Add each ViewModel to the collection
        /// </summary>
        /// <param name="manifests">list of models</param>
        private void Set(List<Manifest> manifests)
        {

            // clear current list
            this.ViewModels.ClearAll();

            // loop through each model and add e ViewModel to the collection
            foreach (var model in manifests)
            {
                ManifestViewModel viewModel = new ManifestViewModel(model, this.manifestRepository, this.TaskManager, false, 
                    this.Vehicles, this.Users);
                this.ViewModels.AddItem(viewModel);
            }

        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void LoadVehicles()
        {

            List<Vehicle> models = null;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                models = vehicleRepository.GetAll().ToList();

            },
            TaskCreationOptions.LongRunning),
            Messages.AllVehicleLoading,
            () =>
            {

                if (models == null)
                {
                    return;
                }

                // set models
                this.Set(models);

                // refresh the list
                this.Vehicles.Refresh();

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "loading vehicles",
             Messages.AllVehicleLoadError,
             log);

        }

        /// <summary>
        /// Add each Model to the collection
        /// </summary>
        /// <param name="manifests">list of models</param>
        private void Set(List<Vehicle> vehicles)
        {

            // clear current list
            this.Vehicles.ClearAll();

            // loop through each model and add to the collection
            foreach (var model in vehicles)
            {                
                this.Vehicles.AddItem(model);
            }

        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void LoadUsers()
        {

            List<User> models = null;

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

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "loading vehicles",
             Messages.AllUserLoadError,
             log);

        }

        /// <summary>
        /// Add each Model to the collection
        /// </summary>
        /// <param name="manifests">list of models</param>
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
            Manifest model = new Manifest(0, "Outgoing", 0, DateTime.Now, DateTime.Now.AddDays(7), false, 0, 0);

            // create new ViewModel
            ManifestViewModel viewModel = new ManifestViewModel(model, this.manifestRepository, this.TaskManager, true,
                this.Vehicles, this.Users);

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
        private void SendViewModel(ManifestViewModel viewModel)
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
            ManifestViewModel viewModel = this.SelectedViewModel;

            // Ask viewModel to confirm the deletion
            string msg = string.Format(Messages.DeleteMessage, "manifest", viewModel.Model.ToString());
            MessageDialogResult result = await dialog.Dialog.ShowMessageAsync(dialog.ViewModel, Messages.TitleApp, msg,
                    MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Negative)
            {
                return;
            }

            // start task to remove viewModel
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                this.manifestRepository.Delete(viewModel.Model);

            },
            TaskCreationOptions.LongRunning),
            Messages.AllManifestDeleting,
            () =>
            {

                this.Load();

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "deleting manifest",
            string.Format(Messages.AllManifestDeleteError, viewModel.DisplayName),
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
                "ManifestID", "ShipmentType", "VehicleID", "DepartureTime", "ETA", "ShippingCost", 
                "EmployeeID");

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

        #endregion

        #region Properties

        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Manifest repository
        /// </summary>
        private IManifestRepository manifestRepository;

        /// <summary>
        /// The vehicle repository
        /// </summary>
        private IVehicleRepository vehicleRepository;

        /// <summary>
        /// The user repository
        /// </summary>
        private IUserRepository userRepository;

        /// <summary>
        /// Dialog service for showing messages from the ViewModel
        /// </summary>
        private IDialogService dialog;

        /// <summary>
        /// Selected ViewModel
        /// </summary>
        public ManifestViewModel SelectedViewModel { get; set; }

        /// <summary>
        /// Collection of ViewModels
        /// </summary>
        public ObservableCollectionExtended<ManifestViewModel> ViewModels { get; set; }

        /// <summary>
        /// Gets or sets the vehicles.
        /// </summary>
        /// <value>
        /// The vehicles.
        /// </value>
        public ObservableCollectionExtended<Vehicle> Vehicles { get; set; }

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
