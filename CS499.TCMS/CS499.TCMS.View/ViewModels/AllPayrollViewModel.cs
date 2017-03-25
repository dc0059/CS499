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
    /// This class will handle the maintenance of the payroll view model
    /// </summary>
    public class AllPayrollViewModel : WorkspaceViewModel, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dialog">Dialog service to show messages from viewmodel</param>
        /// <param name="taskManager">Task manager to hold reference to running tasks</param>
        /// <param name="payrollRepository">payroll repository</param>
        public AllPayrollViewModel(IDialogService dialog, ITaskManager taskManager, IPayrollRepository payrollRepository)
        {
            this.dialog = dialog;
            this.TaskManager = taskManager;
            this.IsSelected = true;
            this.ViewModels = new ObservableCollectionExtended<PayrollViewModel>(new List<PayrollViewModel>());
            this.ViewModels.PageSize = 10;
            this.DisplayName = Messages.AllPayrollDisplayName;
            this.DisplayToolTip = Messages.AllPayrollDisplayToolTip;
            this.payrollRepository = payrollRepository;
            this.Load();
            this.MessengerInstance.Register<NotificationMessage<AllPayrollViewModel>>(this, (n) => this.Load(n));
            this.SearchType = "contains";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load list of viewmodels
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void Load(NotificationMessage<AllPayrollViewModel> notificationMessage)
        {
            this.Load();
        }

        /// <summary>
        /// Load list of viewmodels
        /// </summary>
        private void Load()
        {

            List<Payroll> ViewModels = null;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                ViewModels = payrollRepository.GetAll().ToList();

            },
            TaskCreationOptions.LongRunning),
            Messages.AllPayrollLoading,
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
             "loading payrolls",
             Messages.AllPayrollLoadError,
             log);

        }

        /// <summary>
        /// Add each viewmmodel to the collection
        /// </summary>
        /// <param name="payrolls">list of models</param>
        private void Set(List<Payroll> payrolls)
        {

            // clear current list
            this.ViewModels.ClearAll();

            // loop through each model and add e viewmodel to the collection
            foreach (var model in payrolls)
            {
                PayrollViewModel viewModel = new PayrollViewModel(model, this.payrollRepository, this.TaskManager, false);
                this.ViewModels.AddItem(viewModel);
            }

        }

        /// <summary>
        /// Create new instance of the viewmodel
        /// </summary>
        private void New()
        {

            // create new model
            Payroll model = new Payroll(0, 0, DateTime.Now, 0, 80);

            // create new viewmodel
            PayrollViewModel viewModel = new PayrollViewModel(model, this.payrollRepository, this.TaskManager, true);

            // send viewmodel
            this.SendViewModel(viewModel);

        }

        /// <summary>
        /// Edit instance of the viewmodel
        /// </summary>
        private void Edit()
        {

            // send selected view model
            this.SendViewModel(this.SelectedViewModel);

        }

        /// <summary>
        /// send message to the mainwindowviewmodel to add the viewmodel to the collection
        /// </summary>
        /// <param name="viewModel">viewmodel</param>
        private void SendViewModel(PayrollViewModel viewModel)
        {
            this.MessengerInstance.Send<NotificationMessage<WorkspaceViewModel>>(
                new NotificationMessage<WorkspaceViewModel>(viewModel, null));
        }

        /// <summary>
        /// Delete instance of the viewmodel
        /// </summary>
        private async void Delete()
        {

            // get selected viewmodel
            PayrollViewModel viewModel = this.SelectedViewModel;

            // Ask viewModel to confirm the deletion
            string msg = string.Format(Messages.DeleteMessage, "payroll", viewModel.Model.ToString());
            MessageDialogResult result = await dialog.Dialog.ShowMessageAsync(dialog.ViewModel, Messages.TitleApp, msg,
                    MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Negative)
            {
                return;
            }

            // start task to remove viewModel
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                this.payrollRepository.Delete(viewModel.Model);

            },
            TaskCreationOptions.LongRunning),
            Messages.AllPayrollDeleting,
            () =>
            {

                this.Load();

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "deleting payroll",
            string.Format(Messages.AllPayrollDeleteError, viewModel.DisplayName),
            log);

        }

        /// <summary>
        /// Request the viewmodel be closed
        /// </summary>
        public override void OnRequestClose()
        {

            // unregister viewmodel
            this.MessengerInstance.Unregister(this);
            base.OnRequestClose();
        }

        /// <summary>
        /// Search for the payroll based on the search text
        /// </summary>
        private void SearchForText()
        {

            this.SelectedViewModel = this.ViewModels.Search(this.SearchType, this.SearchText,
                "EmployeeID", "PaymentDate", "Payment", "HoursWorked");

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
        /// Payroll repository
        /// </summary>
        private IPayrollRepository payrollRepository;

        /// <summary>
        /// Dialog service for showing messages from the viewmodel
        /// </summary>
        private IDialogService dialog;

        /// <summary>
        /// Selected viewmodel
        /// </summary>
        public PayrollViewModel SelectedViewModel { get; set; }

        /// <summary>
        /// Collection of viewmodels
        /// </summary>
        public ObservableCollectionExtended<PayrollViewModel> ViewModels { get; set; }

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
        /// Flag indicating if the selected viewmodel can be edited or deleted
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
        /// Command to execute viewmodel creation
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
        /// Command to execute viewmodel edit
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
        /// Command to execute viewmodel delete
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
        /// Command to search the viewmodel collection for the search text
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
