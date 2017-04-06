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
                CommandText = "SELECT vehicle.VehicleID, vehicle.Brand, vehicle.Year, vehicle.Model, vehicle.VehicleType, vehicle.Capacity, " +
                              "maintenancerecord.MaintenanceDate, maintenancerecord.MaintenanceDescription, maintenancerecorddetails.EmployeeID, " +
                              "maintenancerecorddetails.RepairDescription, maintenancerecorddetails.RepairDate, maintenancepart.Quantity, " +
                              "parts.PartDescription, parts.PartNumber, parts.PartPrice, parts.PartWeight, user.FirstName, user.MiddleName, user.LastName, " +
                              "user.HomeStore, user.JobDescription " +
                              "FROM maintenancerecorddetails " +
                              "INNER JOIN user ON maintenancerecorddetails.EmployeeID = user.EmployeeID " +
                              "INNER JOIN maintenancerecord ON maintenancerecorddetails.MaintenanceID = maintenancerecord.MaintenanceID " +
                              "INNER JOIN vehicle ON maintenancerecord.VehicleID = vehicle.VehicleID " +
                              "INNER JOIN maintenancepart ON maintenancepart.MaintenanceRecordID = maintenancerecorddetails.DetailID " +
                              "INNER JOIN parts ON maintenancepart.PartID = parts.PartID " + 
                              "WHERE vehicle.VehicleID = ? " +
                              "ORDER BY VehicleID",
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
