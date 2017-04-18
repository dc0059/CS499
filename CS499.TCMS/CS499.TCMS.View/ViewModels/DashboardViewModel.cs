using CS499.TCMS.DataAccess.IRepositories;
using CS499.TCMS.Model;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Resources;
using CS499.TCMS.View.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CS499.TCMS.View.ViewModels
{
    /// <summary>
    /// This class will handle showing all the user relevant information
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IChanges" />
    public class DashboardViewModel : WorkspaceViewModel, IChanges
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardViewModel"/> class.
        /// </summary>
        /// <param name="user">The user model.</param>
        /// <param name="dialog">Dialog service to show messages from ViewModel</param>
        /// <param name="taskManager">Task manager to hold reference to running tasks</param>
        public DashboardViewModel(User user, ITaskManager taskManager, IDialogService dialog)
        {
            this.dialog = dialog;
            this.TaskManager = taskManager;
            this.user = user;
            this.IsSelected = true;
            this.ViewModels = new List<WorkspaceViewModel>();
            this.DisplayName = Messages.DashboardDisplayName;
            this.CreateWorkspaces();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the workspaces.
        /// </summary>
        private void CreateWorkspaces()
        {

            // start new task to create workspaces
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                // create my info ViewModel
                this.ViewModels.Add(WorkspaceFactory.Create<MyInfoViewModel>(this.user, 
                    Factory.Create<IUserRepository>(), this.TaskManager, this.dialog));

                // create workspaces for the driver
                if (this.user.AccessLevel == Enums.AccessLevel.Driver_Data)
                {

                    // create my manifest ViewModel
                    this.ViewModels.Add(WorkspaceFactory.Create<MyManifestViewModel>(this.dialog, this.TaskManager,
                        Factory.Create<IManifestRepository>(), this.user));

                }


            },
            TaskCreationOptions.LongRunning),
            Messages.LoginLoading,
            () =>
            {

                // set my info as selected
                this.SetSelectedWorkspace(this.ViewModels.First().DisplayName);

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "loading workspaces",
             string.Empty,
             log);

        }
               
        /// <summary>
        /// Sets the selected workspace.
        /// </summary>
        /// <param name="myInfoDisplayName">Display name of my information.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void SetSelectedWorkspace(string myInfoDisplayName)
        {
            this.SelectedViewModel = this.ViewModels.FirstOrDefault((w) => w.DisplayName == myInfoDisplayName);
        }              
       
        /// Will check for any changes made to the underlying model
        /// </summary>
        /// <returns>
        /// true if there are any changes found
        /// </returns>
        bool IChanges.CheckForChanges()
        {

            // loop through each WorkspaceViewModel and check for changes
            foreach (var viewModel in this.ViewModels)
            {

                IChanges model = viewModel as IChanges;

                if (model != null)
                {

                    if (model.CheckForChanges())
                    {
                        return true;
                    }

                }

            }

            return false;

        }

        #endregion

        #region Properties

        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The user
        /// </summary>
        private User user;

        /// <summary>
        /// Dialog service for showing messages from the ViewModel
        /// </summary>
        private IDialogService dialog;

        /// <summary>
        /// Gets or sets the view models.
        /// </summary>
        /// <value>
        /// The view models.
        /// </value>
        public List<WorkspaceViewModel> ViewModels { get; set; }

        private WorkspaceViewModel _selectedViewModel;

        /// <summary>
        /// Gets or sets the selected view model.
        /// </summary>
        /// <value>
        /// The selected view model.
        /// </value>
        public WorkspaceViewModel SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }
            set
            {               
                _selectedViewModel = value;
                base.OnPropertyChanged("SelectedViewModel");

            }
        }

        /// <summary>
        /// Returns true unless overrode in the
        /// child class
        /// </summary>
        public override bool CanClose
        {
            get
            {
                return false;
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
