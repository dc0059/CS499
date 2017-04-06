using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolKit.Data;
using CS499.TCMS.DataAccess.IRepositories;
using System.Data;
using CS499.TCMS.Model;

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

        public DataTable GetPayrollReport(DateTime startDate, DateTime endDate)
        {

            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT payroll.EmployeeID AS 'Employee ID', payroll.PaymentDate AS 'Payment Date', payroll.Payment, payroll.HoursWorked AS 'Hours Worked', " +
                              "user.FirstName AS 'First Name', user.MiddleName AS 'Middle Name', user.LastName AS 'Last Name', user.Address, user.City, user.State, " +
                              "user.ZipCode AS 'Zip Code', user.PayRate AS 'Pay Rate', user.HomeStore AS 'Home Store', user.JobDescription AS 'Job Description' " +
                              "FROM payroll " +
                              "INNER JOIN user ON payroll.EmployeeID = user.EmployeeID " +
                              "WHERE PaymentDate BETWEEN ? and ? " +
                              "ORDER BY user.EmployeeID",
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
                Value = startDate
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_CurrentDate",
                Type = DbType.DateTime,
                Value = endDate
            });

            return this.Database.ExecuteDataTableQuery(definition);
        }

        public DataTable GetVehicleMaintenanceReport(Vehicle model)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT vehicle.VehicleID AS 'Vehicle ID', vehicle.Brand, vehicle.Year, vehicle.Model, vehicle.VehicleType AS 'Vehicle Type', " +
                              "vehicle.Capacity, maintenancerecord.MaintenanceDate AS 'Maintenance Date', maintenancerecord.MaintenanceDescription AS 'Maintenance Description', " +
                              "maintenancerecorddetails.EmployeeID AS 'Employee ID', maintenancerecorddetails.RepairDescription AS 'Repair Description', " +
                              "maintenancerecorddetails.RepairDate AS 'Repair Date', maintenancepart.Quantity, parts.PartDescription AS 'Part Description', " +
                              "parts.PartNumber AS 'Part Number', parts.PartPrice AS 'Part Price', parts.PartWeight AS 'Part Weight', " +
                              "user.FirstName AS 'First Name', user.MiddleName AS 'Middle Name', user.LastName AS 'Last Name', user.HomeStore AS 'Home Store', " +
                              "user.JobDescription AS 'Job Description' " +
                              "FROM maintenancerecorddetails " +
                              "INNER JOIN user ON maintenancerecorddetails.EmployeeID = user.EmployeeID " +
                              "INNER JOIN maintenancerecord ON maintenancerecorddetails.MaintenanceID = maintenancerecord.MaintenanceID " +
                              "INNER JOIN vehicle ON maintenancerecord.VehicleID = vehicle.VehicleID " +
                              "INNER JOIN maintenancepart ON maintenancepart.MaintenanceRecordID = maintenancerecorddetails.DetailID " +
                              "INNER JOIN parts ON maintenancepart.PartID = parts.PartID " + 
                              "WHERE vehicle.VehicleID = ? ",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_VehicleID",
                Type = DbType.Int64,
                Value = model.VehicleID
            });

            return this.Database.ExecuteDataTableQuery(definition);
        }

        protected override DataTable Map(IDataReader reader)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
