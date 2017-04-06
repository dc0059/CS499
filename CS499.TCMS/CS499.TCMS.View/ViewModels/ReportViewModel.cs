using CS499.TCMS.DataAccess.IRepositories;
using CS499.TCMS.Model;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Resources;
using CS499.TCMS.View.Services;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CS499.TCMS.View.ViewModels
{
    /// <summary>
    /// This class will handle running and exporting reports
    /// </summary>
    /// <seealso cref="CS499.TCMS.View.ViewModels.WorkspaceViewModel" />
    /// <seealso cref="CS499.TCMS.View.Interfaces.IKeyCommand" />
    public class ReportViewModel : WorkspaceViewModel, IKeyCommand
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportViewModel"/> class.
        /// </summary>
        /// <param name="dialog">Dialog service to show messages from ViewModel</param>
        /// <param name="taskManager">Task manager to hold reference to running tasks</param>
        /// <param name="reportRepository">The report repository.</param>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        public ReportViewModel(IDialogService dialog, ITaskManager taskManager, IReportRepository reportRepository, IVehicleRepository vehicleRepository)
        {
            this.dialog = dialog;
            this.TaskManager = taskManager;
            this.IsSelected = true;
            this.reportRepository = reportRepository;
            this.vehicleRepository = vehicleRepository;
            this.DisplayName = Messages.ReportDisplayName;
            this.DisplayToolTip = Messages.ReportDisplayToolTip;
            this.StartDate = DateTime.Today.AddDays(-7);
            this.EndDate = DateTime.Today;
            this.Vehicles = new ObservableCollection<Vehicle>();
            this.LoadVehicles();
            this.MessengerInstance.Register<NotificationMessage<AllVehicleViewModel>>(this, (n) => this.LoadVehicles(n));
            this.SetMenuVisibility();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load list of models
        /// </summary>
        /// <param name="notificationMessage">notification message</param>
        private void LoadVehicles(NotificationMessage<AllVehicleViewModel> notificationMessage)
        {
            this.LoadVehicles();
        }

        /// <summary>
        /// Load list of ViewModels
        /// </summary>
        private void LoadVehicles()
        {

            List<Vehicle> models = null;

            // start new task to get the models from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                models = vehicleRepository.GetAll().ToList();

            },
            TaskCreationOptions.LongRunning),
            Messages.AllVehicleLoading,
            () =>
            {

                if (models == null)
                {
                    return;
                }

                // set models
                this.Set(models);

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "loading vehicles",
             Messages.AllVehicleLoadError,
             log);

        }

        /// <summary>
        /// Add each Model to the collection
        /// </summary>
        /// <param name="vehicles">list of models</param>
        private void Set(List<Vehicle> vehicles)
        {

            // clear current list
            this.Vehicles.Clear();

            // loop through each model and add to the collection
            foreach (var model in vehicles)
            {
                this.Vehicles.Add(model);
            }

            // set selected vehicle
            this.SelectedVehicle = this.Vehicles.FirstOrDefault();

        }

        /// <summary>
        /// Run report
        /// </summary>
        private void RunReport()
        {

            DataTable data = null;

            // set end date time element to 23:59:59
            this.EndDate = new DateTime(this.EndDate.Year, this.EndDate.Month, this.EndDate.Day, 23, 59, 59);

            // Start task to get report data from the database
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {

                // select report type
                data = this.GetReport();

            },
            TaskCreationOptions.LongRunning),
            Messages.ReportLoading,
            () =>
            {

                // set report
                this.Report = data;

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "running report",
             Messages.ReportRunFailedError,
             log);

        }

        /// <summary>
        /// Get the report based on the report type
        /// </summary>
        /// <returns></returns>
        private DataTable GetReport()
        {

            switch (this.SelectedReportType)
            {
                case Enums.ReportTypes.Payroll:

                    return this.reportRepository.GetPayrollReport(this.StartDate, this.EndDate);

                case Enums.ReportTypes.Maintenance_Cost:

                    return this.reportRepository.GetMaintenanceCostReport();

                case Enums.ReportTypes.Vehicle_Maintenance:

                    return this.reportRepository.GetVehicleMaintenanceReport(this.SelectedVehicle);

                case Enums.ReportTypes.Incoming_Shipment:

                    return this.reportRepository.GetIncomingShipmentReport();

                case Enums.ReportTypes.Outgoing_Shipment:

                    return this.reportRepository.GetOutgoingShipmentReport();

                default:
                    break;
            }

            return null;
        }

        /// <summary>
        /// Export report
        /// </summary>
        private void ExportReport()
        {

            string filePath = null;

            // get save file name
            filePath = dialog.SaveFileDialog(Messages.ReportFileFilter);

            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            // Start task to export report data to excel
            this.TaskManager.AddTask(Task.Factory.StartNew(() =>
            {
                bool totalRow = false;

                // select the reports that get total row
                switch (this.SelectedReportType)
                {
                    case Enums.ReportTypes.Payroll:
                        totalRow = false;
                        break;
                    case Enums.ReportTypes.Maintenance_Cost:
                        break;
                    case Enums.ReportTypes.Vehicle_Maintenance:
                        break;
                    case Enums.ReportTypes.Incoming_Shipment:
                        break;
                    case Enums.ReportTypes.Outgoing_Shipment:
                        break;
                    default:
                        break;
                }

                ExportService.Export(filePath, this.Report, totalRow);

            },
            TaskCreationOptions.LongRunning),
            Messages.ReportExporting,
            () =>
            {

                // go ahead and open the file
                Process.Start(filePath);

            },
             Messages.MainWindowInitialStatus,
             UIContext.Current,
             "exporting report",
             "",
             log);

        }

        /// <summary>
        /// Validates the report can be run
        /// </summary>
        /// <returns>flag indicating if the report can be run</returns>
        private bool ValidateRun()
        {
            return StartDate < EndDate && this.SelectedVehicle != null;
        }

        /// <summary>
        /// Sets the menu visibility.
        /// </summary>
        private void SetMenuVisibility()
        {

            if (this.SelectedReportType == Enums.ReportTypes.Vehicle_Maintenance)
            {
                this.VehicleSelectionVisible = true;
                this.DateRangeVisible = false;
            }
            else
            {
                this.VehicleSelectionVisible = false;
                this.DateRangeVisible = true;
            }

        }

        /// <summary>
        /// Send key stroke to the ViewModel
        /// </summary>
        /// <param name="e">Key event args</param>
        void IKeyCommand.SendKeys(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.CanRun)
                {
                    this.RunReport();
                }
            }
        }

        /// <summary>
        /// Called when [request close].
        /// </summary>
        public override void OnRequestClose()
        {
            // unregister ViewModel
            this.MessengerInstance.Unregister(this);
            base.OnRequestClose();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Report repository
        /// </summary>
        private IReportRepository reportRepository;

        /// <summary>
        /// The vehicle repository
        /// </summary>
        private IVehicleRepository vehicleRepository;

        /// <summary>
        /// Dialog service for showing messages from the ViewModel
        /// </summary>
        private IDialogService dialog;

        private bool _vehicleSelectionVisible;

        /// <summary>
        /// Gets or sets a value indicating whether [vehicle selection visible].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [vehicle selection visible]; otherwise, <c>false</c>.
        /// </value>
        public bool VehicleSelectionVisible
        {
            get
            {
                return _vehicleSelectionVisible;
            }
            set
            {

                _vehicleSelectionVisible = value;
                base.OnPropertyChanged("VehicleSelectionVisible");

            }
        }

        private bool _dateRangesVisible;

        /// <summary>
        /// Gets or sets a value indicating whether [date range visible].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [date range visible]; otherwise, <c>false</c>.
        /// </value>
        public bool DateRangeVisible
        {
            get
            {
                return _dateRangesVisible;
            }
            set
            {

                _dateRangesVisible = value;
                base.OnPropertyChanged("DateRangeVisible");

            }
        }


        /// <summary>
        /// Report types
        /// </summary>
        public string[] ReportTypes
        {
            get
            {
                return Enums.GetHumanizedValues<Enums.ReportTypes>();
            }
        }

        private Enums.ReportTypes _selectedReportType;

        /// <summary>
        /// Selected report type
        /// </summary>
        public Enums.ReportTypes SelectedReportType
        {
            get { return _selectedReportType; }
            set
            {

                if (_selectedReportType == value)
                {
                    return;
                }

                _selectedReportType = value;
                
                base.OnPropertyChanged("SelectedReportType");
                this.SetMenuVisibility();

            }
        }       

        private DataTable _report;

        /// <summary>
        /// Report data table
        /// </summary>
        public DataTable Report
        {
            get
            {
                return _report;
            }
            set
            {

                if (_report == value)
                {
                    return;
                }

                _report = value;

                base.OnPropertyChanged("Report");

            }
        }

        /// <summary>
        /// Gets or sets the vehicles.
        /// </summary>
        /// <value>
        /// The vehicles.
        /// </value>
        public ObservableCollection<Vehicle> Vehicles { get; set; }

        private Vehicle _selectedVehicle;

        /// <summary>
        /// Gets or sets the selected vehicle.
        /// </summary>
        /// <value>
        /// The selected vehicle.
        /// </value>
        public Vehicle SelectedVehicle
        {
            get
            {
                return _selectedVehicle;
            }
            set
            {

                if (_selectedVehicle == value)
                {
                    return;
                }

                _selectedVehicle = value;
                base.OnPropertyChanged("SelectedVehicle");

            }
        }


        /// <summary>
        /// Start date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Can run report flag
        /// </summary>
        public bool CanRun
        {
            get { return this.ValidateRun(); }
        }

        /// <summary>
        /// Can export report flag
        /// </summary>
        public bool CanExport
        {
            get
            {

                if (this.Report != null)
                {
                    return this.Report.Rows.Count != 0;
                }
                else
                {
                    return false;
                }

            }
        }

        private ICommand _commandRun;

        /// <summary>
        /// Run report command
        /// </summary>
        public ICommand CommandRun
        {
            get
            {

                if (_commandRun == null)
                {
                    _commandRun = new RelayCommand(param =>
                    {
                        this.RunReport();
                    },
                        param => this.CanRun);
                }

                return _commandRun;
            }
        }

        private ICommand _commandExport;

        /// <summary>
        /// Export report command
        /// </summary>
        public ICommand CommandExport
        {
            get
            {

                if (_commandExport == null)
                {
                    _commandExport = new RelayCommand(param =>
                    {
                        this.ExportReport();
                    },
                        param => this.CanExport);
                }

                return _commandExport;
            }
        }

        #endregion

    }
}
