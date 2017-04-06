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
    /// This class will handle the maintenance of the <see cref="Part"/> model
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IChanges" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class PartViewModel : WorkspaceViewModel, IDataErrorInfo, IChanges, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PartViewModel"/> class.
        /// </summary>
        /// <param name="model">model for the part</param>
        /// <param name="partRepository">repository for database operations</param>
        /// <param name="taskManager">task manager to hold reference to running tasks</param>
        /// <param name="isNew">flag indicating if this is a new part</param>
        public PartViewModel(Part model, IPartRepository partRepository, ITaskManager taskManager, bool isNew)
        {
            this.Model = model;
            this.partRepository = partRepository;
            this.TaskManager = taskManager;
            this.IsNew = isNew;
            this.IsSelected = true;
            this.HasChanges = false;
            this.ContentId = model.PartID.GetContentId(this.DisplayName);
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
                    this.partRepository.Insert(this.Model);
                }
                else
                {
                    this.partRepository.Update(this.Model);
                }

            },
            TaskCreationOptions.LongRunning),
            Messages.PartSaving,
            () =>
            {

                // request to remove from parent workspace
                this.CloseCommand.Execute(this);

            },
            Messages.MainWindowInitialStatus,
            UIContext.Current,
            "Saving part",
            string.Format(Messages.PartSaveError, this.Model.ToString()),
            log,
            () =>
            {
                // send load notification to the all part view model
                this.MessengerInstance.Send<NotificationMessage<AllPartViewModel>>(
                    new NotificationMessage<AllPartViewModel>(null, null));

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
        /// part model
        /// </summary>
        public Part Model;

        /// <summary>
        /// part repository
        /// </summary>
        private IPartRepository partRepository;

        /// <summary>
        /// Gets or sets the part description.
        /// </summary>
        /// <value>
        /// The part description.
        /// </value>
        public string PartDescription
        {
            get
            {
                return Model.PartDescription;
            }
            set
            {

                if (Model.PartDescription == value)
                {
                    return;
                }

                Model.PartDescription = value;

                base.OnPropertyChanged("PartDescription");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the part number.
        /// </summary>
        /// <value>
        /// The part number.
        /// </value>
        public long PartNumber
        {
            get
            {
                return Model.PartNumber;
            }
            set
            {

                if (Model.PartNumber == value)
                {
                    return;
                }

                Model.PartNumber = value;

                base.OnPropertyChanged("PartNumber");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the part price.
        /// </summary>
        /// <value>
        /// The part price.
        /// </value>
        public double PartPrice
        {
            get
            {
                return Model.PartPrice;
            }
            set
            {

                if (Model.PartPrice == value)
                {
                    return;
                }

                Model.PartPrice = value;

                base.OnPropertyChanged("PartPrice");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the part weight.
        /// </summary>
        /// <value>
        /// The part weight.
        /// </value>
        public double PartWeight
        {
            get
            {
                return Model.PartWeight;
            }
            set
            {

                if (Model.PartWeight == value)
                {
                    return;
                }

                Model.PartWeight = value;

                base.OnPropertyChanged("PartWeight");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets or sets the quantity in stock.
        /// </summary>
        /// <value>
        /// The quantity in stock.
        /// </value>
        public int QuantityInStock
        {
            get
            {
                return Model.QuantityInStock;
            }
            set
            {

                if (Model.QuantityInStock == value)
                {
                    return;
                }

                Model.QuantityInStock = value;

                base.OnPropertyChanged("QuantityInStock");
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
                string msg = string.Format(Messages.PartDisplayName, this.IsNew ? "New" : displayName);
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
                        param => this.Model != null ? this.Model.IsValid && this.HasChanges : false);
                }

                return _commandSave;
            }
        }

        #endregion

    }
}
