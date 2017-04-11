﻿using CS499.TCMS.DataAccess.IRepositories;
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
    /// This class will handle the maintenance of the <see cref="MaintenanceRecordViewModel"/>
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class AllMaintenanceRecordViewModel : WorkspaceViewModel, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AllMaintenanceRecordViewModel"/> class.
        /// </summary>
        /// <param name="dialog">Dialog service to show messages from ViewModel</param>
        /// <param name="taskManager">Task manager to hold reference to running tasks</param>
        /// <param name="maintenanceRecordRepository">The maintenanceRecord repository.</param>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        public AllMaintenanceRecordViewModel(IDialogService dialog, ITaskManager taskManager,
            IMaintenanceRecordRepository maintenanceRecordRepository, IVehicleRepository vehicleRepository)
        {
            this.dialog = dialog;
            this.TaskManager = taskManager;
            this.IsSelected = true;
            this.ViewModels = new ObservableCollectionExtended<MaintenanceRecordViewModel>(new List<MaintenanceRecordViewModel>());
            this.Vehicles = new ObservableCollectionExtended<Vehicle>(new List<Vehicle>());
            this.ViewModels.PageSize = 10;
            this.Vehicles.PageSize = 100000;
            this.DisplayName = Messages.AllMaintenanceRecordDisplayName;
            this.DisplayToolTip = Messages.AllMaintenanceRecordDisplayToolTip;
            this.maintenanceRecordRepository = maintenanceRecordRepository;
            this.vehicleRepository = vehicleRepository;
            this.LoadVehicles();
            this.Load();
            this.MessengerInstance.Register<NotificationMessage<AllMaintenanceRecordViewModel>>(this, (n) => this.Load(n));
            this.MessengerInstance.Register<NotificationMessage<AllVehicleViewModel>>(this, (n) => this.LoadVehicles(n));
            this.SearchType = "contains";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void Load(NotificationMessage<AllMaintenanceRecordViewModel> notificationMessage)
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
        /// Load list of ViewModels
        /// </summary>
        private void Load()
        {

            List<MaintenanceRecord> ViewModels = null;

            // set loading flag
            this.loading = true;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                ViewModels = maintenanceRecordRepository.GetAll().ToList();

                // wait for other tasks to complete
                while (this.loadingVehicles)
                {
                    Task.Delay(500);
                }

            },
            TaskCreationOptions.LongRunning),
            Messages.AllMaintenanceRecordLoading,
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
             "loading maintenance records",
             Messages.AllMaintenanceRecordLoadError,
             log);

        }

        /// <summary>
        /// Add each ViewModel to the collection
        /// </summary>
        /// <param name="maintenanceRecords">list of models</param>
        private void Set(List<MaintenanceRecord> maintenanceRecords)
        {

            // clear current list
            this.ViewModels.ClearAll();

            // loop through each model and add e ViewModel to the collection
            foreach (var model in maintenanceRecords)
            {
                MaintenanceRecordViewModel viewModel = new MaintenanceRecordViewModel(model, this.maintenanceRecordRepository, this.TaskManager, false,
                    this.Vehicles);
                this.ViewModels.AddItem(viewModel);
            }

        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void LoadVehicles()
        {

            List<Vehicle> models = null;

            // set loading flag
            this.loadingVehicles = true;

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

                // set loading flag
                this.loadingVehicles = false;

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
        /// <param name="vehicles">list of models</param>
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
        /// Create new instance of the ViewModel
        /// </summary>
        private void New()
        {

            // create new model
            MaintenanceRecord model = new MaintenanceRecord(0, 0, DateTime.Now, string.Empty);

            // create new ViewModel
            MaintenanceRecordViewModel viewModel = new MaintenanceRecordViewModel(model, this.maintenanceRecordRepository, this.TaskManager, true,
                this.Vehicles);

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
        private void SendViewModel(MaintenanceRecordViewModel viewModel)
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
            MaintenanceRecordViewModel viewModel = this.SelectedViewModel;

            // Ask viewModel to confirm the deletion
            string msg = string.Format(Messages.DeleteMessage, "maintenance record", viewModel.Model.ToString());
            MessageDialogResult result = await dialog.Dialog.ShowMessageAsync(dialog.ViewModel, Messages.TitleApp, msg,
                    MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Negative)
            {
                return;
            }

            // start task to remove viewModel
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                this.maintenanceRecordRepository.Delete(viewModel.Model);

            },
            TaskCreationOptions.LongRunning),
            Messages.AllMaintenanceRecordDeleting,
            () =>
            {

                this.Load();

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "deleting maintenance record",
            string.Format(Messages.AllMaintenanceRecordDeleteError, viewModel.DisplayName),
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
        /// Search for the maintenance record based on the search text
        /// </summary>
        private void SearchForText()
        {

            this.SelectedViewModel = this.ViewModels.Search(this.SearchType, this.SearchText,
                "MaintenanceID", "SelectedVehicle", "MaintenanceDate", "MaintenanceDescription");

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
        /// Send message to open AllMaintenanceRecordDetailViewModel and apply filter
        /// </summary>
        private void OpenLink()
        {
            AllMaintenanceRecordDetailViewModel viewModel = null;

            // start task to create ViewModel
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                // create ViewModel
                viewModel = WorkspaceFactory.Create<AllMaintenanceRecordDetailViewModel>(this.dialog, this.TaskManager,
                    Factory.Create<IMaintenanceRecordDetailRepository>(),
                    Factory.Create<IMaintenanceRecordRepository>(), 
                    Factory.Create<IUserRepository>());

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
                    new Filter("MaintenanceID", this.SelectedViewModel.MaintenanceID.ToString(), viewModel));

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
        /// Maintenance Record repository
        /// </summary>
        private IMaintenanceRecordRepository maintenanceRecordRepository;

        /// <summary>
        /// The vehicle repository
        /// </summary>
        private IVehicleRepository vehicleRepository;

        /// <summary>
        /// Dialog service for showing messages from the ViewModel
        /// </summary>
        private IDialogService dialog;

        /// <summary>
        /// Flag indicating the maintenance records are loading
        /// </summary>
        private bool loading = false;

        /// <summary>
        /// Flag indicating the vehicles are loading
        /// </summary>
        private bool loadingVehicles = false;

        /// <summary>
        /// Flag indicating the ViewModel is still loading data
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return this.loadingVehicles || this.loading;
            }
        }

        /// <summary>
        /// Selected ViewModel
        /// </summary>
        public MaintenanceRecordViewModel SelectedViewModel { get; set; }

        /// <summary>
        /// Collection of ViewModels
        /// </summary>
        public ObservableCollectionExtended<MaintenanceRecordViewModel> ViewModels { get; set; }

        /// <summary>
        /// Gets or sets the vehicles.
        /// </summary>
        /// <value>
        /// The vehicles.
        /// </value>
        public ObservableCollectionExtended<Vehicle> Vehicles { get; set; }
              
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
