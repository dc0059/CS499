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
    /// This class will handle the maintenance of the <see cref="MaintenancePartViewModel"/>
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class AllMaintenancePartViewModel : WorkspaceViewModel, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AllMaintenancePartViewModel"/> class.
        /// </summary>
        /// <param name="dialog">Dialog service to show messages from ViewModel</param>
        /// <param name="taskManager">Task manager to hold reference to running tasks</param>
        /// <param name="maintenancePartRepository">The maintenance part repository.</param>
        /// <param name="maintenanceRecordDetailRepository">The maintenance record detail repository.</param>
        /// <param name="partRepository">The part repository.</param>
        public AllMaintenancePartViewModel(IDialogService dialog, ITaskManager taskManager,
            IMaintenancePartRepository maintenancePartRepository, IMaintenanceRecordDetailRepository maintenanceRecordDetailRepository,
            IPartRepository partRepository)
        {
            this.dialog = dialog;
            this.TaskManager = taskManager;
            this.IsSelected = true;
            this.ViewModels = new ObservableCollectionExtended<MaintenancePartViewModel>(new List<MaintenancePartViewModel>());
            this.MaintenanceRecordDetails = new ObservableCollectionExtended<MaintenanceRecordDetail>(new List<MaintenanceRecordDetail>());
            this.Parts = new ObservableCollectionExtended<Part>(new List<Part>());
            this.ViewModels.PageSize = 10;
            this.MaintenanceRecordDetails.PageSize = 100000;
            this.Parts.PageSize = 100000;
            this.DisplayName = Messages.AllMaintenancePartDisplayName;
            this.DisplayToolTip = Messages.AllMaintenancePartDisplayToolTip;
            this.maintenancePartRepository = maintenancePartRepository;
            this.maintenanceRecordDetailRepository = maintenanceRecordDetailRepository;
            this.partRepository = partRepository;
            this.LoadMaintenanceRecordDetails();
            this.LoadParts();
            this.Load();
            this.MessengerInstance.Register<NotificationMessage<AllMaintenancePartViewModel>>(this, (n) => this.Load(n));
            this.MessengerInstance.Register<NotificationMessage<AllMaintenanceRecordDetailViewModel>>(this, (n) => this.LoadMaintenanceRecordDetails(n));
            this.MessengerInstance.Register<NotificationMessage<AllPartViewModel>>(this, (n) => this.LoadParts(n));
            this.SearchType = "contains";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void Load(NotificationMessage<AllMaintenancePartViewModel> notificationMessage)
        {
            this.Load();
        }

        /// <summary>
        /// Load list of models
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void LoadMaintenanceRecordDetails(NotificationMessage<AllMaintenanceRecordDetailViewModel> notificationMessage)
        {
            this.LoadMaintenanceRecordDetails();
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

            List<MaintenancePart> ViewModels = null;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                ViewModels = maintenancePartRepository.GetAll().ToList();

                // wait for other tasks to complete
                while (this.TaskManager.TaskCount() > 1)
                {
                    Task.Delay(500);
                }

            },
            TaskCreationOptions.LongRunning),
            Messages.AllMaintenancePartLoading,
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
             "loading maintenance part",
             Messages.AllMaintenancePartLoadError,
             log);

        }

        /// <summary>
        /// Add each ViewModel to the collection
        /// </summary>
        /// <param name="maintenancePart">list of models</param>
        private void Set(List<MaintenancePart> maintenancePart)
        {

            // clear current list
            this.ViewModels.ClearAll();

            // loop through each model and add e ViewModel to the collection
            foreach (var model in maintenancePart)
            {
                MaintenancePartViewModel viewModel = new MaintenancePartViewModel(model, this.maintenancePartRepository, this.TaskManager, false,
                    this.MaintenanceRecordDetails, this.Parts);
                this.ViewModels.AddItem(viewModel);
            }

        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void LoadMaintenanceRecordDetails()
        {

            List<MaintenanceRecordDetail> models = null;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                models = maintenanceRecordDetailRepository.GetAll().ToList();

            },
            TaskCreationOptions.LongRunning),
            Messages.AllMaintenanceRecordDetailLoading,
            () =>
            {

                if (models == null)
                {
                    return;
                }

                // set models
                this.Set(models);

                // refresh the list
                this.MaintenanceRecordDetails.Refresh();

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "loading maintenance record details",
             Messages.AllMaintenanceRecordDetailLoadError,
             log);

        }

        /// <summary>
        /// Add each Model to the collection
        /// </summary>
        /// <param name="maintenanceRecordDetails">list of models</param>
        private void Set(List<MaintenanceRecordDetail> maintenanceRecordDetails)
        {

            // clear current list
            this.MaintenanceRecordDetails.ClearAll();

            // loop through each model and add to the collection
            foreach (var model in maintenanceRecordDetails)
            {
                this.MaintenanceRecordDetails.AddItem(model);
            }

        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void LoadParts()
        {

            List<Part> models = null;

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
            MaintenancePart model = new MaintenancePart(0, 0, 0, 0);

            // create new ViewModel
            MaintenancePartViewModel viewModel = new MaintenancePartViewModel(model, this.maintenancePartRepository, this.TaskManager, true,
                this.MaintenanceRecordDetails, this.Parts);

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
        private void SendViewModel(MaintenancePartViewModel viewModel)
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
            MaintenancePartViewModel viewModel = this.SelectedViewModel;

            // Ask viewModel to confirm the deletion
            string msg = string.Format(Messages.DeleteMessage, "maintenance part", viewModel.Model.ToString());
            MessageDialogResult result = await dialog.Dialog.ShowMessageAsync(dialog.ViewModel, Messages.TitleApp, msg,
                    MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Negative)
            {
                return;
            }

            // start task to remove viewModel
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                this.maintenancePartRepository.Delete(viewModel.Model);

            },
            TaskCreationOptions.LongRunning),
            Messages.AllMaintenancePartDeleting,
            () =>
            {

                this.Load();

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "deleting maintenance part",
            string.Format(Messages.AllMaintenancePartDeleteError, viewModel.DisplayName),
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
                "MaintenancePartID", "Quantity", "DetailID", "PartID");

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
        /// Maintenance Part repository
        /// </summary>
        private IMaintenancePartRepository maintenancePartRepository;

        /// <summary>
        /// The maintenance record detail repository
        /// </summary>
        private IMaintenanceRecordDetailRepository maintenanceRecordDetailRepository;

        /// <summary>
        /// The part repository
        /// </summary>
        private IPartRepository partRepository;

        /// <summary>
        /// Dialog service for showing messages from the ViewModel
        /// </summary>
        private IDialogService dialog;

        /// <summary>
        /// Selected ViewModel
        /// </summary>
        public MaintenancePartViewModel SelectedViewModel { get; set; }

        /// <summary>
        /// Collection of ViewModels
        /// </summary>
        public ObservableCollectionExtended<MaintenancePartViewModel> ViewModels { get; set; }

        /// <summary>
        /// Gets or sets the maintenance record details.
        /// </summary>
        /// <value>
        /// The maintenance record details.
        /// </value>
        public ObservableCollectionExtended<MaintenanceRecordDetail> MaintenanceRecordDetails { get; set; }

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
