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
    class MaintenanceRecordRepository : GenericRepository<MaintenanceRecord>, IMaintenanceRecordRepository
    {

        #region Constructor
        public MaintenanceRecordRepository(IDatabase database) : base(database)
        {
        }
        #endregion


        /// <summary>
        /// Deletes the Maintenance record in the database with the same MaintenanceID as the argument object
        /// </summary>
        /// <param name="model"></param>
        void IRepository<MaintenanceRecord>.Delete(MaintenanceRecord model)
        {

            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM maintenancerecord " +
                              "WHERE MaintenanceID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenanceID",
                Type = DbType.Int64,
                Value = model.MaintenanceID
            });

            this.Database.ExecuteModQuery(definition);

            // Create query definition
            definition = new QueryDefinition()
            {
                CommandText = "UPDATE maintenancerecord_log " +
                              "SET DeletedBy = ? " +
                              "WHERE MaintenanceID = ? " +
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
                Value = model.MaintenanceID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Deletes the maintenance Record in the database with the same argument value for the RecordID
        /// </summary>
        /// <param name="RecordID"></param>
        void IMaintenanceRecordRepository.Delete(long RecordID)
        {

            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM maintenancerecord " +
                              "WHERE MaintenanceID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenanceID",
                Type = DbType.Int64,
                Value = RecordID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Deletes all MaintenanceRecords in the database that are marked with a date between the argument dates.
        /// </summary>
        /// <param name="Earliest"></param>
        /// <param name="Latest"></param>
        void IMaintenanceRecordRepository.DeleteByDate(DateTime Earliest, DateTime Latest)
        {

            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM maintenancerecord " +
                              "WHERE MaintenanceDate => ? " +
                              "AND MaintenanceDate <= ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Earliest",
                Type = DbType.DateTime,
                Value = Earliest
            });
            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Latest",
                Type = DbType.DateTime,
                Value = Latest
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Deletes a all records in the database with the argument's value as vehicleID
        /// </summary>
        /// <param name="VehicleID"></param>
        void IMaintenanceRecordRepository.DeleteByVehicle(long VehicleID)
        {

            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM maintenancerecord " +
                              "WHERE VehicleID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_VehicleID",
                Type = DbType.Int64,
                Value = VehicleID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Deletes every record that falls within the date and corresponds to the vehicleID
        /// QUERY NEEDS CHECKING
        /// </summary>
        /// <param name="VehicleID"></param>
        /// <param name="Earliest"></param>
        /// <param name="Latest"></param>
        void IMaintenanceRecordRepository.DeleteByVehicleAndDate(long VehicleID, DateTime Earliest, DateTime Latest)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM maintenancerecord " +
                              "WHERE VehicleID = ? " +
                              "AND MaintenanceDate => ? " +
                              "AND MaintenanceDate <= ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_VehicleID",
                Type = DbType.Int64,
                Value = VehicleID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Earliest",
                Type = DbType.DateTime,
                Value = Earliest
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Latest",
                Type = DbType.DateTime,
                Value = Latest
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Returns a collection of every unique Maintenance Record in the database
        /// </summary>
        /// <returns></returns>
        IEnumerable<MaintenanceRecord> IRepository<MaintenanceRecord>.GetAll()
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT MaintenanceID, VehicleID, MaintenanceDate, MaintenanceDescription " +
                              "FROM maintenancerecord " +
                              "ORDER BY MaintenanceID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            return this.Database.ExecuteListQuery<MaintenanceRecord>(definition, Map);
        }


        /// <summary>
        /// Returns a collection of maintenance Records that fall within the date range
        /// </summary>
        /// <param name="Earliest"></param>
        /// <param name="Latest"></param>
        /// <returns></returns>
        IEnumerable<MaintenanceRecord> IMaintenanceRecordRepository.GetRecordsByDate(DateTime Earliest, DateTime Latest)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT MaintenanceID, VehicleID, MaintenanceDate, MaintenanceDescription " +
                              "FROM maintenancerecord " +
                              "WHERE MaintenanceDate => ? " +
                              "AND MaintenanceDate <= ? " +
                              "ORDER BY MaintenanceDate",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Earliest",
                Type = DbType.DateTime,
                Value = Earliest
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Latest",
                Type = DbType.DateTime,
                Value = Latest
            });

            return this.Database.ExecuteListQuery<MaintenanceRecord>(definition, Map);
        }


        /// <summary>
        /// Returns a collection of all maintenance records ever done on a single vehicle
        /// </summary>
        /// <param name="VehicleID"></param>
        /// <returns></returns>
        IEnumerable<MaintenanceRecord> IMaintenanceRecordRepository.GetRecordsByVehicle(long VehicleID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT MaintenanceID, VehicleID, MaintenanceDate, MaintenanceDescription " +
                              "FROM maintenancerecord " +
                              "WHERE VehicleID = ? ",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_VehicleID",
                Type = DbType.Int64,
                Value = VehicleID
            });
            return this.Database.ExecuteListQuery<MaintenanceRecord>(definition, Map);
        }


        /// <summary>
        /// returns a collection of maintenance Records corresponding to the vehicleID and falling within the date range
        /// </summary>
        /// <param name="VehicleID"></param>
        /// <param name="Earliest"></param>
        /// <param name="Latest"></param>
        /// <returns></returns>
        IEnumerable<MaintenanceRecord> IMaintenanceRecordRepository.GetRecordsByVehicleAndDate(long VehicleID, DateTime Earliest, DateTime Latest)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT MaintenanceID, VehicleID, MaintenanceDate, MaintenanceDescription " +
                              "FROM maintenancerecord " +
                              "WHERE VehicleID = ? " +
                              "AND MaintenanceDate => ? " +
                              "AND MaintenanceDate <= ? " +
                              "ORDER BY maintenanceID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_VehicleID",
                Type = DbType.Int64,
                Value = VehicleID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Earliest",
                Type = DbType.DateTime,
                Value = Earliest
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Latest",
                Type = DbType.DateTime,
                Value = Latest
            });

            return this.Database.ExecuteListQuery<MaintenanceRecord>(definition, Map);
        }


        /// <summary>
        /// Finds and returns the maintenance Record with a matching ID the argument's value 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MaintenanceRecord IRepository<MaintenanceRecord>.GetSingle(object id)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT MaintenanceID, VehicleID, MaintenanceDate, MaintenanceDescription " +
                              "FROM maintenancerecord " +
                              "WHERE MaintenanceID = ? ",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenanceID",
                Type = DbType.Int64,
                Value = id
            });

            return this.Database.ExecuteSingleQuery<MaintenanceRecord>(definition, Map);
        }


        /// <summary>
        /// Finds and returns the maintenance Record with a matching ID the argument's value 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MaintenanceRecord IMaintenanceRecordRepository.GetSingle(long RecordID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT MaintenanceID, VehicleID, MaintenanceDate, MaintenanceDescription " +
                              "FROM maintenancerecord " +
                              "WHERE MaintenanceID = ? ",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenanceID",
                Type = DbType.Int64,
                Value = RecordID
            });

            return this.Database.ExecuteSingleQuery<MaintenanceRecord>(definition, Map);
        }


        /// <summary>
        /// Adds the argument object to the maintenance Record database
        /// </summary>
        /// <param name="model"></param>
        void IRepository<MaintenanceRecord>.Insert(MaintenanceRecord model)
        {

            long id;
            
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "INSERT INTO maintenancerecord (VehicleID, MaintenanceDate, MaintenanceDescription, CreatedBy, LastModifiedBy) " +
                              "VALUES (?,?,?,?,?)",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };
            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_VehicleID",
                Type = DbType.Int64,
                Value = model.VehicleID
            });
            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenanceDate",
                Type = DbType.DateTime,
                Value = model.MaintenanceDate
            });
            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenanceDescription",
                Type = DbType.String,
                Value = model.MaintenanceDescription
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

            model.MaintenanceID = id;
        }


        /// <summary>
        /// Finds the object with the model
        /// </summary>
        /// <param name="model"></param>
        void IRepository<MaintenanceRecord>.Update(MaintenanceRecord model)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "UPDATE maintenancerecord " +
                              "SET VehicleID = ?, MaintenanceDate = ?, MaintenanceDescription = ?, LastModifiedBy = ? " +
                              "WHERE MaintenanceID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };
            // create parameter definition

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_VehicleID",
                Type = DbType.Int64,
                Value = model.MaintenanceID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenanceDate",
                Type = DbType.DateTime,
                Value = model.MaintenanceID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenanceDescription",
                Type = DbType.String,
                Value = model.MaintenanceID
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
                Name = "P_MaintenanceID",
                Type = DbType.Int64,
                Value = model.MaintenanceID
            });
            this.Database.ExecuteModQuery(definition);
        }


        protected override MaintenanceRecord Map(IDataReader reader)
        {
            return new MaintenanceRecord(reader.GetValueOrDefault<Int64>("MaintenanceID"),
                reader.GetValueOrDefault<Int64>("VehicleID"),
                reader.GetValueOrDefault<DateTime>("MaintenanceDate"),
                reader.GetValueOrDefault<String>("MaintenanceDescription"));
        }
    }
}
