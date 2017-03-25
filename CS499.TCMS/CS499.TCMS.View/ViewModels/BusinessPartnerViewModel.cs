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
    /// This class will handle the maintenance of the business partner model
    /// </summary>
    public class BusinessPartnerViewModel : WorkspaceViewModel, IDataErrorInfo, IChanges, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="model">model for the business partner</param>
        /// <param name="businessPartnerRepository">repository for database operations</param>
        /// <param name="taskManager">task manager to hold reference to running tasks</param>
        /// <param name="isNew">flag indicating if this is a new business partner</param>
        public BusinessPartnerViewModel(BusinessPartner model, IBusinessPartnerRepository businessPartnerRepository, ITaskManager taskManager, bool isNew)
        {
            this.Model = model;
            this.businessPartnerRepository = businessPartnerRepository;
            this.TaskManager = taskManager;
            this.IsNew = isNew;
            this.IsSelected = true;
            this.HasChanges = false;
            this.ContentId = model.CompanyID.GetContentId(this.DisplayName);
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
                    this.businessPartnerRepository.Insert(this.Model);
                }
                else
                {
                    this.businessPartnerRepository.Update(this.Model);
                }

            },
            TaskCreationOptions.LongRunning),
            Messages.BusinessPartnerSaving,
            () =>
            {

                // request to remove from parent workspace
                this.CloseCommand.Execute(this);

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "Saving business partner",
            string.Format(Messages.BusinessPartnerSaveError, this.Model.ToString()),
            log,
            () =>
            {
                // send load notification to the all business partner view model
                this.MessengerInstance.Send<NotificationMessage<AllBusinessPartnerViewModel>>(
                    new NotificationMessage<AllBusinessPartnerViewModel>(null, null));

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
        /// business partner model
        /// </summary>
        public BusinessPartner Model;

        /// <summary>
        /// business partner repository
        /// </summary>
        private IBusinessPartnerRepository businessPartnerRepository;

        /// <summary>
        /// <see cref="BusinessPartner"/>
        /// </summary>
        public string CompanyName
        {
            get
            {
                return Model.CompanyName;
            }
            set
            {

                if (Model.CompanyName == value)
                {
                    return;
                }

                Model.CompanyName = value;

                base.OnPropertyChanged("CompanyName");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="BusinessPartner"/>
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
        /// <see cref="BusinessPartner"/>
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
        /// <see cref="BusinessPartner"/>
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
        /// <see cref="BusinessPartner"/>
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
        /// <see cref="BusinessPartner"/>
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                return Model.PhoneNumber;
            }
            set
            {

                if (Model.PhoneNumber == value)
                {
                    return;
                }

                Model.PhoneNumber = value;

                base.OnPropertyChanged("PhoneNumber");
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
                string msg = string.Format(Messages.BusinessPartnerDisplayName, this.IsNew ? "New" : displayName);
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
