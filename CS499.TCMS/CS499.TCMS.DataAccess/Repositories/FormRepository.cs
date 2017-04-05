using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolKit.Data;
using CS499.TCMS.DataAccess.IRepositories;
using System.Data;

namespace CS499.TCMS.DataAccess.Repositories
{
    internal class ReportRepository : GenericRepository<DataTable>, IReportRepository
    {
        #region Constructor

        /// <summary>
        /// Form Repository Constructor
        /// </summary>
        /// <param name="database"></param>
        public ReportRepository(IDatabase database) : base(database)
        {
        }
        #endregion

        #region Methods
        public DataTable GetIncomingShipmentReport()
        {
            throw new NotImplementedException();
        }

        public DataTable GetMaintenanceCostReport()
        {
            throw new NotImplementedException();
        }

        public DataTable GetOutgoingShipmentReport()
        {
            throw new NotImplementedException();
        }

        public DataTable GetPayrollReport(DateTime date)
        {

            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT payroll.EmployeeID, payroll.PaymentDate, payroll.Payment, payroll.HoursWorked, user.FirstName, " +
                              "user.MiddleName, user.LastName, user.Address, user.City, user.State, user.ZipCode, user.PayRate, user.HomeStore, user.JobDescription " +
                              "FROM payroll " +
                              "INNER JOIN user ON payroll.EmployeeID = user.EmployeeID " +
                              "WHERE PaymentDate BETWEEN ? and ? " +
                              "ORDER BY EmployeeID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            //Create parameter
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Date",
                Type = DbType.DateTime,
                Value = date
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_CurrentDate",
                Type = DbType.DateTime,
                Value = DateTime.Now
            });

            return this.Database.ExecuteDataTableQuery(definition);
        }

        public DataTable GetVehicleMaintenanceReport()
        {
            throw new NotImplementedException();
        }

        protected override DataTable Map(IDataReader reader)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
