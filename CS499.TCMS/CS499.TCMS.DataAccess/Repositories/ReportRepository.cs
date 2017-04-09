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
                CommandText = "SELECT * FROM (SELECT m.ManifestID AS `Manifest ID`, m.ShipmentType AS `Shipment Type`, m.DepartureTime AS `Departure Time`, m.ETA, " +
                              "m.Arrived, po.OrderNumber AS `Order Number`, s.Address AS `Source Address`, d.Address AS `Destination Address`, " +
                              "p.PartNumber AS `Part Number`, p.PartDescription AS `Part Description`, pi.PartStatus AS `Part Status`, pi.Quantity, " +
                              "p.PartPrice AS `Part Price`, SUM(pi.Quantity * p.PartPrice) AS `Total Cost` " +
                              "FROM ((((purchaseorder po " +
                              "INNER JOIN businesspartners_addresses s ON (po.SourceID = s.CompanyID)) " +
                              "INNER JOIN manifest m ON (po.ManifestID = m.ManifestID)) " +
                              "INNER JOIN purchaseitems pi ON (pi.OrderID = po.OrderID)) " +
                              "INNER JOIN parts p ON (pi.PartID = p.PartID)) " +
                              "INNER JOIN businesspartners_addresses d ON (po.DestinationID = d.CompanyID) " +
                              "WHERE m.ShipmentType = ? " +
                              "GROUP BY m.ManifestID, m.ShipmentType, m.DepartureTime, m.ETA, m.Arrived, po.OrderNumber, " +
                              "s.Address, d.Address, p.PartNumber, p.PartDescription, pi.PartStatus, pi.Quantity, p.PartPrice " +
                              "UNION " +
                              "SELECT m.ManifestID AS `Manifest ID`, NULL AS `Shipment Type`, NULL AS `Departure Time`, NULL ETA, NULL Arrived, NULL AS `Order Number`, " +
                              "NULL AS `Source Address`, NULL AS `Destination Address`, NULL AS `Part Number`, NULL AS `Part Description`, " +
                              "NULL AS `Part Status`, NULL AS Quantity, 'Total:' AS `Part Price`, SUM(pi.Quantity * p.PartPrice) AS `Total Cost` " +
                              "FROM ((((purchaseorder po " +
                              "INNER JOIN businesspartners_addresses s ON (po.SourceID = s.CompanyID)) " +
                              "INNER JOIN manifest m ON (po.ManifestID = m.ManifestID)) " +
                              "INNER JOIN purchaseitems pi ON (pi.OrderID = po.OrderID)) " +
                              "INNER JOIN parts p ON (pi.PartID = p.PartID)) " +
                              "INNER JOIN businesspartners_addresses d ON (po.DestinationID = d.CompanyID) " +
                              "WHERE m.ShipmentType = ? " +
                              "GROUP BY m.ManifestID) T1 " +
                              "ORDER BY `Manifest ID`, ISNULL(`Order Number`), `Order Number`, ISNULL(`Part Number`), `Part Number`",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            //Create parameter
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ShipmentType1",
                Type = DbType.String,
                Value = "Incoming"
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ShipmentType2",
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
                CommandText = "SELECT maintenancerecord.MaintenanceDate AS 'Maintenance Date', maintenancerecord.MaintenanceDescription AS 'Maintenance Description', vehicle_info.VehicleID AS 'Vehicle ID', vehicle_info.Vehicle, " +
                              "maintenancepart.Quantity, parts.PartNumber AS 'Part Number', parts.PartDescription AS 'Part Description', parts.PartPrice AS 'Part Price', " +
                              "sum(parts.PartPrice * maintenancepart.Quantity) AS 'Total Cost' " +
                              "FROM (((maintenancerecorddetails maintenancerecorddetails " +
                              "INNER JOIN maintenancerecord maintenancerecord ON (maintenancerecorddetails.MaintenanceID = maintenancerecord.MaintenanceID)) " +
                              "INNER JOIN maintenancepart maintenancepart ON (maintenancepart.MaintenanceRecordID = maintenancerecorddetails.DetailID)) " +
                              "INNER JOIN parts parts ON (maintenancepart.PartID = parts.PartID)) " +
                              "INNER JOIN vehicle_info vehicle_info ON (vehicle_info.VehicleID = maintenancerecord.VehicleID) " +
                              "WHERE maintenancerecord.MaintenanceDate BETWEEN ? and ? " +
                              "GROUP BY  maintenancerecord.MaintenanceDate, maintenancerecord.MaintenanceDescription, vehicle_info.VehicleID, vehicle_info.Vehicle, maintenancepart.Quantity, parts.PartNumber, parts.PartDescription, parts.PartPrice " +
                              "Union " +
                              "SELECT '' AS 'Maintenance Date', '' AS 'Maintenance Description', '' AS 'Vehicle ID', '' AS Vehicle, '' AS Quantity, '' AS 'Part Number', '' AS 'Part Description', 'Total:' AS 'Part Price', sum(parts.PartPrice * maintenancepart.Quantity) AS 'Total Cost' " +
                              "FROM (((maintenancerecorddetails maintenancerecorddetails " +
                              "INNER JOIN maintenancerecord maintenancerecord ON (maintenancerecorddetails.MaintenanceID = maintenancerecord.MaintenanceID)) " +
                              "INNER JOIN maintenancepart maintenancepart ON (maintenancepart.MaintenanceRecordID = maintenancerecorddetails.DetailID)) " +
                              "INNER JOIN parts parts ON (maintenancepart.PartID = parts.PartID)) " +
                              "INNER JOIN vehicle_info vehicle_info ON (vehicle_info.VehicleID = maintenancerecord.VehicleID) " +
                              "WHERE maintenancerecord.MaintenanceDate BETWEEN ? and ?",

                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_startDate1",
                Type = DbType.DateTime,
                Value = startDate
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_endDate1",
                Type = DbType.DateTime,
                Value = endDate
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_startDate2",
                Type = DbType.DateTime,
                Value = startDate
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_endDate2",
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
                CommandText = "SELECT * FROM (SELECT m.ManifestID AS `Manifest ID`, m.ShipmentType AS `Shipment Type`, m.DepartureTime AS `Departure Time`, m.ETA, " +
                              "m.Arrived, po.OrderNumber AS `Order Number`, s.Address AS `Source Address`, d.Address AS `Destination Address`, " +
                              "p.PartNumber AS `Part Number`, p.PartDescription AS `Part Description`, pi.PartStatus AS `Part Status`, pi.Quantity, " +
                              "p.PartPrice AS `Part Price`, SUM(pi.Quantity * p.PartPrice) AS `Total Cost` " +
                              "FROM ((((purchaseorder po " +
                              "INNER JOIN businesspartners_addresses s ON (po.SourceID = s.CompanyID)) " +
                              "INNER JOIN manifest m ON (po.ManifestID = m.ManifestID)) " +
                              "INNER JOIN purchaseitems pi ON (pi.OrderID = po.OrderID)) " +
                              "INNER JOIN parts p ON (pi.PartID = p.PartID)) " +
                              "INNER JOIN businesspartners_addresses d ON (po.DestinationID = d.CompanyID) " +
                              "WHERE m.ShipmentType = ? " +
                              "GROUP BY m.ManifestID, m.ShipmentType, m.DepartureTime, m.ETA, m.Arrived, po.OrderNumber, " +
                              "s.Address, d.Address, p.PartNumber, p.PartDescription, pi.PartStatus, pi.Quantity, p.PartPrice " +
                              "UNION " +
                              "SELECT m.ManifestID AS `Manifest ID`, NULL AS `Shipment Type`, NULL AS `Departure Time`, NULL ETA, NULL Arrived, NULL AS `Order Number`, " +
                              "NULL AS `Source Address`, NULL AS `Destination Address`, NULL AS `Part Number`, NULL AS `Part Description`, " +
                              "NULL AS `Part Status`, NULL AS Quantity, 'Total:' AS `Part Price`, SUM(pi.Quantity * p.PartPrice) AS `Total Cost` " +
                              "FROM ((((purchaseorder po " +
                              "INNER JOIN businesspartners_addresses s ON (po.SourceID = s.CompanyID)) " +
                              "INNER JOIN manifest m ON (po.ManifestID = m.ManifestID)) " +
                              "INNER JOIN purchaseitems pi ON (pi.OrderID = po.OrderID)) " +
                              "INNER JOIN parts p ON (pi.PartID = p.PartID)) " +
                              "INNER JOIN businesspartners_addresses d ON (po.DestinationID = d.CompanyID) " +
                              "WHERE m.ShipmentType = ? " +
                              "GROUP BY m.ManifestID) T1 " +
                              "ORDER BY `Manifest ID`, ISNULL(`Order Number`), `Order Number`, ISNULL(`Part Number`), `Part Number`",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            //Create parameter
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ShipmentType1",
                Type = DbType.String,
                Value = "Outgoing"
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ShipmentType2",
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
