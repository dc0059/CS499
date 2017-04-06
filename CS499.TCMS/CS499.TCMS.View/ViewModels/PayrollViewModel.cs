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
    /// This class will handle the maintenance of the payroll model
    /// </summary>
    public class PayrollViewModel : WorkspaceViewModel, IDataErrorInfo, IChanges, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PayrollViewModel"/> class.
        /// </summary>
        /// <param name="model">model for the payroll</param>
        /// <param name="payrollRepository">repository for database operations</param>
        /// <param name="taskManager">task manager to hold reference to running tasks</param>
        /// <param name="isNew">flag indicating if this is a new payroll</param>
        /// <param name="users">collection of all users</param>
        public PayrollViewModel(Payroll model, IPayrollRepository payrollRepository, ITaskManager taskManager, bool isNew,
            ObservableCollectionExtended<User> users)
        {
            this.Model = model;
            this.payrollRepository = payrollRepository;
            this.TaskManager = taskManager;
            this.IsNew = isNew;
            this.IsSelected = true;
            this.HasChanges = false;
            this.ContentId = model.EmployeeID.GetContentId(this.DisplayName);
            this.Users = users;
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
                this.SelectedUser = this.Users.UnfilteredList.FirstOrDefault();
            }
            else
            {
                this.SelectedUser = this.Users.UnfilteredList
                    .FirstOrDefault((u) => u.EmployeeID.Equals(this.EmployeeID));
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
                    this.payrollRepository.Insert(this.Model);
                }
                else
                {
                    this.payrollRepository.Update(this.Model);
                }

            },
            TaskCreationOptions.LongRunning),
            Messages.PayrollSaving,
            () =>
            {

                // request to remove from parent workspace
                this.CloseCommand.Execute(this);

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "Saving payroll",
            Messages.PayrollSaveError,
            log,
            () =>
            {
                // send load notification to the all payroll view model
                this.MessengerInstance.Send<NotificationMessage<AllPayrollViewModel>>(
                    new NotificationMessage<AllPayrollViewModel>(null, null));

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
        /// payroll model
        /// </summary>
        public Payroll Model;

        /// <summary>
        /// payroll repository
        /// </summary>
        private IPayrollRepository payrollRepository;

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public ObservableCollectionExtended<User> Users { get; set; }

        private User _selectedUser;

        /// <summary>
        /// Gets or sets the selected user.
        /// </summary>
        /// <value>
        /// The selected user.
        /// </value>
        public User SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {

                if (_selectedUser == value)
                {
                    return;
                }

                _selectedUser = value;

                if (_selectedUser != null)
                {
                    this.EmployeeID = _selectedUser.EmployeeID;
                }

                base.OnPropertyChanged("SelectedUser");

            }
        }

        /// <summary>
        /// <see cref="Payroll"/>
        /// </summary>
        public long EmployeeID
        {
            get
            {
                return Model.EmployeeID;
            }
            set
            {

                if (Model.EmployeeID == value)
                {
                    return;
                }

                Model.EmployeeID = value;

                base.OnPropertyChanged("EmployeeID");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="Payroll"/>
        /// </summary>
        public DateTime PaymentDate
        {
            get
            {
                return Model.PaymentDate;
            }
            set
            {

                if (Model.PaymentDate == value)
                {
                    return;
                }

                Model.PaymentDate = value;

                base.OnPropertyChanged("PaymentDate");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="Payroll"/>
        /// </summary>
        public double Payment
        {
            get
            {
                return Model.Payment;
            }
            set
            {

                if (Model.Payment == value)
                {
                    return;
                }

                Model.Payment = value;

                base.OnPropertyChanged("Payment");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="Payroll"/>
        /// </summary>
        public double HoursWorked
        {
            get
            {
                return Model.HoursWorked;
            }
            set
            {

                if (Model.HoursWorked == value)
                {
                    return;
                }

                Model.HoursWorked = value;

                base.OnPropertyChanged("HoursWorked");
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
                string msg = string.Format(Messages.PayrollDisplayName, this.IsNew ? "New" : displayName);
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
                        param => this.Model != null ? this.Model.IsValid : false);
                }

                return _commandSave;
            }
        }

        #endregion

    }
}
