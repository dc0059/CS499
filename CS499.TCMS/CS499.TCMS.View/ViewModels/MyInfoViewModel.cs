using CS499.TCMS.DataAccess.IRepositories;
using CS499.TCMS.Model;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Resources;
using CS499.TCMS.View.Services;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CS499.TCMS.View.ViewModels
{
    /// <summary>
    /// This class will handle displaying <see cref="User"/> model information and changing the users password.
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IChanges" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class MyInfoViewModel : WorkspaceViewModel, IChanges, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MyInfoViewModel"/> class.
        /// </summary>
        /// <param name="model">model for the user</param>
        /// <param name="userRepository">repository for database operations</param>
        /// <param name="taskManager">task manager to hold reference to running tasks</param>
        /// <param name="dialog">Dialog service to show messages from ViewModel</param>
        public MyInfoViewModel(User model, IUserRepository userRepository, ITaskManager taskManager, IDialogService dialog)
        {
            this.Model = model;
            this.userRepository = userRepository;
            this.TaskManager = taskManager;
            this.dialog = dialog;
            this.IsNew = false;
            this.HasChanges = false;
            this.IsSelected = true;
            this.DisplayName = Messages.MyInfoDisplayName;
            this.Password = new SecureString();
            this.RetryPassword = new SecureString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Save ViewModel
        /// </summary>
        private async void Save()
        {

            // create login dialog setting
            LoginDialogSettings settings = new LoginDialogSettings()
            {
                InitialUsername = CoreAssembly.CurrentUser(),
                NegativeButtonVisibility = Visibility.Visible,
                ShouldHideUsername = true
            };

            // show login dialog
            LoginDialogData credentials = await this.dialog.Dialog.ShowLoginAsync(
                this.dialog.ViewModel, Messages.TitleApp, Messages.LoginVerify, settings);

            // exit if the user cancels
            if (credentials == null)
            {
                return;
            }

            // check password
            if (!PasswordService.ValidatePassword(credentials.Password, 
                this.Model.Passphrase, this.Model.HashKey))
            {

                // show error message and exit
                await this.dialog.Dialog.ShowMessageAsync(this.dialog.ViewModel,
                    Messages.TitleApp, string.Format(Messages.LoginFailPassword,
                    credentials.Username));
                return;
            }

            // start task to save viewModel information
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                // update the users password hash and passphrase
                string passphrase;
                string hash = PasswordService.HashPassword(this.Password.ToUnsecuredString(), out passphrase);

                // update model
                this.Model.Passphrase = passphrase;
                this.Model.HashKey = hash;

                // update current viewModel
                this.userRepository.Update(this.Model);

            },
            TaskCreationOptions.LongRunning),
            Messages.UserSaving,
            () => { },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "Saving user",
            string.Format(Messages.UserSaveError, this.Model.ToString()),
            log);

        }

        /// <summary>
        /// Checks the password.
        /// </summary>
        /// <returns>true if the password matches, false otherwise.</returns>
        private bool CheckPassword()
        {

            // check null values
            if (this.Password == null ||
                this.RetryPassword == null)
            {
                return false;
            }

            // check password against retry
            bool isCorrect = this.Password.IsEqualTo(this.RetryPassword);

            return isCorrect;

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
        /// The model
        /// </summary>
        public User Model;

        /// <summary>
        /// Dialog service for showing messages from the ViewModel
        /// </summary>
        private IDialogService dialog;

        /// <summary>
        /// The user repository
        /// </summary>
        private IUserRepository userRepository;
        
        private SecureString _password;

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {

                if (_password == value)
                {
                    return;
                }

                _password = value;
                base.OnPropertyChanged("Password");
                base.OnPropertyChanged("DontMatchError");

            }
        }

        private SecureString _retryPassword;

        /// <summary>
        /// Gets or sets the retry password.
        /// </summary>
        /// <value>
        /// The retry password.
        /// </value>
        public SecureString RetryPassword
        {
            get
            {
                return _retryPassword;
            }
            set
            {

                if (_retryPassword == value)
                {
                    return;
                }

                _retryPassword = value;
                base.OnPropertyChanged("RetryPassword");
                base.OnPropertyChanged("DontMatchError");

            }
        }
       
        /// <summary>
        /// Gets or sets the don't match error.
        /// </summary>
        /// <value>
        /// The don't match error.
        /// </value>
        public string DontMatchError
        {

            get
            {
                return this.Password.IsEqualTo(this.RetryPassword) ? null : Messages.DontMatchError;
            }
            
        }


        /// <summary>
        /// Gets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public long EmployeeID
        {
            get
            {
                return Model.EmployeeID;
            }            
        }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName
        {
            get
            {
                return Model.UserName;
            }
        }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName
        {
            get
            {
                return Model.FirstName;
            }
        }

        /// <summary>
        /// Gets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        public string MiddleName
        {
            get
            {
                return Model.MiddleName;
            }
        }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName
        {
            get
            {
                return Model.LastName;
            }
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address
        {
            get
            {
                return Model.Address;
            }
        }

        /// <summary>
        /// Gets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City
        {
            get
            {
                return Model.City;
            }            
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State
        {
            get
            {
                return Model.State;
            }
        }

        /// <summary>
        /// Gets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        public int ZipCode
        {
            get
            {
                return Model.ZipCode;
            }
        }

        /// <summary>
        /// Gets the home phone.
        /// </summary>
        /// <value>
        /// The home phone.
        /// </value>
        public string HomePhone
        {
            get
            {
                return Model.HomePhone;
            }            
        }

        /// <summary>
        /// Gets the cell phone.
        /// </summary>
        /// <value>
        /// The cell phone.
        /// </value>
        public string CellPhone
        {
            get
            {
                return Model.CellPhone;
            }            
        }

        /// <summary>
        /// Gets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public string EmailAddress
        {
            get
            {
                return Model.EmailAddress;
            }            
        }

        /// <summary>
        /// Gets the pay rate.
        /// </summary>
        /// <value>
        /// The pay rate.
        /// </value>
        public double PayRate
        {
            get
            {
                return Model.PayRate;
            }
        }

        /// <summary>
        /// Gets the employment date.
        /// </summary>
        /// <value>
        /// The employment date.
        /// </value>
        public DateTime EmploymentDate
        {
            get
            {
                return Model.EmploymentDate;
            }            
        }

        /// <summary>
        /// Gets the access level.
        /// </summary>
        /// <value>
        /// The access level.
        /// </value>
        public Enums.AccessLevel AccessLevel
        {
            get
            {
                return Model.AccessLevel;
            }            
        }

        /// <summary>
        /// Gets the home store.
        /// </summary>
        /// <value>
        /// The home store.
        /// </value>
        public string HomeStore
        {
            get
            {
                return Model.HomeStore;
            }
        }

        /// <summary>
        /// Gets the job description.
        /// </summary>
        /// <value>
        /// The job description.
        /// </value>
        public string JobDescription
        {
            get
            {
                return Model.JobDescription;
            }            
        }

        /// <summary>
        /// Gets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive
        {
            get
            {
                return Model.IsActive;
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
                        param => this.CheckPassword());
                }

                return _commandSave;
            }
        }

        /// <summary>
        /// Returns the viewModel-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return base.DisplayName;    
            }

            protected set
            {
                base.DisplayName = value;
            }
        }

        #endregion

    }
}
