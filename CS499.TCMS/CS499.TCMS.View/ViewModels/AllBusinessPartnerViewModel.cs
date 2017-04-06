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
    /// This class will handle the maintenance of the business partner view model
    /// </summary>
    public class AllBusinessPartnerViewModel : WorkspaceViewModel, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dialog">Dialog service to show messages from ViewModel</param>
        /// <param name="taskManager">Task manager to hold reference to running tasks</param>
        /// <param name="businessPartnerRepository">business partner repository</param>
        public AllBusinessPartnerViewModel(IDialogService dialog, ITaskManager taskManager, IBusinessPartnerRepository businessPartnerRepository)
        {
            this.dialog = dialog;
            this.TaskManager = taskManager;
            this.IsSelected = true;
            this.ViewModels = new ObservableCollectionExtended<BusinessPartnerViewModel>(new List<BusinessPartnerViewModel>());
            this.ViewModels.PageSize = 10;
            this.DisplayName = Messages.AllBusinessPartnerDisplayName;
            this.DisplayToolTip = Messages.AllBusinessPartnerDisplayToolTip;
            this.businessPartnerRepository = businessPartnerRepository;
            this.Load();
            this.MessengerInstance.Register<NotificationMessage<AllBusinessPartnerViewModel>>(this, (n) => this.Load(n));
            this.SearchType = "contains";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void Load(NotificationMessage<AllBusinessPartnerViewModel> notificationMessage)
        {
            this.Load();
        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void Load()
        {

            List<BusinessPartner> ViewModels = null;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                ViewModels = businessPartnerRepository.GetAll().ToList();

            },
            TaskCreationOptions.LongRunning),
            Messages.AllBusinessPartnerLoading,
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
             "loading business partners",
             Messages.AllBusinessPartnerLoadError,
             log);

        }

        /// <summary>
        /// Add each viewmmodel to the collection
        /// </summary>
        /// <param name="businessPartners">list of models</param>
        private void Set(List<BusinessPartner> businessPartners)
        {

            // clear current list
            this.ViewModels.ClearAll();

            // loop through each model and add e ViewModel to the collection
            foreach (var model in businessPartners)
            {
                BusinessPartnerViewModel viewModel = new BusinessPartnerViewModel(model, this.businessPartnerRepository, this.TaskManager, false);
                this.ViewModels.AddItem(viewModel);
            }

        }

        /// <summary>
        /// Create new instance of the ViewModel
        /// </summary>
        private void New()
        {

            // create new model
            BusinessPartner model = new BusinessPartner(0, string.Empty, string.Empty, 
                string.Empty, string.Empty, 0, string.Empty);

            // create new ViewModel
            BusinessPartnerViewModel viewModel = new BusinessPartnerViewModel(model, this.businessPartnerRepository, this.TaskManager, true);

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
        private void SendViewModel(BusinessPartnerViewModel viewModel)
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
            BusinessPartnerViewModel viewModel = this.SelectedViewModel;

            // Ask viewModel to confirm the deletion
            string msg = string.Format(Messages.DeleteMessage, "business partner", viewModel.Model.ToString());
            MessageDialogResult result = await dialog.Dialog.ShowMessageAsync(dialog.ViewModel, Messages.TitleApp, msg,
                    MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Negative)
            {
                return;
            }

            // start task to remove viewModel
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                this.businessPartnerRepository.Delete(viewModel.Model);

            },
            TaskCreationOptions.LongRunning),
            Messages.AllBusinessPartnerDeleting,
            () =>
            {

                this.Load();

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "deleting business partner",
            string.Format(Messages.AllBusinessPartnerDeleteError, viewModel.DisplayName),
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
        /// Search for the business partner based on the search text
        /// </summary>
        private void SearchForText()
        {

            this.SelectedViewModel = this.ViewModels.Search(this.SearchType, this.SearchText,
                "CompanyName", "Address", "City", "State", "ZipCode", "PhoneNumber");

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
        /// Business Partner repository
        /// </summary>
        private IBusinessPartnerRepository businessPartnerRepository;

        /// <summary>
        /// Dialog service for showing messages from the ViewModel
        /// </summary>
        private IDialogService dialog;

        /// <summary>
        /// Selected ViewModel
        /// </summary>
        public BusinessPartnerViewModel SelectedViewModel { get; set; }

        /// <summary>
        /// Collection of ViewModels
        /// </summary>
        public ObservableCollectionExtended<BusinessPartnerViewModel> ViewModels { get; set; }

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
