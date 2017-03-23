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
    /// This class will handle the maintenance of the payroll model
    /// </summary>
    public class PayrollViewModel : WorkspaceViewModel, IDataErrorInfo, IChanges, IKeyCommand
    {

        #region Constructor

        #endregion

        #region Methods

        #endregion

        #region Properties

        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// payroll model
        /// </summary>
        public Payroll Model;

        /// <summary>
        /// payroll repository
        /// </summary>
        private IPayrollRepository payrollRepository;

        /// <summary>
        /// <see cref="DataAccess.Models.Payroll"/>
        /// </summary>
        public long EmployeeID
        {
            get
            {
                return Model.EmployeeID;
            }
            set
            {

                if (Model.EmployeeID == value)
                {
                    return;
                }

                Model.EmployeeID = value;

                base.OnPropertyChanged("EmployeeID");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="DataAccess.Models.Payroll"/>
        /// </summary>
        public DateTime PaymentDate
        {
            get
            {
                return Model.PaymentDate;
            }
            set
            {

                if (Model.PaymentDate == value)
                {
                    return;
                }

                Model.PaymentDate = value;

                base.OnPropertyChanged("PaymentDate");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="DataAccess.Models.Payroll"/>
        /// </summary>
        public double Payment
        {
            get
            {
                return Model.Payment;
            }
            set
            {

                if (Model.Payment == value)
                {
                    return;
                }

                Model.Payment = value;

                base.OnPropertyChanged("Payment");
                this.HasChanges = true;

            }
        }

        /// <summary>
        /// <see cref="DataAccess.Models.Payroll"/>
        /// </summary>
        public double HoursWorked
        {
            get
            {
                return Model.HoursWorked;
            }
            set
            {

                if (Model.HoursWorked == value)
                {
                    return;
                }

                Model.HoursWorked = value;

                base.OnPropertyChanged("HoursWorked");
                this.HasChanges = true;

            }
        }

        #endregion

    }
}
