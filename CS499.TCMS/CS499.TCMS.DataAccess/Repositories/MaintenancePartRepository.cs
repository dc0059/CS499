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
    class MaintenancePartRepository : GenericRepository<MaintenancePart>, IMaintenancePartRepository
    {

        #region Constructor
        public MaintenancePartRepository(IDatabase database) : base(database)
        {
        }
        #endregion


        /// <summary>
        /// Deletes maintenance part object in database corresponding to argument object
        /// </summary>
        /// <param name="model"></param>
        void IRepository<MaintenancePart>.Delete(MaintenancePart model)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM maintenancepart " +
                              "WHERE MaintenancePartID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenancePartID",
                Type = DbType.Int64,
                Value = model.MaintenancePartID
            });

            this.Database.ExecuteModQuery(definition);

            // Create query definition
            definition = new QueryDefinition()
            {
                CommandText = "UPDATE maintenancepart_log " +
                              "SET DeletedBy = ? " +
                              "WHERE MaintenancePartID = ? " +
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
                Value = model.MaintenancePartID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Deletes maintenance part obj in database with matching PartID
        /// </summary>
        /// <param name="MaintenancePartID"></param>
        void IMaintenancePartRepository.Delete(long MaintenancePartID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM maintenancepart " +
                              "WHERE MaintenancePartID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenancePartID",
                Type = DbType.Int64,
                Value = MaintenancePartID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Deletes every maintenance part obj in database with a matching MaintenanceRecordID
        /// </summary>
        /// <param name="RecordID"></param>
        void IMaintenancePartRepository.DeleteByMaintenanceRecord(long RecordID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM maintenancepart " +
                              "WHERE MaintenanceRecordID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_RecordID",
                Type = DbType.Int64,
                Value = RecordID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Deletes every maintenance part obj in database with a matching PartID
        /// </summary>
        /// <param name="PartID"></param>
        void IMaintenancePartRepository.DeleteByPart(long PartID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM maintenancepart " +
                              "WHERE PartID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartID",
                Type = DbType.Int64,
                Value = PartID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Returns collection of every unique Maintenance Part obj in database
        /// </summary>
        /// <returns></returns>
        IEnumerable<MaintenancePart> IRepository<MaintenancePart>.getAll()
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT MaintenancePartID, Quantity, MaintenanceID, PartID, " +
                              "FROM maintenancepart " +
                              "ORDER BY MaintenancePartID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            return this.Database.ExecuteListQuery<MaintenancePart>(definition, Map);
        }


        /// <summary>
        /// Returns a collection of every maintenance part from the same Maintenance Record
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        IEnumerable<MaintenancePart> IMaintenancePartRepository.getPartByMaintenanceRecord(long RecordID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT MaintenancePartID, Quantity, MaintenanceRecordID, PartID, " +
                              "FROM manifest " +
                              "WHERE MaintenanceRecordID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_RecordID",
                Type = DbType.Int64,
                Value = RecordID
            });

            return this.Database.ExecuteListQuery<MaintenancePart>(definition, Map);
        }


        /// <summary>
        /// Returns a collection of every maintenance part obj that uses the same part
        /// </summary>
        /// <param name="PartID"></param>
        /// <returns></returns>
        IEnumerable<MaintenancePart> IMaintenancePartRepository.getPartByPart(long PartID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT MaintenancePartID, Quantity, MaintenanceRecordID, PartID, " +
                              "FROM manifest " +
                              "WHERE PartID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartID",
                Type = DbType.Int64,
                Value = PartID
            });

            return this.Database.ExecuteListQuery<MaintenancePart>(definition, Map);
        }


        /// <summary>
        /// Returns a maintenance Part object with the given MaintenancePartID. Arg should be an int64 or long
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MaintenancePart IRepository<MaintenancePart>.getSingle(object id)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT MaintenancePartID, Quantity, MaintenanceRecordID, PartID, " +
                              "FROM manifest " +
                              "WHERE PartID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenancePartID",
                Type = DbType.Int64,
                Value = id
            });

            return this.Database.ExecuteSingleQuery<MaintenancePart>(definition, Map);
        }


        /// <summary>
        /// Returns a single maintenance part obj with the matching maintenancepartID
        /// </summary>
        /// <param name="MaintenancePartID"></param>
        /// <returns></returns>
        MaintenancePart IMaintenancePartRepository.getSingle(long MaintenancePartID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT MaintenancePartID, Quantity, MaintenanceRecordID, PartID, " +
                              "FROM manifest " +
                              "WHERE PartID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenancePartID",
                Type = DbType.Int64,
                Value = MaintenancePartID
            });

            return this.Database.ExecuteSingleQuery<MaintenancePart>(definition, Map);
        }


        /// <summary>
        /// Adds the argument obj to the database
        /// </summary>
        /// <param name="model"></param>
        void IRepository<MaintenancePart>.Insert(MaintenancePart model)
        {

            long id;
            
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "INSERT INTO maintenancepart (MaintenancePartID, Quantity, MaintenanceRecordID, PartID, CreatedBy, LastModifiedBy) " +
                              "VALUES (?, ?, ?, ?,?,?)", 
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenancePartID",
                Type = DbType.Int64,
                Value = model.MaintenancePartID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Quantity",
                Type = DbType.Int64,
                Value = model.Quantity
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenanceRecordID",
                Type = DbType.Int64,
                Value = model.MaintenanceID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartID",
                Type = DbType.Int64,
                Value = model.PartID
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

            model.MaintenancePartID = id;
        }


        /// <summary>
        /// Finds the arg obj in database and updates dbase with arg's field values
        /// </summary>
        /// <param name="model"></param>
        void IRepository<MaintenancePart>.Update(MaintenancePart model)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "UPDATE maintenancepart " +
                              "SET Quantity = ?, MaintenanceRecordID = ?, PartID = ? " +
                              "WHERE MaintenancePartID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };


            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Quantity",
                Type = DbType.Int64,
                Value = model.Quantity
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenanceRecordID",
                Type = DbType.Int64,
                Value = model.MaintenanceID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartID",
                Type = DbType.Int64,
                Value = model.PartID
            });

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MaintenancePartID",
                Type = DbType.Int64,
                Value = model.MaintenancePartID
            });
            this.Database.ExecuteModQuery(definition);
        }

        protected override MaintenancePart Map(IDataReader reader)
        {
            return new MaintenancePart(reader.GetValueOrDefault<Int64>("MaintenancePartID"),
                reader.GetValueOrDefault<Int32>("Quantity"),
                reader.GetValueOrDefault<Int64>("MaintenanceID"),
                reader.GetValueOrDefault<Int64>("PartID"));
        }
    }
}
