using CS499.TCMS.View.Models;
using CS499.TCMS.VIEW;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Themes;

namespace CS499.TCMS.View.Services
{
    /// <summary>
    /// This class will handle core assembly tasks
    /// </summary>
    public class CoreAssembly
    {

        #region Properties

        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get the reference of the containing assembly
        /// </summary>
        public static readonly Assembly Reference = typeof(CoreAssembly).Assembly;

        /// <summary>
        /// Gets the current version of the containing assembly
        /// </summary>
        public static readonly string Version = Reference.GetName().Version.ToString();

        /// <summary>
        /// Secure string to hold the users password
        /// </summary>
        private static SecureString secureString = new SecureString();

        #endregion

        #region Methods

        /// <summary>
        /// Gets the current major and minor version of the containing assembly
        /// </summary>
        /// <returns>string containing the version or "Debug" if the application is not deployed</returns>
        public static string MajorMinorVersion()
        {
            return !Debugger.IsAttached && ApplicationDeployment.IsNetworkDeployed ?
                string.Format("Version {0}.{1}", Reference.GetName().Version.Major, Reference.GetName().Version.Minor) :
                "Debug";
        }

        /// <summary>
        /// Get the database name
        /// </summary>
        /// <returns>production database name if not debugging, dev database name otherwise</returns>
        public static string GetDatabaseName()
        {
            return IsDebug() ? Properties.Settings.Default.DevDatabase : Properties.Settings.Default.ProductionDatabase;
        }

        /// <summary>
        /// Check to see if the application is in debug mode
        /// </summary>
        /// <returns>true if in debug mode, false otherwise</returns>
        public static bool IsDebug()
        {

#if (DEBUG)
            return true;
#else
            return false;
#endif

        }

        /// <summary>
        /// Get current viewModel
        /// </summary>
        public static string CurrentUser()
        {
            return Properties.Settings.Default.LoggedInUser;
        }

        /// <summary>
        /// Set current viewModel
        /// </summary>
        /// <param name="userName">viewModel name of the current viewModel</param>
        public static void SetCurrentUser(string userName)
        {
            Properties.Settings.Default.LoggedInUser = userName;
            Properties.Settings.Default.Save();
            log4net.GlobalContext.Properties["userNameProperty"] = userName;
        }

        /// <summary>
        /// Store the current user password in a secure string
        /// </summary>
        /// <param name="password">password to secure</param>
        public static void SetCurrentUserPassword(string password)
        {

            secureString.Clear();

            foreach (var c in password)
            {
                secureString.AppendChar(c);
            }

        }

        /// <summary>
        /// Get current user password
        /// </summary>
        /// <returns>password</returns>
        public static string CurrentUserPassword()
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }

        }

        /// <summary>
        /// Set the update location
        /// </summary>
        public static void SetUpdateLocation()
        {
            if (!IsDebug())
            {

                try
                {

                    Properties.Settings.Default.UpdateLocation = ApplicationDeployment.CurrentDeployment.UpdateLocation.OriginalString;
                    Properties.Settings.Default.Save();

                }
                catch (Exception ex)
                {
                    log.Error("Failed to set update location.", ex);
                }

            }
        }

        /// <summary>
        /// Gets the update location
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateLocation()
        {
            return Properties.Settings.Default.UpdateLocation;
        }

        /// <summary>
        /// Closes the main window
        /// </summary>
        public static void CloseApp()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Restarts the main window
        /// </summary>
        public static void RestartApp()
        {

            if (IsDebug())
            {
                Process.Start(Assembly.GetExecutingAssembly().Location);
            }
            else
            {
                Process.Start(Properties.Settings.Default.StartupPath);
            }

            CloseApp();

        }

        /// <summary>
        /// Checks for any deployement updates
        /// </summary>
        /// <returns>flag indicating that updates are pending</returns>
        public static bool CheckForUpdate()
        {

            if (!IsDebug())
            {

                // download manifest
                var manifest = new WebClient().DownloadString(GetUpdateLocation());

                // parse the manifest
                var doc = XDocument.Parse(manifest);

                // set namespace
                XNamespace xNamespace = "urn:schemas-microsoft-com:asm.v1";

                // get version
                var version = new Version(doc.Descendants(xNamespace + "assemblyIdentity")
                    .First().Attribute("version").Value);

                return version > ApplicationDeployment.CurrentDeployment.CurrentVersion;

            }

            return false;
        }

        /// <summary>
        /// Change theme on Avalondock dockmanager
        /// </summary>
        /// <param name="theme">Theme</param>
        public static void ChangeAvalonTheme(Theme theme)
        {

            DockingManager dockManager = Application.Current.Windows.OfType<MainWindow>()
                    .FirstOrDefault().MainDockManager;
            if (!dockManager.Theme.ToString().Equals(theme.ToString()))
                dockManager.Theme = theme;

        }

        /// <summary>
        /// Get the current accent theme as Drawing.Color
        /// </summary>
        /// <returns>Drawing.Color</returns>
        public static System.Drawing.Color GetAccentColor()
        {

            Color mediaColor = Color.FromRgb(255, 0, 0);

            if (Application.Current != null)
            {
                SolidColorBrush mediaBrush = (SolidColorBrush)Application.Current.TryFindResource("AccentColorBrush");
                mediaColor = mediaBrush.Color;
            }

            return System.Drawing.Color.FromArgb(mediaColor.A, mediaColor.R, mediaColor.G, mediaColor.B);
        }

        /// <summary>
        /// Get the help document file path
        /// </summary>
        /// <returns>file path to the help document</returns>
        public static string GetHelpDocument()
        {
            return Properties.Settings.Default.HelpDocument;
        }

        /// <summary>
        /// Get the file path location of the log file
        /// </summary>
        /// <returns>Full path to the log file</returns>
        public static string GetLogFileLocation()
        {
            return string.Format(Properties.Settings.Default.LogFileLocation, Path.GetTempPath());
        }

        /// <summary>
        /// Get a list of all loaded assemblies in the current
        /// appdomain
        /// </summary>
        /// <returns>List of assemblies</returns>
        public static List<AssemblyInformation> GetLoadedAssemblies()
        {

            List<AssemblyInformation> assemblies = new List<AssemblyInformation>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                assemblies.Add(new AssemblyInformation(assembly.GetName().Name,
                    assembly.GetName().Version.ToString()));
            }

            return assemblies.OrderBy(a => a.Name).ToList();
        }

        #endregion

    }
}
