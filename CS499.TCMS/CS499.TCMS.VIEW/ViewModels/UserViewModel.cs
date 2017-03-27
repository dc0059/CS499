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
    /// This class will handle the maintenance of the <see cref="User"/> model
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IChanges" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class UserViewModel : WorkspaceViewModel, IDataErrorInfo, IChanges, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewModel"/> class.
        /// </summary>
        /// <param name="model">model for the user</param>
        /// <param name="userRepository">repository for database operations</param>
        /// <param name="taskManager">task manager to hold reference to running tasks</param>
        /// <param name="isNew">flag indicating if this is a new user</param>
        public UserViewModel(User model, IUserRepository userRepository, ITaskManager taskManager, bool isNew)
        {
            this.Model = model;
            this.userRepository = userRepository;
            this.TaskManager = taskManager;
            this.IsNew = isNew;
            this.IsSelected = true;
            this.HasChanges = false;
            this.ContentId = model.EmployeeID.GetContentId(this.DisplayName);
        }

        #endregion

        #region Methods

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
                    this.userRepository.Insert(this.Model);
                }
                else
                {
                    this.userRepository.Update(this.Model);
                }

            },
            TaskCreationOptions.LongRunning),
            Messages.UserSaving,
            () => {},
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "Saving user",
            string.Format(Messages.UserSaveError, this.Model.ToString()),
            log,
            () =>
            {
                // send load notification to the all user view model
                this.MessengerInstance.Send<NotificationMessage<AllUserViewModel>>(
                    new NotificationMessage<AllUserViewModel>(null, null));

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
        /// user model
        /// </summary>
        public User Model;

        /// <summary>
        /// user repository
        /// </summary>
        private IUserRepository userRepository;

        /// <summary>
        /// Gets the access levels.
        /// </summary>
        /// <value>
        /// The access levels.
        /// </value>
        public string[] AccessLevels
        {
            get
            {
                return Enums.GetHumanizedValues<Enums.AccessLevel>();
            }
        }

        /// <summary>
        /// <see cref="User"/>
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
        /// <see cref="User"/>
        /// </summary>
        public string UserName
        {
            get
            {
                return Model.UserName;
            }
            set
            {

                if (Model.UserName == value)
                {
                    return;
                }

                Model.UserName = value;

                base.OnPropertyChanged("UserName");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public string FirstName
        {
            get
            {
                return Model.FirstName;
            }
            set
            {

                if (Model.FirstName == value)
                {
                    return;
                }

                Model.FirstName = value;

                base.OnPropertyChanged("FirstName");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public string MiddleName
        {
            get
            {
                return Model.MiddleName;
            }
            set
            {

                if (Model.MiddleName == value)
                {
                    return;
                }

                Model.MiddleName = value;

                base.OnPropertyChanged("MiddleName");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public string LastName
        {
            get
            {
                return Model.LastName;
            }
            set
            {

                if (Model.LastName == value)
                {
                    return;
                }

                Model.LastName = value;

                base.OnPropertyChanged("LastName");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public string Address
        {
            get
            {
                return Model.Address;
            }
            set
            {

                if (Model.Address == value)
                {
                    return;
                }

                Model.Address = value;

                base.OnPropertyChanged("Address");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public string City
        {
            get
            {
                return Model.City;
            }
            set
            {

                if (Model.City == value)
                {
                    return;
                }

                Model.City = value;

                base.OnPropertyChanged("City");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public string State
        {
            get
            {
                return Model.State;
            }
            set
            {

                if (Model.State == value)
                {
                    return;
                }

                Model.State = value;

                base.OnPropertyChanged("State");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public int ZipCode
        {
            get
            {
                return Model.ZipCode;
            }
            set
            {

                if (Model.ZipCode == value)
                {
                    return;
                }

                Model.ZipCode = value;

                base.OnPropertyChanged("ZipCode");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public string HomePhone
        {
            get
            {
                return Model.HomePhone;
            }
            set
            {

                if (Model.HomePhone == value)
                {
                    return;
                }

                Model.HomePhone = value;

                base.OnPropertyChanged("HomePhone");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public string CellPhone
        {
            get
            {
                return Model.CellPhone;
            }
            set
            {

                if (Model.CellPhone == value)
                {
                    return;
                }

                Model.CellPhone = value;

                base.OnPropertyChanged("CellPhone");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public string EmailAddress
        {
            get
            {
                return Model.EmailAddress;
            }
            set
            {

                if (Model.EmailAddress == value)
                {
                    return;
                }

                Model.EmailAddress = value;

                base.OnPropertyChanged("EmailAddress");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public double PayRate
        {
            get
            {
                return Model.PayRate;
            }
            set
            {

                if (Model.PayRate == value)
                {
                    return;
                }

                Model.PayRate = value;

                base.OnPropertyChanged("PayRate");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public DateTime EmploymentDate
        {
            get
            {
                return Model.EmploymentDate;
            }
            set
            {

                if (Model.EmploymentDate == value)
                {
                    return;
                }

                Model.EmploymentDate = value;

                base.OnPropertyChanged("EmploymentDate");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public Enums.AccessLevel AccessLevel
        {
            get
            {
                return Model.AccessLevel;
            }
            set
            {

                if (Model.AccessLevel == value)
                {
                    return;
                }

                Model.AccessLevel = value;

                base.OnPropertyChanged("AccessLevel");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public string HomeStore
        {
            get
            {
                return Model.HomeStore;
            }
            set
            {

                if (Model.HomeStore == value)
                {
                    return;
                }

                Model.HomeStore = value;

                base.OnPropertyChanged("HomeStore");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public string JobDescription
        {
            get
            {
                return Model.JobDescription;
            }
            set
            {

                if (Model.JobDescription == value)
                {
                    return;
                }

                Model.JobDescription = value;

                base.OnPropertyChanged("JobDescription");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public bool IsActive
        {
            get
            {
                return Model.IsActive;
            }
            set
            {

                if (Model.IsActive == value)
                {
                    return;
                }

                Model.IsActive = value;

                base.OnPropertyChanged("IsActive");
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
                string msg = string.Format(Messages.UserDisplayName, this.IsNew ? "New" : displayName);
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
