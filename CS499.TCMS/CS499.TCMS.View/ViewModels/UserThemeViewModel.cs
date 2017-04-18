using CS499.TCMS.View.Models;
using CS499.TCMS.View.Resources;
using CS499.TCMS.View.Services;
using MahApps.Metro;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock.Themes;
using CS499.TCMS.Model;

namespace CS499.TCMS.View.ViewModels
{
    /// <summary>
    /// This class will hold the application theme information
    /// and interact with the <see cref="ThemeManager"/> and <see cref="DockingManager"/>
    /// </summary>
    public class UserThemeViewModel : WorkspaceViewModel
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserThemeViewModel"/> class.
        /// </summary>
        public UserThemeViewModel()
        {
            this.BuildAccentList();
            this.IsVisible = true;
            this.IsActive = true;
            this.DisplayName = Messages.ThemeDisplayName;
            this.DisplayToolTip = Messages.ThemeDisplayToolTip;
            this.LoadUserTheme();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Build collection of accents and themes
        /// </summary>
        private void BuildAccentList()
        {

            // create new collections
            this.Accents = ThemeManager.Accents.Select(a =>
                new ThemeType(a.Name, null, a.Resources["AccentColorBrush"] as Brush)).ToList();
            this.Themes = ThemeManager.AppThemes.Select(t =>
                new ThemeType(t.Name, t.Resources["BlackColorBrush"] as Brush, t.Resources["WhiteColorBrush"] as Brush)).ToList();

            // set current selected theme and accent
            this.CurrentTheme = this.Themes.FirstOrDefault(t => t.Name.Equals(Properties.Settings.Default.Theme));
            this.CurrentAccent = this.Accents.FirstOrDefault(a => a.Name.Equals(Properties.Settings.Default.Accent));

        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        private void Save()
        {

            // change current user theme to match the selected theme
            if (this.currentUserTheme == null)
            {
                this.currentUserTheme = new UserTheme(0, CoreAssembly.CurrentUser(),
                        this.CurrentTheme.Name, this.CurrentAccent.Name, this.currentUserTheme.AvalonTheme);
            }
            else
            {
                this.currentUserTheme.BaseColor = this.CurrentTheme.Name;
                this.currentUserTheme.AccentColor = this.CurrentAccent.Name;
                this.currentUserTheme.AvalonTheme = this.CurrentDockTheme;
            }

            // save theme
            CoreAssembly.SaveUserTheme(this.currentUserTheme);

            // set theme in application                                        
            this.SetTheme();

        }

        /// <summary>
        /// Sets the theme for the window
        /// </summary>
        private void SetTheme()
        {

            // get theme and accent by name
            var theme = ThemeManager.GetAppTheme(
                this.currentUserTheme == null ? this.CurrentTheme.Name : this.currentUserTheme.BaseColor);
            var accent = ThemeManager.GetAccent(
                this.currentUserTheme == null ? this.CurrentAccent.Name : this.currentUserTheme.AccentColor);

            // change theme and accent on AvalonDock DockingManager
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme);
            switch (this.CurrentDockTheme)
            {
                case AvalonThemes.Generic:
                    CoreAssembly.ChangeAvalonTheme(new GenericTheme());
                    break;
                case AvalonThemes.Aero:
                    CoreAssembly.ChangeAvalonTheme(new AeroTheme());
                    break;
                case AvalonThemes.Metro:
                    CoreAssembly.ChangeAvalonTheme(new MetroTheme());
                    break;                
                case AvalonThemes.Visual_Studio_2010:
                    CoreAssembly.ChangeAvalonTheme(new VS2010Theme());
                    break;
                case AvalonThemes.Visual_Studio_2013_Blue:
                    CoreAssembly.ChangeAvalonTheme(new Vs2013BlueTheme());
                    break;
                case AvalonThemes.Visual_Studio_2013_Dark:
                    CoreAssembly.ChangeAvalonTheme(new Vs2013DarkTheme());
                    break;
                case AvalonThemes.Visual_Studio_2013_Light:
                    CoreAssembly.ChangeAvalonTheme(new Vs2013LightTheme());
                    break;
                default:
                    CoreAssembly.ChangeAvalonTheme(new GenericTheme());
                    break;
            }

            // set flag
            this.HasChanges = false;

        }

        /// <summary>
        /// Check to see if the user can save changes
        /// </summary>
        /// <returns>flag indicating true/false</returns>
        private bool CanSaveTheme()
        {
            return this.HasChanges;
        }

        /// <summary>
        /// Load user theme from database or load default
        /// </summary>
        private void LoadUserTheme()
        {

            UserTheme userTheme = CoreAssembly.GetUserTheme();

            if (userTheme != null)
            {

                // set the current theme to match user
                this.currentUserTheme = userTheme;
                this.CurrentTheme = this.Themes.FirstOrDefault(
                (th) => th.Name == userTheme.BaseColor);
                this.CurrentAccent = this.Accents.FirstOrDefault(
                    (a) => a.Name == userTheme.AccentColor);
                this.CurrentDockTheme = userTheme.AvalonTheme;

            }

            // set theme in application
            this.SetTheme();
                       
        }

        #endregion

        #region Properties

        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets or sets the accents.
        /// </summary>
        /// <value>
        /// The accents.
        /// </value>
        public List<ThemeType> Accents { get; set; }

        /// <summary>
        /// Gets or sets the themes.
        /// </summary>
        /// <value>
        /// The themes.
        /// </value>
        public List<ThemeType> Themes { get; set; }

        private UserTheme currentUserTheme;

        private ThemeType _currentTheme;

        /// <summary>
        /// Gets or sets the current theme.
        /// </summary>
        /// <value>
        /// The current theme.
        /// </value>
        public ThemeType CurrentTheme
        {
            get
            {
                return _currentTheme;
            }
            set
            {

                if (_currentTheme == value)
                {
                    return;
                }

                _currentTheme = value;

                base.OnPropertyChanged("CurrentTheme");
                this.HasChanges = true;

            }
        }

        private ThemeType _currentAccent;

        /// <summary>
        /// Gets or sets the current accent.
        /// </summary>
        /// <value>
        /// The current accent.
        /// </value>
        public ThemeType CurrentAccent
        {
            get
            {
                return _currentAccent;
            }
            set
            {

                if (_currentAccent == value)
                {
                    return;
                }

                _currentAccent = value;

                base.OnPropertyChanged("CurrentAccent");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets the dock themes.
        /// </summary>
        /// <value>
        /// The dock themes.
        /// </value>
        public string[] DockThemes
        {
            get
            {
                return Enums.GetHumanizedValues<AvalonThemes>();
            }
        }

        private AvalonThemes _currentDockTheme;

        /// <summary>
        /// Gets or sets the current dock theme.
        /// </summary>
        /// <value>
        /// The current dock theme.
        /// </value>
        public AvalonThemes CurrentDockTheme
        {
            get
            {
                return _currentDockTheme;
            }
            set
            {

                if (_currentDockTheme == value)
                {
                    return;
                }

                _currentDockTheme = value;

                base.OnPropertyChanged("CurrentDockTheme");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can save.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can save; otherwise, <c>false</c>.
        /// </value>
        public bool CanSave
        {
            get { return this.CanSaveTheme(); }
        }

        private ICommand _saveCommand;

        /// <summary>
        /// Gets the save command.
        /// </summary>
        /// <value>
        /// The save command.
        /// </value>
        public ICommand SaveCommand
        {
            get
            {

                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(param => this.Save(),
                        param => this.CanSave);
                }

                return _saveCommand;
            }
        }


        /// <summary>
        /// Returns a visibility value for the object
        /// </summary>
        public override bool IsVisible
        {
            get
            {
                return base.IsVisible;
            }
            set
            {

                base.IsVisible = value;
                base.OnPropertyChanged("IsVisible");

            }
        }

        private bool _isActive;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {

                _isActive = value;
                base.OnPropertyChanged("IsActive");

            }
        }

        #endregion

    }
}
