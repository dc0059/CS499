//STILL NEED TO VERIFY QUERIES AND DATABASE TABLE NAMES, COLUMN NAMES, ETC
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.DataAccess.IRepositories;
using ToolKit.Data;
using CS499.TCMS.Model;
using System.Data;

namespace CS499.TCMS.DataAccess.Repositories
{
    class ManifestRepository : GenericRepository<Manifest>, IManifestRepository
    {
        #region Constructor
        public ManifestRepository(IDatabase database) : base(database)
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// Deletes the argument Manifest object from the database
        /// </summary>
        /// <param name="model"></param>
        void IRepository<Manifest>.Delete(Manifest model)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM manifest " +
                              "WHERE ManifestID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ManifestID",
                Type = DbType.Int64,
                Value = model.ManifestID
            });

            this.Database.ExecuteModQuery(definition);

            // Create query definition
            definition = new QueryDefinition()
            {
                CommandText = "UPDATE manifest_log " +
                              "SET DeletedBy = ? " +
                              "WHERE ManifestID = ? " +
                              "AND ModifiedStatus = 'D'",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition            
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_User",
                Type = DbType.String,
                Value = this.Database.UserName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ID",
                Type = DbType.Int64,
                Value = model.ManifestID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Delete Manifest in Database with matching ID to the argument
        /// </summary>
        /// <param name="ManifestID"></param>
        void IManifestRepository.Delete(long ManifestID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM manifest " +
                              "WHERE ManifestID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ManifestID",
                Type = DbType.Int64,
                Value = ManifestID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Delete Manifest in the database with matching EmployeeID number
        /// </summary>
        /// <param name="EmployeeID"></param>
        void IManifestRepository.DeleteByEmployee(long EmployeeID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM manifest " +
                              "WHERE EmployeeID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_EmployeeID",
                Type = DbType.Int64,
                Value = EmployeeID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Returns a collection of every unique Manifest
        /// </summary>
        /// <returns></returns>
        IEnumerable<Manifest> IRepository<Manifest>.GetAll()
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT man.ManifestID, ShipmentType, VehicleID, DepartureTime, ETA, Arrived, EmployeeID, " +
                              "(SELECT SUM(pt.PartPrice * pi.Quantity) " +
                              "FROM ((purchaseorder p " +
                              "INNER JOIN manifest m ON (p.ManifestID = m.ManifestID)) " +
                              "INNER JOIN purchaseitems pi ON (pi.OrderID = p.OrderID)) " +
                              "INNER JOIN parts pt ON (pi.PartID = pt.PartID) " +
                              "WHERE m.ManifestID = man.ManifestID ) AS ShippingCost " +
                              "FROM manifest man " +
                              "ORDER BY ManifestID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            return this.Database.ExecuteListQuery<Manifest>(definition, Map);
        }


        /// <summary>
        /// Returns a collection of manifests assigned to specific employee
        /// </summary>
        /// <param name="EmployeeID">ID of employee</param>
        /// <returns>collection of Manifests belonging to the argument employeeID</returns>
        IEnumerable<Manifest> IManifestRepository.GetAllByEmployee(long EmployeeID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT man.ManifestID, ShipmentType, VehicleID, DepartureTime, ETA, Arrived, EmployeeID, " +
                              "(SELECT SUM(pt.PartPrice * pi.Quantity) " +
                              "FROM ((purchaseorder p " +
                              "INNER JOIN manifest m ON (p.ManifestID = m.ManifestID)) " +
                              "INNER JOIN purchaseitems pi ON (pi.OrderID = p.OrderID)) " +
                              "INNER JOIN parts pt ON (pi.PartID = pt.PartID) " +
                              "WHERE m.ManifestID = man.ManifestID ) AS ShippingCost " +
                              "FROM manifest man " +
                              "WHERE EmployeeID = ? ",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_EmployeeID",
                Type = DbType.Int64,
                Value = EmployeeID
            });

            return this.Database.ExecuteListQuery<Manifest>(definition, Map);
        }


        /// <summary>
        /// Gets a collection of every manifest of incoming or outcoming type. But not both.
        /// </summary>
        /// <param name="type">values 'incoming' or 'outgoing'</param>
        /// <returns>collection of manifests of the same shipment type</returns>
        IEnumerable<Manifest> IManifestRepository.GetAllByIncOrOut(string type)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT man.ManifestID, ShipmentType, VehicleID, DepartureTime, ETA, Arrived, EmployeeID, " +
                              "(SELECT SUM(pt.PartPrice * pi.Quantity) " +
                              "FROM ((purchaseorder p " +
                              "INNER JOIN manifest m ON (p.ManifestID = m.ManifestID)) " +
                              "INNER JOIN purchaseitems pi ON (pi.OrderID = p.OrderID)) " +
                              "INNER JOIN parts pt ON (pi.PartID = pt.PartID) " +
                              "WHERE m.ManifestID = man.ManifestID ) AS ShippingCost " +
                              "FROM manifest man " +
                              "WHERE ShipmentType = ? ",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ShipmentType",
                Type = DbType.String,
                Value = type
            });

            return this.Database.ExecuteListQuery<Manifest>(definition, Map);
        }


        /// <summary>
        /// Returns a collection of manifests set to arrive on the argument date
        /// </summary>
        /// <param name="ArrivalDate">DateTime object specifying search date</param>
        /// <returns>collection of manifests arriving on argument date</returns>
        IEnumerable<Manifest> IManifestRepository.GetManifestByArrivalDate(DateTime ArrivalDate)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT man.ManifestID, ShipmentType, VehicleID, DepartureTime, ETA, Arrived, EmployeeID, " +
                              "(SELECT SUM(pt.PartPrice * pi.Quantity) " +
                              "FROM ((purchaseorder p " +
                              "INNER JOIN manifest m ON (p.ManifestID = m.ManifestID)) " +
                              "INNER JOIN purchaseitems pi ON (pi.OrderID = p.OrderID)) " +
                              "INNER JOIN parts pt ON (pi.PartID = pt.PartID) " +
                              "WHERE m.ManifestID = man.ManifestID ) AS ShippingCost " +
                              "FROM manifest man " +
                              "WHERE ETA = ? ",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ArrivalDate",
                Type = DbType.DateTime,
                Value = ArrivalDate
            });

            return this.Database.ExecuteListQuery<Manifest>(definition, Map);
        }


        /// <summary>
        /// Returns a collection of Manifests set to depart on the date argument
        /// </summary>
        /// <param name="DepartureTime">DateTime object of departure date</param>
        /// <returns></returns>
        IEnumerable<Manifest> IManifestRepository.GetManifestByDepartureDate(DateTime DepartureTime)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT man.ManifestID, ShipmentType, VehicleID, DepartureTime, ETA, Arrived, EmployeeID, " +
                              "(SELECT SUM(pt.PartPrice * pi.Quantity) " +
                              "FROM ((purchaseorder p " +
                              "INNER JOIN manifest m ON (p.ManifestID = m.ManifestID)) " +
                              "INNER JOIN purchaseitems pi ON (pi.OrderID = p.OrderID)) " +
                              "INNER JOIN parts pt ON (pi.PartID = pt.PartID) " +
                              "WHERE m.ManifestID = man.ManifestID ) AS ShippingCost " +
                              "FROM manifest man " +
                              "WHERE DepartureTime = ? " +
                              "ORDER BY VehicleID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DepartureTime",
                Type = DbType.DateTime,
                Value = DepartureTime
            });

            return this.Database.ExecuteListQuery<Manifest>(definition, Map);
        }


        /// <summary>
        /// Return a single manifest object corresponding to argument object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Manifest IRepository<Manifest>.GetSingle(object id)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT man.ManifestID, ShipmentType, VehicleID, DepartureTime, ETA, Arrived, EmployeeID, " +
                              "(SELECT SUM(pt.PartPrice * pi.Quantity) " +
                              "FROM ((purchaseorder p " +
                              "INNER JOIN manifest m ON (p.ManifestID = m.ManifestID)) " +
                              "INNER JOIN purchaseitems pi ON (pi.OrderID = p.OrderID)) " +
                              "INNER JOIN parts pt ON (pi.PartID = pt.PartID) " +
                              "WHERE m.ManifestID = man.ManifestID ) AS ShippingCost " +
                              "FROM manifest man " +
                              "WHERE man.ManifestID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ManifestID",
                Type = DbType.Int64,
                Value = id
            });

            return this.Database.ExecuteSingleQuery<Manifest>(definition, Map);
        }


        /// <summary>
        /// Return a manifest object corresponding to the ManifestID argument
        /// </summary>
        /// <param name="ManifestID"></param>
        /// <returns>manifest object form Database with given manifestID</returns>
        /*Manifest IManifestRepository.GetSingle(long ManifestID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT ManifestID, ShipmentType, VehicleID, DepartureTime, ETA, Arrived, ShippingCost, EmployeeID " +
                              "FROM manifest " +
                              "WHERE ManifestID = ? ",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ManifestID",
                Type = DbType.Int64,
                Value = ManifestID
            });

            return this.Database.ExecuteSingleQuery<Manifest>(definition, Map);
        }*/


        /// <summary>
        /// Inserts a new object into the manifest database table
        /// </summary>
        /// <param name="model"></param>
        void IRepository<Manifest>.Insert(Manifest model)
        {

            long id;
            
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "INSERT INTO manifest (ShipmentType, VehicleID, DepartureTime, ETA, Arrived, ShippingCost, EmployeeID, " +
                              "CreatedBy, LastModifiedBy ) " +
                              "VALUES (?,?,?,?,?,?,?,?,?)",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ShipmentType",
                Type = DbType.String,
                Value = model.ShipmentType
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_VehicleID",
                Type = DbType.Int64,
                Value = model.VehicleID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DepartureTime",
                Type = DbType.DateTime,
                Value = model.DepartureTime
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ETA",
                Type = DbType.DateTime,
                Value = model.ETA
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Arrived",
                Type = DbType.Boolean,
                Value = model.Arrived
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ShippingCost",
                Type = DbType.Double,
                Value = model.ShippingCost
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_EmployeeID",
                Type = DbType.Int64,
                Value = model.EmployeeID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_CreatedBy",
                Type = DbType.String,
                Value = this.Database.UserName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_LastModifiedBy",
                Type = DbType.String,
                Value = this.Database.UserName
            });
            this.Database.ExecuteModQuery(definition, out id);

            model.ManifestID = id;
        }


        /// <summary>
        /// Finds and updates the argument Manifest in the Database
        /// </summary>
        /// <param name="model"></param>
        void IRepository<Manifest>.Update(Manifest model)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "UPDATE manifest " +
                              "SET ShipmentType = ?, VehicleID = ?, DepartureTime = ?, ETA = ?, Arrived = ?, ShippingCost = ?, EmployeeID = ?, " +
                              "LastModifiedBy = ? " +
                              "WHERE ManifestID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ShipmentType",
                Type = DbType.String,
                Value = model.ShipmentType
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_VehicleID",
                Type = DbType.Int64,
                Value = model.VehicleID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DepartureTime",
                Type = DbType.DateTime,
                Value = model.DepartureTime
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ETA",
                Type = DbType.DateTime,
                Value = model.ETA
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "Arrived",
                Type = DbType.Boolean,
                Value = model.Arrived
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ShippingCost",
                Type = DbType.Double,
                Value = model.ShippingCost
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_EmployeeID",
                Type = DbType.Int64,
                Value = model.EmployeeID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_LastModifiedBy",
                Type = DbType.String,
                Value = this.Database.UserName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Manifest",
                Type = DbType.Int64,
                Value = model.ManifestID
            });
            this.Database.ExecuteModQuery(definition);
        }


        protected override Manifest Map(IDataReader reader)
        {
            return new Manifest(reader.GetValueOrDefault<long>("ManifestID"),
                reader.GetValueOrDefault<string>("ShipmentType"),
                reader.GetValueOrDefault<long>("VehicleID"),
                reader.GetValueOrDefault<DateTime>("DepartureTime"),
                reader.GetValueOrDefault<DateTime>("ETA"),
                reader.GetValueOrDefault<bool>("Arrived"),
                reader.GetValueOrDefault<double>("ShippingCost"),
                reader.GetValueOrDefault<long>("EmployeeID"));
        }

        /// <summary>
        /// Gets the manifest by employee identifier.
        /// </summary>
        /// <param name="employeeID">The employee identifier.</param>
        /// <returns>
        /// a <see cref="DataTable" /> containing the manifest information
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        DataTable IManifestRepository.GetManifestByEmployeeID(long employeeID)
        {

            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT m.ManifestID AS `Manifest ID`, m.DepartureTime AS `Departure Time`, m.ETA, po.OrderNumber AS `Order Number`, " +
                              "(SELECT ba.Address FROM businesspartners_addresses ba WHERE ba.CompanyID = po.SourceID)  AS `Source Address`, " +
                              "(SELECT ba.Address FROM businesspartners_addresses ba WHERE ba.CompanyID = po.DestinationID )  AS `Destination Address`, " +
                              "pi.PartStatus AS `Part Status`, pi.Quantity, p.PartNumber AS `Part Number`, p.PartDescription AS `Part Description`, " +
                              "pi.Quantity * p.PartPrice  AS `Total Part Price`, pi.Quantity * p.PartWeight AS `Total Part Weight` " +
                              "FROM ((cs_499_tcms.purchaseitems pi " +
                              "INNER JOIN cs_499_tcms.parts p ON (pi.PartID = p.PartID)) " +
                              "INNER JOIN cs_499_tcms.purchaseorder po ON (pi.OrderID = po.OrderID)) " +
                              "INNER JOIN cs_499_tcms.manifest m ON (po.ManifestID = m.ManifestID) " +
                              "WHERE m.Arrived = 0 " +
                              "AND m.EmployeeID = ? " +
                              "ORDER BY `Manifest ID` ASC, `Order Number` ASC, `Part Number` ASC",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_EmployeeID",
                Type = DbType.Int64,
                Value = employeeID
            });

            return this.Database.ExecuteDataTableQuery(definition);

        }

        #endregion

    }

}
