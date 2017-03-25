using CS499.TCMS.DataAccess.IRepositories;
using CS499.TCMS.Model;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Resources;
using CS499.TCMS.View.Services;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
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
        /// Default constructor
        /// </summary>
        /// <param name="model">model for the payroll</param>
        /// <param name="payrollRepository">repository for database operations</param>
        /// <param name="taskManager">task manager to hold reference to running tasks</param>
        /// <param name="isNew">flag indicating if this is a new payroll</param>
        public PayrollViewModel(Payroll model, IPayrollRepository payrollRepository, ITaskManager taskManager, bool isNew)
        {
            this.Model = model;
            this.payrollRepository = payrollRepository;
            this.TaskManager = TaskManager;
            this.IsNew = isNew;
            this.IsSelected = true;
            this.HasChanges = false;
            this.ContentId = model.EmployeeID.GetContentId(this.DisplayName);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Save viewmodel
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
            () => { },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "Saving payroll",
            string.Format(Messages.PayrollSaveError, this.Model.ToString()),
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

        /// <summary>
        /// Execute close command
        /// </summary>
        private void Back()
        {
            // request to remove from parent workspace
            this.CloseCommand.Execute(this);
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
        /// Flag indicating this viewmodel is new
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
        /// Display tooltip
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
        /// Flag indicating this viewmodel is selected in the UI
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
        /// Command to execute the save viewmodel
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


        private ICommand _commandBack;

        /// <summary>
        /// Go back to the previous tab
        /// </summary>
        public ICommand CommandBack
        {
            get
            {

                if (_commandBack == null)
                {
                    _commandBack = new RelayCommand(param =>
                    {
                        this.Back();
                    },
                       param => !this.HasChanges);
                }

                return _commandBack;
            }
        }

        #endregion

    }
}
