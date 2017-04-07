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
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT manifest.ManifestID AS 'Manifest ID', manifest.ShipmentType AS 'Shipment Type', manifest.VehicleID AS 'Vehicle ID', " +
                              "manifest.DepartureTime AS 'Departure Time', manifest.ETA, manifest.Arrived, manifest.ShippingCost AS 'Shipping Cost', " +
                              "purchaseorder.OrderNumber AS 'Order Number', businesspartners.CompanyName AS 'Company Name', businesspartners.Address, " +
                              "businesspartners.City, businesspartners.State, businesspartners.ZipCode AS 'Zip Code', businesspartners.PhoneNumber AS 'Phone Number', " +
                              "purchaseorder.PaymentMade AS 'Payment Made', purchaseitems.Quantity, parts.PartNumber AS 'Part Number', purchaseitems.Quantity * parts.PartPrice AS 'Total Part Price', " +
                              "purchaseitems.Quantity * parts.PartWeight AS 'Total Part Weight', purchaseitems.PartStatus AS 'Part Status', " +
                              "user.EmployeeID AS 'Employee ID', user.FirstName AS 'First Name', user.MiddleName AS 'Middle Name', user.LastName AS 'Last Name'" +
                              "FROM purchaseitems " +
                              "INNER JOIN parts ON purchaseitems.PartID = parts.PartID " +
                              "INNER JOIN purchaseorder ON purchaseitems.OrderID = purchaseorder.OrderID " +
                              "INNER JOIN businesspartners ON purchaseorder.SourceID = businesspartners.CompanyID AND purchaseorder.DestinationID = businesspartners.CompanyID " +
                              "INNER JOIN manifest ON purchaseorder.ManifestID = manifest.ManifestID " +
                              "INNER JOIN user ON manifest.EmployeeID = user.EmployeeID " +
                              "WHERE manifest.ShipmentType = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            //Create parameter
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ShipmentType",
                Type = DbType.String,
                Value = "Incoming"
            });

            return this.Database.ExecuteDataTableQuery(definition);
        }

        public DataTable GetMaintenanceCostReport(DateTime startDate, DateTime endDate)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT vehicle.VehicleID AS 'Vehicle ID', vehicle.Brand, vehicle.Year, vehicle.Model, vehicle.VehicleType AS 'Vehicle Type', " +
                              "vehicle.Capacity, maintenancerecord.MaintenanceDate AS 'Maintenance Date', maintenancerecord.MaintenanceDescription AS 'Maintenance Description', " +
                              "maintenancerecorddetails.EmployeeID AS 'Employee ID', maintenancerecorddetails.RepairDescription AS 'Repair Description', " +
                              "maintenancerecorddetails.RepairDate AS 'Repair Date', maintenancepart.Quantity, parts.PartDescription AS 'Part Description', " +
                              "parts.PartNumber AS 'Part Number', maintenancepart.Quantity * parts.PartPrice AS 'Total Part Price', maintenancepart.Quantity * parts.PartWeight AS 'Total Part Weight', " +
                              "user.FirstName AS 'First Name', user.MiddleName AS 'Middle Name', user.LastName AS 'Last Name', user.HomeStore AS 'Home Store', " +
                              "user.JobDescription AS 'Job Description' " +
                              "FROM maintenancerecorddetails " +
                              "INNER JOIN user ON maintenancerecorddetails.EmployeeID = user.EmployeeID " +
                              "INNER JOIN maintenancerecord ON maintenancerecorddetails.MaintenanceID = maintenancerecord.MaintenanceID " +
                              "INNER JOIN vehicle ON maintenancerecord.VehicleID = vehicle.VehicleID " +
                              "INNER JOIN maintenancepart ON maintenancepart.MaintenanceRecordID = maintenancerecorddetails.DetailID " +
                              "INNER JOIN parts ON maintenancepart.PartID = parts.PartID " +
                              "WHERE maintenancerecord.MaintenanceDate BETWEEN ? and ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_startDate",
                Type = DbType.DateTime,
                Value = startDate
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_endDate",
                Type = DbType.DateTime,
                Value = endDate
            });

            return this.Database.ExecuteDataTableQuery(definition);
        }

        public DataTable GetOutgoingShipmentReport()
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT manifest.ManifestID AS 'Manifest ID', manifest.ShipmentType AS 'Shipment Type', manifest.VehicleID AS 'Vehicle ID', " +
                              "manifest.DepartureTime AS 'Departure Time', manifest.ETA, manifest.Arrived, manifest.ShippingCost AS 'Shipping Cost', " +
                              "purchaseorder.OrderNumber AS 'Order Number', businesspartners.CompanyName AS 'Company Name', businesspartners.Address, " +
                              "businesspartners.City, businesspartners.State, businesspartners.ZipCode AS 'Zip Code', businesspartners.PhoneNumber AS 'Phone Number', " +
                              "purchaseorder.PaymentMade AS 'Payment Made', purchaseitems.Quantity, parts.PartNumber AS 'Part Number', purchaseitems.Quantity * parts.PartPrice AS 'Total Part Price', " +
                              "purchaseitems.Quantity * parts.PartWeight AS 'Total Part Weight', purchaseitems.PartStatus AS 'Part Status', " +
                              "user.EmployeeID AS 'Employee ID', user.FirstName AS 'First Name', user.MiddleName AS 'Middle Name', user.LastName AS 'Last Name'" +
                              "FROM purchaseitems " +
                              "INNER JOIN parts ON purchaseitems.PartID = parts.PartID " +
                              "INNER JOIN purchaseorder ON purchaseitems.OrderID = purchaseorder.OrderID " +
                              "INNER JOIN businesspartners ON purchaseorder.SourceID = businesspartners.CompanyID AND purchaseorder.DestinationID = businesspartners.CompanyID " +
                              "INNER JOIN manifest ON purchaseorder.ManifestID = manifest.ManifestID " +
                              "INNER JOIN user ON manifest.EmployeeID = user.EmployeeID " +  
                              "WHERE manifest.ShipmentType = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            //Create parameter
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ShipmentType",
                Type = DbType.String,
                Value = "Outgoing"
            });

            return this.Database.ExecuteDataTableQuery(definition);
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
                Name = "P_startDate",
                Type = DbType.DateTime,
                Value = startDate
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_endDate",
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
                              "parts.PartNumber AS 'Part Number', maintenancepart.Quantity * parts.PartPrice AS 'Total Part Price', maintenancepart.Quantity * parts.PartWeight AS 'Total Part Weight', " +
                              "user.FirstName AS 'First Name', user.MiddleName AS 'Middle Name', user.LastName AS 'Last Name', user.HomeStore AS 'Home Store', " +
                              "user.JobDescription AS 'Job Description' " +
                              "FROM maintenancerecorddetails " +
                              "INNER JOIN user ON maintenancerecorddetails.EmployeeID = user.EmployeeID " +
                              "INNER JOIN maintenancerecord ON maintenancerecorddetails.MaintenanceID = maintenancerecord.MaintenanceID " +
                              "INNER JOIN vehicle ON maintenancerecord.VehicleID = vehicle.VehicleID " +
                              "INNER JOIN maintenancepart ON maintenancepart.MaintenanceRecordID = maintenancerecorddetails.DetailID " +
                              "INNER JOIN parts ON maintenancepart.PartID = parts.PartID " + 
                              "WHERE vehicle.VehicleID = ?",
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
