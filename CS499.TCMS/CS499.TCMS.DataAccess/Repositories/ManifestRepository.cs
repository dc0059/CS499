﻿//STILL NEED TO VERIFY QUERIES AND DATABASE TABLE NAMES, COLUMN NAMES, ETC
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
        IEnumerable<Manifest> IRepository<Manifest>.getAll()
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT ManifestID, ShipmentType, VehicleID, DepartureTime, ETA, Arrived, ShippingCost, EmployeeID " +
                              "FROM manifest " +
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
        IEnumerable<Manifest> IManifestRepository.getAllByEmployee(long EmployeeID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT ManifestID, ShipmentType, VehicleID, DepartureTime, ETA, Arrived, ShippingCost, EmployeeID " +
                              "FROM manifest " +
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
        IEnumerable<Manifest> IManifestRepository.getAllByIncOrOut(string type)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT ManifestID, ShipmentType, VehicleID, DepartureTime, ETA, Arrived, ShippingCost, EmployeeID " +
                              "FROM manifest " +
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
        IEnumerable<Manifest> IManifestRepository.getManifestByArrivalDate(DateTime ArrivalDate)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT ManifestID, ShipmentType, VehicleID, DepartureTime, ETA, Arrived, ShippingCost, EmployeeID " +
                              "FROM manifest " +
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
        IEnumerable<Manifest> IManifestRepository.getManifestByDepartureDate(DateTime DepartureTime)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT ManifestID, ShipmentType, VehicleID, DepartureTime, ETA, Arrived, ShippingCost, EmployeeID " +
                              "FROM manifest " +
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
        Manifest IRepository<Manifest>.getSingle(object id)
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
                Value = id
            });

            return this.Database.ExecuteSingleQuery<Manifest>(definition, Map);
        }


        /// <summary>
        /// Return a manifest object corresponding to the ManifestID argument
        /// </summary>
        /// <param name="ManifestID"></param>
        /// <returns>manifest object form Database with given manifestID</returns>
        Manifest IManifestRepository.getSingle(long ManifestID)
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
        }


        /// <summary>
        /// Inserts a new object into the manifest database table
        /// </summary>
        /// <param name="model"></param>
        void IRepository<Manifest>.Insert(Manifest model)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "INSERT INTO user (ManifestID, ShipmentType, VehicleID, DepartureTime, ETA, Arrived, ShippingCost, EmployeeID) " +
                              "VALUES (?,?,?,?,?,?,?,?)",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ManifestID",
                Type = DbType.Int64,
                Value = model.ManifestID
            });
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
            this.Database.ExecuteModQuery(definition);
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
                              "SET ShipmentType = ?, VehicleID = ?, DepartureTime = ?, ETA = ?, Arrived = ?, ShippingCost = ?, EmployeeID = ? " +
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
                Name = "P_Manifest",
                Type = DbType.Int64,
                Value = model.ManifestID
            });
            this.Database.ExecuteModQuery(definition);
        }


        protected override Manifest Map(IDataReader reader)
        {
            return new Manifest(reader.GetValueOrDefault<Int64>("ManifestID"),
                reader.GetValueOrDefault<string>("ShipmentType"),
                reader.GetValueOrDefault<Int64>("VehicleID"),
                reader.GetValueOrDefault<DateTime>("DepartureTime"),
                reader.GetValueOrDefault<DateTime>("ETA"),
                reader.GetValueOrDefault<bool>("Arrived"),
                reader.GetValueOrDefault<Int64>("ShippingCost"),
                reader.GetValueOrDefault<Int64>("EmployeeID"));
        }
    }

    #endregion

}