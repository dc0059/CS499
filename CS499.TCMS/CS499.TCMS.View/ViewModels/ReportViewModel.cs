using CS499.TCMS.DataAccess.IRepositories;
using CS499.TCMS.Model;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Resources;
using CS499.TCMS.View.Services;
using System;
using System.Data;
using System.Diagnostics;
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
        /// Default constructor
        /// </summary>
        /// <param name="dialog">Dialog service to show messages from ViewModel</param>
        /// <param name="taskManager">Task manager to hold reference to running tasks</param>
        /// <param name="reportRepository">The report repository</param>
        public ReportViewModel(IDialogService dialog, ITaskManager taskManager, IReportRepository reportRepository)
        {
            this.dialog = dialog;
            this.TaskManager = taskManager;
            this.IsSelected = true;
            this.reportRepository = reportRepository;
            this.DisplayName = Messages.ReportDisplayName;
            this.DisplayToolTip = Messages.ReportDisplayToolTip;
            this.StartDate = DateTime.Today.AddDays(-7);
            this.EndDate = DateTime.Today;
        }

        #endregion

        #region Methods

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
            return StartDate < EndDate;
        }

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
        /// Dialog service for showing messages from the ViewModel
        /// </summary>
        private IDialogService dialog;

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
