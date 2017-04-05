using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Services;
using CS499.TCMS.View.ViewModels;
using CS499.TCMS.View.Views;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Windows;
using System.Windows.Threading;

//Here is the once-per-application setup information
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace CS499.TCMS.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // configure unhandled exceptions
            this.ConfigureUnhandledExceptions();

            // set the start username
            log4net.GlobalContext.Properties["userNameProperty"] = CoreAssembly.CurrentUser();
            log.Info("Started application.");

            // show splash screen
            SplashScreenService.ShowSplash();

            // play truck horn
            CoreAssembly.PlayTruckSound();

            // get the update location
            CoreAssembly.SetUpdateLocation();

            // create new instance of main window
            MainWindow window = new MainWindow();

            // create new instance of ViewModel
            var viewModel = new MainWindowViewModel();
            viewModel.Dialog = new DialogService(DialogCoordinator.Instance, viewModel);

            // create task manager and subscribe to the change event
            ITaskManager taskManager = new TaskManager(viewModel.Dialog);
            viewModel.TaskManager = taskManager;
            taskManager.OnTaskStatusChanged += viewModel.OnTaskStatusChanged;
                       
            // bind ViewModel to window
            window.DataContext = viewModel;

            // add closing event handler
            window.Closing += viewModel.OnClosing;
            window.KeyDown += viewModel.OnKeyPress;

            // close splash screen when the main window ViewModel is done loading all data
            SplashScreenService.CloseSplash();

            // show created window
            window.Show();

            // show login
            viewModel.Login();

            // fixes an issue where the window was not focused after the splash screen closes
            window.Topmost = true;
            window.Topmost = false;
            window.Activate();

        }

        /// <summary>
        /// Configure unhandled exceptions
        /// </summary>
        private void ConfigureUnhandledExceptions()
        {

            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        }

        /// <summary>
        /// Catch unhandled domain exceptions and close gracefully
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">exception</param>
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

            try
            {

                string title = string.Format("Fatal Non-UI Exception: {0}", Environment.MachineName);
                string message = "A fatal exception has occurred and the application must close. Please contact support with a screen shot of this message.";

                // log error
                LogUnhandledException(e.ExceptionObject as Exception, title, message);

            }
            catch
            {
                CoreAssembly.CloseApp();
            }
            finally
            {
                CoreAssembly.CloseApp();
            }

        }

        /// <summary>
        /// Catch unhandled dispatcher exceptions and close gracefully
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">exception</param>
        void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {

            try
            {

                string title = string.Format("Fatal UI Exception: {0}", Environment.MachineName);
                string message = "A fatal exception has occurred and the application must close. Please contact support with a screen shot of this message.";

                // log error
                LogUnhandledException(e.Exception, title, message);

            }
            catch
            {
                CoreAssembly.CloseApp();
            }
            finally
            {
                e.Handled = true;
                CoreAssembly.CloseApp();
            }

        }

        /// <summary>
        /// Log unhandled exception for debugging purposes
        /// </summary>
        /// <param name="e">exception</param>
        /// <param name="title">title of the message to show user</param>
        /// <param name="message">short message to alert the user of what to do next</param>
        private static void LogUnhandledException(Exception e, string title, string message)
        {

            // log error
            log.Error(title, e);

            // show error message to user
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);

        }

        /// <summary>
        /// Log application termination for log file parsing
        /// </summary>
        /// <param name="e">The <see cref="ExitEventArgs"/></param>
        protected override void OnExit(ExitEventArgs e)
        {

            base.OnExit(e);

            // log close
            log.Info("Closed application.");

        }

    }
}
