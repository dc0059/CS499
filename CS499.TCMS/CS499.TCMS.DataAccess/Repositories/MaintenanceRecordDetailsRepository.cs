﻿using System;
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
    class MaintenanceRecordDetailRepository : GenericRepository<MaintenanceRecordDetail>, IMaintenanceRecordDetailRepository
    {

        #region Constructor
        public MaintenanceRecordDetailRepository(IDatabase database) : base(database)
        {
        }
        #endregion


        /// <summary>
        /// Deletes maintenanceRecordDetail from database corresponding to the argument object
        /// </summary>
        /// <param name="model"></param>
        void IRepository<MaintenanceRecordDetail>.Delete(MaintenanceRecordDetail model)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM maintenancerecorddetails " +
                              "WHERE DetailID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DetailID",
                Type = DbType.Int64,
                Value = model.DetailID
            });

            this.Database.ExecuteModQuery(definition);

            // Create query definition
            definition = new QueryDefinition()
            {
                CommandText = "UPDATE maintenancerecorddetails_log " +
                              "SET DeletedBy = ? " +
                              "WHERE DetailID = ? " +
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
                Value = model.DetailID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Deletes details object from database with matching DetailID
        /// </summary>
        /// <param name="DetailID"></param>
        void IMaintenanceRecordDetailRepository.Delete(long DetailID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM maintenancerecorddetails " +
                              "WHERE DetailID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DetailID",
                Type = DbType.Int64,
                Value = DetailID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Deletes all details marked with datetimes between the arguments
        /// </summary>
        /// <param name="Earliest"></param>
        /// <param name="Latest"></param>
        void IMaintenanceRecordDetailRepository.DeleteByDate(DateTime Earliest, DateTime Latest)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM maintenancerecorddetails " +
                              "WHERE RepairDate => ? " +
                              "AND RepairDate <= ?",
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
        /// Deletes each detail object in database from the given employee 
        /// </summary>
        /// <param name="EmployeeID"></param>
        void IMaintenanceRecordDetailRepository.DeleteByEmployee(long EmployeeID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM maintenancerecorddetails " +
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
        /// Deletes each detail from the given maintenanceID
        /// </summary>
        /// <param name="MaintenanceID"></param>
        void IMaintenanceRecordDetailRepository.DeleteByMaintenanceID(long MaintenanceID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM maintenancerecorddetails " +
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
                Value = MaintenanceID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Returns a collection of every MaintenanceRecordDetails from database
        /// </summary>
        /// <returns></returns>
        IEnumerable<MaintenanceRecordDetail> IRepository<MaintenanceRecordDetail>.GetAll()
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT DetailID, MaintenanceID, EmployeeID, RepairDescription, RepairDate " +
                              "FROM maintenancerecorddetails " +
                              "ORDER BY MaintenanceID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            return this.Database.ExecuteListQuery<MaintenanceRecordDetail>(definition, Map);
        }


        /// <summary>
        /// Returns a collection of every detail that is marked with a date between the argument dates
        /// </summary>
        /// <param name="Earliest"></param>
        /// <param name="Latest"></param>
        /// <returns></returns>
        IEnumerable<MaintenanceRecordDetail> IMaintenanceRecordDetailRepository.GetDetailsByDate(DateTime Earliest, DateTime Latest)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT DetailID, MaintenanceID, EmployeeID, RepairDescription, RepairDate " +
                              "FROM maintenancerecorddetails " +
                              "WHERE DetailID = ? " +
                              "AND RepairDate => ? " +
                              "AND RepairDate <= ? " +
                              "ORDER BY maintenanceID",
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


            return this.Database.ExecuteListQuery<MaintenanceRecordDetail>(definition, Map);
        }


        /// <summary>
        /// Returns all maintenance details from same employee
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        IEnumerable<MaintenanceRecordDetail> IMaintenanceRecordDetailRepository.GetDetailsByEmployee(long EmployeeID)
        {

            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT DetailID, MaintenanceID, EmployeeID, RepairDescription, RepairDate " +
                              "FROM maintenancerecorddetails " +
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

            return this.Database.ExecuteListQuery<MaintenanceRecordDetail>(definition, Map);
        }


        /// <summary>
        /// Returns all maintenance Details from same maintenance record object
        /// </summary>
        /// <param name="MaintenanceID"></param>
        /// <returns></returns>
        IEnumerable<MaintenanceRecordDetail> IMaintenanceRecordDetailRepository.GetDetailsByMaintenanceID(long MaintenanceID)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT DetailID, MaintenanceID, EmployeeID, RepairDescription, RepairDate " +
                              "FROM maintenancerecorddetails " +
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
                Value = MaintenanceID
            });

            return this.Database.ExecuteListQuery<MaintenanceRecordDetail>(definition, Map);
        }


        /// <summary>
        /// Returns a single MaintenanceRecordDetails object. Argument should be an int64 or long
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MaintenanceRecordDetail IRepository<MaintenanceRecordDetail>.GetSingle(object id)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT DetailID, MaintenanceID, EmployeeID, RepairDescription, RepairDate " +
                              "FROM maintenancerecorddetails " +
                              "WHERE DetailID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DetailID",
                Type = DbType.Int64,
                Value = id
            });

            return this.Database.ExecuteSingleQuery<MaintenanceRecordDetail>(definition, Map);
        }


        /// <summary>
        /// Returns a single MaintenanceRecordDetails object corresponding to the argument
        /// </summary>
        /// <param name="DetailID"></param>
        /// <returns></returns>
        /*MaintenanceRecordDetails IMaintenanceRecordDetailsRepository.GetSingle(long DetailID)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT DetailID, MaintenanceID, EmployeeID, RepairDescription, RepairDate " +
                              "FROM maintenancerecorddetails " +
                              "WHERE DetailID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DetailID",
                Type = DbType.Int64,
                Value = DetailID
            });

            return this.Database.ExecuteSingleQuery<MaintenanceRecordDetails>(definition, Map);
        }*/


        /// <summary>
        /// Adds the argument object to the database
        /// </summary>
        /// <param name="model"></param>
        void IRepository<MaintenanceRecordDetail>.Insert(MaintenanceRecordDetail model)
        {

            long id;

            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "INSERT INTO maintenancerecorddetails (MaintenanceID, EmployeeID, RepairDescription, RepairDate, " +
                "CreatedBy, LastModifiedBy) " +
                              "VALUES (?,?,?,?,?,?)",
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
            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_EmployeeID",
                Type = DbType.Int64,
                Value = model.EmployeeID
            });
            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_RepairDescription",
                Type = DbType.String,
                Value = model.RepairDescription
            });
            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_RepairDate",
                Type = DbType.DateTime,
                Value = model.RepairDate
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

            model.DetailID = id;
        }


        /// <summary>
        /// Finds argument in database and updates dbase information using that argument
        /// </summary>
        /// <param name="model"></param>
        void IRepository<MaintenanceRecordDetail>.Update(MaintenanceRecordDetail model)
        {

            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "UPDATE maintenancerecorddetails " +
                              "SET MaintenanceID = ?, EmployeeID = ?, RepairDescription = ?, RepairDate = ?, LastModifiedBy = ? " +
                              "WHERE DetailID = ?",
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
            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_EmployeeID",
                Type = DbType.Int64,
                Value = model.EmployeeID
            });
            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_RepairDescription",
                Type = DbType.String,
                Value = model.RepairDescription
            });
            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_RepairDate",
                Type = DbType.DateTime,
                Value = model.RepairDate
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_LastModifiedBy",
                Type = DbType.String,
                Value = this.Database.UserName
            });
            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DetailID",
                Type = DbType.Int64,
                Value = model.DetailID
            });

            this.Database.ExecuteModQuery(definition);
        }

        protected override MaintenanceRecordDetail Map(IDataReader reader)
        {
            return new MaintenanceRecordDetail(reader.GetValueOrDefault<long>("DetailID"),
                reader.GetValueOrDefault<long>("MaintenanceID"),
                reader.GetValueOrDefault<long>("EmployeeID"),
                reader.GetValueOrDefault<string>("RepairDescription"),
                reader.GetValueOrDefault<DateTime>("RepairDate"));
        }
    }
}
