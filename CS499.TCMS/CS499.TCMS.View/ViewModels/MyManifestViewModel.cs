using CS499.TCMS.DataAccess.IRepositories;
using CS499.TCMS.Model;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Resources;
using CS499.TCMS.View.Services;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace CS499.TCMS.View.ViewModels
{
    /// <summary>
    /// This class will handle displaying the manifest information for the driver
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    public class MyManifestViewModel : WorkspaceViewModel
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MyManifestViewModel"/> class.
        /// </summary>
        /// <param name="dialog">Dialog service to show messages from ViewModel</param>
        /// <param name="taskManager">Task manager to hold reference to running tasks</param>
        /// <param name="manifestRepository">The manifest repository.</param>
        /// <param name="user">The user.</param>
        public MyManifestViewModel(IDialogService dialog, ITaskManager taskManager,
            IManifestRepository manifestRepository, User user)
        {
            this.IsSelected = true;
            this.DisplayName = Messages.MyManifestDisplayName;
            this.dialog = dialog;
            this.TaskManager = taskManager;
            this.manifestRepository = manifestRepository;
            this.User = user;
            this.Load();
            this.timer = new DispatcherTimer(DispatcherPriority.Background, System.Windows.Application.Current.Dispatcher);
            this.timer.Tick += this.ReloadDispatcherTimer_Tick;
            this.timer.Interval = TimeSpan.FromMinutes(3);
            this.timer.Start();
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Tick event of the ReloadDispatcherTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ReloadDispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.Load();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the manifest for the driver.
        /// </summary>
        private void Load()
        {

            DataTable manifests = null;

            // start new task to get the manifests from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                manifests = manifestRepository.GetManifestByEmployeeID(this.User.EmployeeID);

            },
            TaskCreationOptions.LongRunning),
            Messages.AllManifestLoading,
            () =>
            {

                if (manifests == null)
                {
                    return;
                }

                // set manifests
                this.Manifests = manifests;

                // notify total change
                base.OnPropertyChanged("TotalWeight");

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "loading manifests",
             Messages.AllManifestLoadError,
             log);

        }

        /// <summary>
        /// Calculates the total weight.
        /// </summary>
        /// <returns>sum of the weight</returns>
        private long CalculateTotalWeight()
        {
            long total = 0;

            if (this.Manifests != null)
            {
                total = this.Manifests.AsEnumerable().Sum(c => c.Field<long>("Total Part Weight"));
            }

            return total;

        }

        /// <summary>
        /// Sends the manifest to the <see cref="MapViewModel"/>
        /// </summary>
        private void Map()
        {
            this.MessengerInstance.Send(new NotificationMessage<DataTable>(this.Manifests, null));
        }

        /// <summary>
        /// Called when [request close].
        /// </summary>
        public override void OnRequestClose()
        {
            this.timer.Stop();
            base.OnRequestClose();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// User model
        /// </summary>
        public User User;

        /// <summary>
        /// Manifest repository
        /// </summary>
        private IManifestRepository manifestRepository;

        /// <summary>
        /// Dialog service for showing messages from the ViewModel
        /// </summary>
        private IDialogService dialog;

        private DataTable _manifests;

        /// <summary>
        /// Gets or sets the manifests.
        /// </summary>
        /// <value>
        /// The manifests.
        /// </value>
        public DataTable Manifests
        {
            get
            {
                return _manifests;
            }

            set
            {
                _manifests = value;
                base.OnPropertyChanged("Manifests");
            }
        }

        /// <summary>
        /// Gets or sets the total weight.
        /// </summary>
        /// <value>
        /// The total weight.
        /// </value>
        public long TotalWeight
        {
            get
            {
                return this.CalculateTotalWeight();
            }
        }

        private ICommand _commandLoad;

        /// <summary>
        /// Gets the command load.
        /// </summary>
        /// <value>
        /// The command load.
        /// </value>
        public ICommand CommandLoad
        {
            get
            {

                if (_commandLoad == null)
                {
                    _commandLoad = new RelayCommand(param =>
                        {
                            this.Load();
                        });
                }

                return _commandLoad;
            }
        }


        private ICommand _commandMap;

        /// <summary>
        /// Gets the command map.
        /// </summary>
        /// <value>
        /// The command map.
        /// </value>
        public ICommand CommandMap
        {
            get
            {

                if ( _commandMap == null)
                {
                     _commandMap = new RelayCommand(param =>
                        {
                            this.Map();
                        },
                        param => this.Manifests != null && this.Manifests.Rows.Count > 0);
                }

                return  _commandMap;
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
