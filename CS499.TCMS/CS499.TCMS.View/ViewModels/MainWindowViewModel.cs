using CS499.TCMS.DataAccess.IRepositories;
using CS499.TCMS.Model;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Resources;
using CS499.TCMS.View.Services;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace CS499.TCMS.View.ViewModels
{

    /// <summary>
    /// This class will construct the main window workspaces and layout
    /// </summary>
    public class MainWindowViewModel : WorkspaceViewModel, IKeyPressed, IKeyCommand
    {

        #region Constructor
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.InitMainWindow();
        }

        #endregion

        #region Events

        /// <summary>
        /// Called when the workspace is changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        void OnDocumentWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnDocumentWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnDocumentWorkspaceRequestClose;
        }


        /// <summary>
        /// Called when the workspace asks to be removed from the collection
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void OnDocumentWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            this.DocumentWorkspaces.Remove(workspace);
        }


        /// <summary>
        /// Called when the task status has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TaskManagerStatusEventArgs"/> instance containing the event data.</param>
        public void OnTaskStatusChanged(object sender, TaskManagerStatusEventArgs e)
        {
            this.Status = e.Status;
        }


        /// <summary>
        /// Called when the <see cref="MainWindow"/> is closing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        public async void OnClosing(object sender, CancelEventArgs e)
        {

            e.Cancel = !shutdown;

            if (shutdown)
            {
                return;
            }

            if (await this.CheckForChanges())
            {
                shutdown = false;
            }
            else
            {
                shutdown = true;
            }

            if (shutdown && !restart)
            {
                CoreAssembly.CloseApp();
            }

        }

        // <summary>
        /// Event raised when a key is presses and the App has focus
        /// </summary>
        /// <param name="sender">MainWindow</param>
        /// <param name="e">KeyEventArgs</param>
        public void OnKeyPress(object sender, KeyEventArgs e)
        {

            // open help document if the user presses the F1 button
            if (e.Key == Key.F1)
            {
                this.CreateHelp();
            }
            else if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) && e.Key == Key.R)
            {
                // restart application
                this.RestartApp();
            }

            // send key press event to the ViewModels
            this.SendKeys(e);

        }

        /// <summary>
        /// Timer callback that check for application updates
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">EventArgs</param>
        private void checkForUpdateDispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.CheckForUpdate();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add ViewModel to the workspace collection
        /// </summary>
        /// <param name="sender">ViewModel</param>
        private void AddWorkspace(NotificationMessage<WorkspaceViewModel> notification)
        {
            WorkspaceViewModel viewModel = notification.Content as WorkspaceViewModel;

            // check to make sure we are not editing the same viewModel
            if (!viewModel.IsNew)
            {

                if (this.DocumentWorkspaces
                    .FirstOrDefault(vm => vm.ContentId
                        .Equals(viewModel.ContentId, StringComparison.OrdinalIgnoreCase)) != null)
                    return;
            }

            // add viewModel as workspace to the document collection
            viewModel.IsSelected = viewModel.IsVisible = true;
            this.DocumentWorkspaces.Add(viewModel);
        }


        /// <summary>
        /// Send key stroke to the ViewModel
        /// </summary>
        /// <param name="e">The <see cref="KeyEventArgs"/></param>
        public void SendKeys(KeyEventArgs e)
        {

            foreach (var workspace in this.DocumentWorkspaces)
            {

                if (workspace.IsSelected)
                {

                    IKeyCommand command = workspace as IKeyCommand;

                    if (command != null)
                    {
                        command.SendKeys(e);
                        return;
                    }

                }

            }
        }

        /// <summary>
        /// Check to make sure time sheet does not have any unsaved changes
        /// </summary>
        /// <returns>flag indicating close cancellation</returns>
        private async Task<bool> CheckForChanges()
        {

            if (!restart)
            {

                bool changesFound = false;
                MessageDialogResult result;

                // return if there are any background tasks running
                if (TaskManager.TaskCount() > 0)
                {
                    this.Status = Messages.MainWindowBackgroundTask;
                    return true;
                }

                // loop through each open WorkspaceViewModel and check for changes
                foreach (var viewModel in this.DocumentWorkspaces)
                {

                    IChanges model = viewModel as IChanges;

                    if (model != null)
                    {

                        if (model.CheckForChanges())
                        {
                            changesFound = true;
                            break;
                        }
                    }

                }

                // if no changes were found in the documents then check the anchorables
                if (!changesFound)
                {

                    // loop through each open WorkspaceViewModel and check for changes
                    foreach (var viewModel in this.AnchorableWorkspaces)
                    {

                        IChanges model = viewModel as IChanges;

                        if (model != null)
                        {

                            if (model.CheckForChanges())
                            {
                                changesFound = true;
                                break;
                            }
                        }

                    }

                }

                // if changes were found then ask the user if they want to continue
                if (changesFound)
                {

                    result = await this.ShowChangesFoundMessage();
                    if (result == MessageDialogResult.Negative)
                    {
                        return true;
                    }
                }

            }

            return false;

        }

        /// <summary>
        /// Show the changes found message dialog
        /// </summary>
        /// <returns>message dialog result</returns>
        private Task<MessageDialogResult> ShowChangesFoundMessage()
        {
            // set dialog settings
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
                AnimateShow = true,
                AnimateHide = false,
            };

            return this.Dialog.Dialog
                .ShowMessageAsync(this, Messages.TitleApp,
                Messages.MainWindowViewModelCheckForChanges,
                MessageDialogStyle.AffirmativeAndNegative,
                settings);
        }

        /// <summary>
        /// Initializes all MainWindow bindings
        /// </summary>
        private void InitMainWindow()
        {

            // set display name
            this.DisplayName = string.Format(Messages.MainWindowDisplayName, CoreAssembly.MajorMinorVersion());

            // set initial status
            this.Status = Messages.MainWindowInitialStatus;

            // set help button tool tip text
            this.HelpTooltip = Messages.MainWindowHelpTooltip;

            // set menu button tool tip text
            this.MenuTooltip = Messages.MainWindowMenuTooltip;

            // set starting percentage to 0
            this.TaskStatusPercentage = 0;

            // set progress bar to zero
            this.TaskStatusVisible = Visibility.Hidden;

            // create shortcuts
            this.CreateWorkspaces();

            // register MainWindowViewModel to the add notification
            this.MessengerInstance.Register<NotificationMessage<WorkspaceViewModel>>(this, (n) => this.AddWorkspace(n));

            // create user repository
            this.userRepository = Factory.Create<IUserRepository>();

            // set restart flag
            restart = false;

            // create and start dispatcher timer
            var timer = new DispatcherTimer();
            timer.Tick += this.checkForUpdateDispatcherTimer_Tick;
            timer.Interval = TimeSpan.FromMinutes(5);
            timer.Start();

        }

        /// <summary>
        /// Create collections
        /// </summary>
        private void CreateWorkspaces()
        {

            _documentWorkspaces = new ObservableCollection<WorkspaceViewModel>();
            _documentWorkspaces.CollectionChanged += this.OnDocumentWorkspacesChanged;
            _anchorableWorkspaces = new ObservableCollection<WorkspaceViewModel>();

        }

        /// <summary>
        /// Create main menu commands
        /// </summary>
        /// <param name="user">logged in username</param>
        /// <param name="controller">progress dialog controller</param>
        private void CreateCommands(User user, ProgressDialogController controller)
        {

            List<CommandViewModel> commandList = new List<CommandViewModel>();

            // add commands based on user access level
            switch (user.AccessLevel)
            {
                case Enums.AccessLevel.Full:

                    this.GetFullAccessCommands(commandList);

                    break;
                case Enums.AccessLevel.ShippingData:

                    this.GetShippingDataAccessCommands(commandList);

                    break;
                case Enums.AccessLevel.MaintenanceData:

                    this.GetMaintenanceDataAccessCommands(commandList);

                    break;
                case Enums.AccessLevel.DriverData:

                    // default commands

                    break;
                default:
                    break;
            }

            // sort commands based on the display name
            commandList.Sort((x, y) =>
            {
                return x.DisplayName.CompareTo(y.DisplayName);
            });

            // add default commands
            commandList.Add(new CommandViewModel(
                    Messages.ThemeDisplayName,
                    Messages.ThemeDisplayToolTip,
                    new RelayCommand(param => this.ExecuteCommand(() => this.CreateThemeAnchorable())),
                    "DrawPaintbrush"));
            commandList.Add(new CommandViewModel(
                    Messages.AppSettingsDisplayName,
                    Messages.AppSettingsDisplayToolTip,
                    new RelayCommand(param => this.ExecuteCommand(() => this.CreateAppSettings())),
                    "Settings"));
            commandList.Add(new CommandViewModel(
                    Messages.LogoutDisplayName,
                    Messages.LogoutDisplayToolTip,
                    new RelayCommand(param => this.ExecuteCommand(() => this.Logout())),
                    "Close"));

            // set list of commands
            this.SetCommandList(commandList);

            // set display name
            this.DisplayName = string.Format(Messages.MainWindowDisplayNameLoggedIn, CoreAssembly.MajorMinorVersion(), CoreAssembly.CurrentUser());

            // close loading dialog
            controller.CloseAsync();

        }

        /// <summary>
        /// Gets the maintenance data access commands.
        /// </summary>
        /// <param name="commandList">The command list.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void GetMaintenanceDataAccessCommands(List<CommandViewModel> commandList)
        {
                        
            commandList.Add(
                    new CommandViewModel(
                        Messages.AllVehicleDisplayName,
                        Messages.AllVehicleDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllVehicleViewModel>(this.Dialog, this.TaskManager, Factory.Create<IVehicleRepository>()))),
                            "Truck"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllMaintenanceRecordDisplayName,
                        Messages.AllMaintenanceRecordDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllMaintenanceRecordViewModel>(this.Dialog, this.TaskManager, Factory.Create<IMaintenanceRecordRepository>(),
                        Factory.Create<IVehicleRepository>()))),
                            "Tools"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllMaintenanceRecordDetailDisplayName,
                        Messages.AllMaintenanceRecordDetailDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllMaintenanceRecordDetailViewModel>(this.Dialog, this.TaskManager, Factory.Create<IMaintenanceRecordDetailRepository>(),
                        Factory.Create<IMaintenanceRecordRepository>(), Factory.Create<IUserRepository>()))),
                            "PeopleProfile"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllMaintenancePartDisplayName,
                        Messages.AllMaintenancePartDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllMaintenancePartViewModel>(this.Dialog, this.TaskManager, Factory.Create<IMaintenancePartRepository>(),
                        Factory.Create<IMaintenanceRecordDetailRepository>(), Factory.Create<IPartRepository>()))),
                            "ListAddBelow"));

        }

        /// <summary>
        /// Gets the shipping data access commands.
        /// </summary>
        /// <param name="commandList">The command list.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void GetShippingDataAccessCommands(List<CommandViewModel> commandList)
        {
            
            commandList.Add(
                    new CommandViewModel(
                        Messages.AllPartDisplayName,
                        Messages.AllPartDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllPartViewModel>(this.Dialog, this.TaskManager, Factory.Create<IPartRepository>()))),
                            "Cart"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllManifestDisplayName,
                        Messages.AllManifestDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllManifestViewModel>(this.Dialog, this.TaskManager, Factory.Create<IManifestRepository>(),
                        Factory.Create<IVehicleRepository>(), Factory.Create<IUserRepository>()))),
                            "ListCheck"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllPurchaseOrderDisplayName,
                        Messages.AllPurchaseOrderDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllPurchaseOrderViewModel>(this.Dialog, this.TaskManager, Factory.Create<IPurchaseOrderRepository>(),
                        Factory.Create<IBusinessPartnerRepository>(), Factory.Create<IManifestRepository>()))),
                            "Creditcard"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllPurchaseItemDisplayName,
                        Messages.AllPurchaseItemDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllPurchaseItemViewModel>(this.Dialog, this.TaskManager, Factory.Create<IPurchaseItemRepository>(),
                        Factory.Create<IPurchaseOrderRepository>(), Factory.Create<IPartRepository>()))),
                            "ListAddBelow"));

        }

        /// <summary>
        /// Gets the full access commands.
        /// </summary>
        /// <param name="commandList">The command list.</param>
        private void GetFullAccessCommands(List<CommandViewModel> commandList)
        {

            commandList.Add(
                new CommandViewModel(
                    Messages.AllUserDisplayName,
                    Messages.AllUserDisplayToolTip,
                    new RelayCommand(param => this.ExecuteCommand(() =>
                    this.CreateDocument<AllUserViewModel>(this.Dialog, this.TaskManager, this.userRepository))),
                        "People"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllBusinessPartnerDisplayName,
                        Messages.AllBusinessPartnerDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllBusinessPartnerViewModel>(this.Dialog, this.TaskManager, Factory.Create<IBusinessPartnerRepository>()))),
                            "Group"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllPayrollDisplayName,
                        Messages.AllPayrollDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllPayrollViewModel>(this.Dialog, this.TaskManager, Factory.Create<IPayrollRepository>(),
                        Factory.Create<IUserRepository>()))),
                            "Money"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllVehicleDisplayName,
                        Messages.AllVehicleDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllVehicleViewModel>(this.Dialog, this.TaskManager, Factory.Create<IVehicleRepository>()))),
                            "Truck"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllPartDisplayName,
                        Messages.AllPartDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllPartViewModel>(this.Dialog, this.TaskManager, Factory.Create<IPartRepository>()))),
                            "Cart"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllManifestDisplayName,
                        Messages.AllManifestDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllManifestViewModel>(this.Dialog, this.TaskManager, Factory.Create<IManifestRepository>(),
                        Factory.Create<IVehicleRepository>(), Factory.Create<IUserRepository>()))),
                            "ListCheck"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllPurchaseOrderDisplayName,
                        Messages.AllPurchaseOrderDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllPurchaseOrderViewModel>(this.Dialog, this.TaskManager, Factory.Create<IPurchaseOrderRepository>(),
                        Factory.Create<IBusinessPartnerRepository>(), Factory.Create<IManifestRepository>()))),
                            "Creditcard"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllPurchaseItemDisplayName,
                        Messages.AllPurchaseItemDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllPurchaseItemViewModel>(this.Dialog, this.TaskManager, Factory.Create<IPurchaseItemRepository>(),
                        Factory.Create<IPurchaseOrderRepository>(), Factory.Create<IPartRepository>()))),
                            "ListAddBelow"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllMaintenanceRecordDisplayName,
                        Messages.AllMaintenanceRecordDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllMaintenanceRecordViewModel>(this.Dialog, this.TaskManager, Factory.Create<IMaintenanceRecordRepository>(),
                        Factory.Create<IVehicleRepository>()))),
                            "Tools"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllMaintenanceRecordDetailDisplayName,
                        Messages.AllMaintenanceRecordDetailDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllMaintenanceRecordDetailViewModel>(this.Dialog, this.TaskManager, Factory.Create<IMaintenanceRecordDetailRepository>(),
                        Factory.Create<IMaintenanceRecordRepository>(), Factory.Create<IUserRepository>()))),
                            "PeopleProfile"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.AllMaintenancePartDisplayName,
                        Messages.AllMaintenancePartDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<AllMaintenancePartViewModel>(this.Dialog, this.TaskManager, Factory.Create<IMaintenancePartRepository>(),
                        Factory.Create<IMaintenanceRecordDetailRepository>(), Factory.Create<IPartRepository>()))),
                            "ListAddBelow"));

            commandList.Add(
                    new CommandViewModel(
                        Messages.ReportDisplayName,
                        Messages.ReportDisplayToolTip,
                        new RelayCommand(param => this.ExecuteCommand(() =>
                        this.CreateDocument<ReportViewModel>(this.Dialog, this.TaskManager, Factory.Create<IReportRepository>()))),
                            "GraphLineUp"));


        }

        /// <summary>
        /// Set command list
        /// </summary>
        /// <param name="commandList">list of commands</param>
        private void SetCommandList(List<CommandViewModel> commandList)
        {
            _commands = new ReadOnlyCollection<CommandViewModel>(commandList);
            base.OnPropertyChanged("Commands");
        }

        /// <summary>
        /// Create login window
        /// </summary>
        public async void Login()
        {

            string errorMsg = string.Empty;
            bool exit = false;

            // create login dialog setting
            LoginDialogSettings settings = new LoginDialogSettings()
            {
                InitialUsername = CoreAssembly.CurrentUser(),
                NegativeButtonVisibility = Visibility.Visible
            };

            // loop until valid credentials are entered
            while (!exit)
            {


                try
                {

                    if (true)
                    {

                        // show login dialog
                        LoginDialogData credentials = await this.Dialog.Dialog.ShowLoginAsync(
                            this.Dialog.ViewModel, Messages.TitleApp, Messages.LoginStart, settings);

                        // close application if the user cancels
                        if (credentials == null)
                        {
                            this.OnClosing(this, new CancelEventArgs());
                            return;
                        }

                        string userName = credentials.Username;
                        User user = this.userRepository.GetUserByUserName(userName); 

                        // validate username first
                        if (user == null)
                        {

                            // show failure message
                            await this.Dialog.Dialog.ShowMessageAsync(
                                this.Dialog.ViewModel, Messages.TitleApp, string.Format(Messages.LoginFailUsername, userName));

                        }
                        else
                        {

                            // validate credentials
                            if (PasswordService.ValidatePassword(credentials.Password, user.Passphrase, user.HashKey))
                            {

                                // set current application user
                                CoreAssembly.SetCurrentUser(credentials.Username);

                                // load user theme settings
                                this.UserThemeViewModel = WorkspaceFactory.Create<UserThemeViewModel>();

                                // show loading while changing the user time sheet is loading
                                var controller = await this.Dialog.Dialog.ShowProgressAsync(this.Dialog.ViewModel, Messages.TitleApp, Messages.LoginLoading);
                                await Task.Delay(500);

                                // add dashboard
                                this.CreateDocument<DashboardViewModel>(user, this.TaskManager, this.Dialog);

                                // create command list
                                this.CreateCommands(user, controller);

                                // set exit flag
                                exit = true;

                            }
                            else
                            {

                                // show failure message
                                await this.Dialog.Dialog.ShowMessageAsync(
                                    this.Dialog.ViewModel, Messages.TitleApp, string.Format(Messages.LoginFailPassword, userName));

                            }

                        } 

                    }
                    else
                    {

                        // testing user
                        User user = new User(1, "dc0059", "Donal", "David", "Cavanaugh",
                            "12345 some where Dr.", "Huntsville", "Alabama", 35802, "1234567890",
                            "1234567890", "dc0059@uah.edu", 50, DateTime.Now, Enums.AccessLevel.Full,
                            "UAH", "The Man", true, @"uXgbWhLABkEQ/khtpfAmCDP4A/an3qV/351ndiBxKJ4acPPr6LDV90kdx3pFbOSqSkev2KXibj8Ok08vuIVf2g==", @"d2Trfixx39ZtUkwyBZJeVepV9NZ8nuTCAFe6rDPzxQprzq3ExWiRpBGFyVlRB0FJk07G0F0Pj7C5lrMSoph9026AID0Q+hqGmXBNFJGXV4lg1GZNGj8QTp/h3NPeAyN1Vt51ERCxb/jYYRTWZzncIamj2m/Pw3dflCcOJnPi91Zp4ZvT5tdWnLduRVQDKaYczfDADFgHqr3I6amy3mYx+Z/IPBzCMqxxB2pOODMjXfgbFE0RorP+Z7F5oj74xCpSNXlPClJk9bjM/ATnisp1lyMsruSNy1b3JSz2jm66g6VBz1a3rcukgpT9Te+wBQzu31Waip3PcF82+qFFhbMB3w==");

                        // set current application user
                        CoreAssembly.SetCurrentUser("dc0059");

                        // load user theme settings
                        this.UserThemeViewModel = WorkspaceFactory.Create<UserThemeViewModel>();

                        // show loading while changing the user time sheet is loading
                        var controller = await this.Dialog.Dialog.ShowProgressAsync(this.Dialog.ViewModel, Messages.TitleApp, Messages.LoginLoading);
                        await Task.Delay(500);

                        // add dashboard
                        this.CreateDocument<DashboardViewModel>(user, this.TaskManager, this.Dialog);

                        // create commands for debugging
                        this.CreateCommands(user, controller);

                        // set exit flag
                        exit = true;

                    }

                }
                catch (Exception ex)
                {

                    errorMsg = Messages.MainWindowUnexpectedLoginError;
                    log.Error(errorMsg, ex);

                }

                // show failure message
                if (errorMsg != string.Empty)
                {
                    await this.Dialog.Dialog.ShowMessageAsync(this.Dialog.ViewModel, Messages.TitleApp, errorMsg);
                    errorMsg = string.Empty;
                }

            }

        }

        /// <summary>
        /// Log user out of the application
        /// </summary>
        private async void Logout()
        {

            if (!await this.CheckForChanges() && TaskManager.TaskCount() == 0)
            {

                // remove all documents and anchorables
                this.DocumentWorkspaces.Clear();
                this.AnchorableWorkspaces.Clear();

                // set display name
                this.DisplayName = string.Format(Messages.MainWindowDisplayName, CoreAssembly.MajorMinorVersion());

                // clear command list
                this.SetCommandList(new List<CommandViewModel>());

                // create login
                this.Login();

            }

        }

        /// <summary>
        /// Create document window T
        /// </summary>
        /// <typeparam name="T">type of ViewModel</typeparam>
        private void CreateDocument<T>() where T : WorkspaceViewModel
        {

            // check to see if the workspace already exists
            var workspace = this.DocumentWorkspaces.FirstOrDefault(vm => vm is T) as T;

            if (workspace == null)
            {
                                
                this.TaskManager.AddTask(Task.Run(() => workspace = WorkspaceFactory.Create<T>(this.Dialog, this.TaskManager)),
                    Messages.MainWindowLoadingWindow, () => this.DocumentWorkspaces.Add(workspace),
                    Messages.MainWindowInitialStatus,
                    UIContext.Current,
                    "opening menu item",
                    string.Empty,
                    log);

            }

        }

        /// <summary>
        /// Create document window T
        /// </summary>
        /// <typeparam name="T">type of ViewModel</typeparam>
        /// <param name="constructorArgs">list of constructor arguments</param>
        private void CreateDocument<T>(params object[] constructorArgs) where T : WorkspaceViewModel
        {

            // check to see if the workspace already exists
            var workspace = this.DocumentWorkspaces.FirstOrDefault(vm => vm is T) as T;

            if (workspace == null)
            {

                this.TaskManager.AddTask(Task.Run(() => workspace = WorkspaceFactory.Create<T>(constructorArgs)),
                    Messages.MainWindowLoadingWindow,() => this.DocumentWorkspaces.Add(workspace), 
                    Messages.MainWindowInitialStatus, 
                    UIContext.Current, 
                    "opening menu item", 
                    string.Empty, 
                    log);                

            }

        }

        /// <summary>
        /// Create theme anchorable window
        /// </summary>
        private void CreateThemeAnchorable()
        {

            // Check to see if the workspaces already exists
            var workspace = this.AnchorableWorkspaces
                .FirstOrDefault(vm => vm is UserThemeViewModel)
                as UserThemeViewModel;

            if (workspace == null)
            {

                // load user theme settings
                this.UserThemeViewModel =
                    WorkspaceFactory.Create<UserThemeViewModel>();

                // add workspace
                workspace = this.UserThemeViewModel;
                this.AnchorableWorkspaces.Add(workspace);

            }
            else
            {
                workspace.IsVisible = true;

            }

        }

        /// <summary>
        /// Create App settings tool window
        /// </summary>
        private void CreateAppSettings()
        {

            // Hide menu
            this.HideMenu();

            // Check to see if the workspaces already exists
            var workspace = this.AnchorableWorkspaces
                .FirstOrDefault(vm => vm is AppSettingsViewModel)
                as AppSettingsViewModel;

            if (workspace == null)
            {

                // add workspace
                workspace = WorkspaceFactory.Create<AppSettingsViewModel>(this.TaskManager);
                this.AnchorableWorkspaces.Add(workspace);

            }
            else
            {
                workspace.IsVisible = true;
            }

        }

        /// <summary>
        /// Create help document view
        /// </summary>
        private void CreateHelp()
        {

            try
            {

                // set file name
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Path.GetFileName(CoreAssembly.GetHelpDocument()));

                // copy file from the network
                File.Copy(CoreAssembly.GetHelpDocument(), fileName, true);

                // open copied file
                Process.Start(fileName);

            }
            catch (Exception ex)
            {

                log.Error(Messages.HelpDocumentDowloadError, ex);
                this.Dialog.Dialog.ShowMessageAsync(this, Messages.TitleApp, Messages.HelpDocumentDowloadError);

            }

        }

        /// <summary>
        /// Hide main menu
        /// </summary>
        private void HideMenu()
        {
            this.IsMenuOpen = false;
        }

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="commandAction">command action</param>
        private void ExecuteCommand(Action commandAction)
        {

            // Hide menu
            this.HideMenu();

            // execute action
            commandAction();

        }

        /// <summary>
        /// Check for any updates.
        /// If any updates are found then it will initialize a
        /// application restart.
        /// </summary>
        private void CheckForUpdate()
        {

            // call restart
            try
            {

                if (CoreAssembly.CheckForUpdate())
                {
                    this.RestartApp();
                }

            }
            catch (Exception ex)
            {

                log.Error("Failed to restart application.", ex);

            }

        }

        /// <summary>
        /// Will restart the application and save any pending changes
        /// on the time sheet
        /// </summary>
        private void RestartApp()
        {

            // initialize restart
            restart = true;
            CoreAssembly.RestartApp();

        }

        #endregion

        #region Properties

        /// <summary>
        /// Synchronized context
        /// </summary>
        private TaskScheduler scheduler = UIContext.Current;

        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Flag indicating the application is shutting down
        /// </summary>
        private bool shutdown;

        /// <summary>
        /// Flag indicating the application is restarting
        /// </summary>
        private bool restart;

        /// <summary>
        /// User repository
        /// </summary>
        private IUserRepository userRepository;

        /// <summary>
        /// Dialog service to show dialogs from ViewModels
        /// </summary>        
        public IDialogService Dialog { get; set; }        

        private string _status;

        /// <summary>
        /// Status text to show user
        /// </summary>
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {

                if (_status == value)
                {
                    return;
                }

                _status = value;

                OnPropertyChanged("Status");

            }
        }

        /// <summary>
        /// Tool tip for the help menu
        /// </summary>
        public string HelpTooltip { get; set; }

        /// <summary>
        /// Tool tip for the main menu
        /// </summary>
        public string MenuTooltip { get; set; }

        private double _taskStatusPercentage;

        /// <summary>
        /// Task percentage
        /// </summary>
        public double TaskStatusPercentage
        {
            get
            {
                return _taskStatusPercentage;
            }
            set
            {

                if (_taskStatusPercentage == value)
                {
                    return;
                }

                _taskStatusPercentage = value;

                OnPropertyChanged("TaskStatusPercentage");

                if (_taskStatusPercentage > 0)
                {
                    this.TaskStatusVisible = Visibility.Visible;
                    OnPropertyChanged("TaskStatusVisible");
                }
                else
                {
                    this.TaskStatusVisible = Visibility.Hidden;
                    OnPropertyChanged("TaskStatusVisible");
                }
            }
        }

        /// <summary>
        /// Visibility of the task progress bar
        /// </summary>
        public Visibility TaskStatusVisible { get; set; }

        private ObservableCollection<WorkspaceViewModel> _documentWorkspaces;

        /// <summary>
        /// Returns the collection of available workspaces to display.
        /// A 'workspace' is a ViewModel that can request to be closed.
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> DocumentWorkspaces
        {
            get
            {
                return _documentWorkspaces;
            }
        }

        private ObservableCollection<WorkspaceViewModel> _anchorableWorkspaces;

        /// <summary>
        /// Returns the collection of available workspaces to display.
        /// A 'workspace' is a ViewModel that can request to be closed.
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> AnchorableWorkspaces
        {
            get
            {
                return _anchorableWorkspaces;
            }
        }

        private ReadOnlyCollection<CommandViewModel> _commands;

        /// <summary>
        /// Returns a read-only list of commands 
        /// that the UI can display and execute.
        /// </summary>
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                return _commands;
            }
        }

        private UserThemeViewModel _userThemeViewModel;

        /// <summary>
        /// User theme ViewModel
        /// </summary>
        public UserThemeViewModel UserThemeViewModel
        {
            get { return _userThemeViewModel; }
            set { _userThemeViewModel = value; }
        }

        private bool _isMenuOpen;

        /// <summary>
        /// Flag indicating the main menu is open
        /// </summary>
        public bool IsMenuOpen
        {
            get
            {
                return _isMenuOpen;
            }
            set
            {

                if (_isMenuOpen == value)
                {
                    return;
                }

                _isMenuOpen = value;

                base.OnPropertyChanged("IsMenuOpen");

            }
        }

        private ICommand _commandHelp;

        public ICommand CommandHelp
        {
            get
            {

                if (_commandHelp == null)
                {
                    _commandHelp = new RelayCommand(param =>
                    {
                        this.CreateHelp();
                    });
                }

                return _commandHelp;
            }
        }

        public override string DisplayName
        {
            get
            {
                return base.DisplayName;
            }
            protected set
            {
                base.DisplayName = value;
                base.OnPropertyChanged("DisplayName");
            }
        }

        #endregion

    }
}
