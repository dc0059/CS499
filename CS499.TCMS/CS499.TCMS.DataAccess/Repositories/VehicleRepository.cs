using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;
using ToolKit.Data;
using System.Data;
using CS499.TCMS.DataAccess.IRepositories;

namespace CS499.TCMS.DataAccess.Repositories
{
    internal class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="database">database connection</param>
        public VehicleRepository(IDatabase database)
            : base(database)
        {

        }

        #endregion

        #region Methods
        public IEnumerable<Vehicle> GetVehiclesBySpecs(string Brand = null, string Model = null, int Year = 2000)
        {
            //Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT VehicleID, Brand, Year, Model, VehicleType, Capacity " +
                              "FROM vehicle " +
                              "WHERE Brand = ? AND Model = ? AND Year = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            //create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Brand",
                Type = DbType.String,
                Value = Brand
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Model",
                Type = DbType.String,
                Value = Model
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Year",
                Type = DbType.Int16,
                Value = Year
            });

            return this.Database.ExecuteListQuery<Vehicle>(definition, Map);
        }

        public IEnumerable<Vehicle> GetVehiclesByType(string VehicleType)
        {
            //Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT VehicleID, Brand, Year, Model, VehicleType, Capacity " +
                              "FROM vehicle " +
                              "WHERE VehicleType = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            //create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_VehicleType",
                Type = DbType.String,
                Value = VehicleType
            });

            return this.Database.ExecuteListQuery<Vehicle>(definition, Map);
        }

        public IEnumerable<Vehicle> GetVehiclesByCapacity(int CapacityClass)
        {
            //Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT VehicleID, Brand, Year, Model, VehicleType, Capacity " +
                              "FROM vehicle " +
                              "WHERE Capacity = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            //create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Capacity",
                Type = DbType.String,
                Value = CapacityClass
            });

            return this.Database.ExecuteListQuery<Vehicle>(definition, Map);
        }

        public void DeleteBySpecs(string Brand = null, string Model = null, int Year = 2000)
        {
            //Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM vehicle " +
                              "WHERE Brand = ? AND Model = ? AND Year = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            //create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Brand",
                Type = DbType.String,
                Value = Brand
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Model",
                Type = DbType.String,
                Value = Model
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Year",
                Type = DbType.Int16,
                Value = Year
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void DeleteByType(string VehicleType)
        {
            //Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM vehicle " +
                              "WHERE VehicleType = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            //create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_VehicleType",
                Type = DbType.String,
                Value = VehicleType
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void DeleteByCapacity(int CapacityClass)
        {
            //Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM vehicle " +
                              "WHERE Capacity = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            //create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Capacity",
                Type = DbType.String,
                Value = CapacityClass
            });

            this.Database.ExecuteModQuery(definition);
        }

        public Vehicle GetSingle(object id)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT VehicleID, Brand, Year, Model, VehicleType, Capacity " +
                              "FROM vehicle " +
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
                Value = id
            });

            return this.Database.ExecuteSingleQuery<Vehicle>(definition, Map);
        }

        public IEnumerable<Vehicle> GetAll()
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT VehicleID, Brand, Year, Model, VehicleType, Capacity " +
                              "FROM vehicle " +
                              "ORDER BY VehicleID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            return this.Database.ExecuteListQuery<Vehicle>(definition, Map);
        }

        public void Update(Vehicle model)
        {
            //create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "UPDATE vehicle " +
                              "SET Brand = ?, Year = ?, Model = ?, VehicleType = ?, Capacity = ?, LastModifiedBy = ? " +
                              "WHERE VehicleID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Brand",
                Type = DbType.String,
                Value = model.Brand
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Year",
                Type = DbType.Int16,
                Value = model.Year
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Model",
                Type = DbType.String,
                Value = model.Model
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_VehicleType",
                Type = DbType.Int32,
                Value = model.VehicleType
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Capacity",
                Type = DbType.Int16,
                Value = model.Capacity
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
                Name = "P_VehicleID",
                Type = DbType.Int64,
                Value = model.VehicleID
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void Delete(Vehicle model)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM vehicle " +
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
                Value = model.VehicleID
            });

            this.Database.ExecuteModQuery(definition);

            // Create query definition
            definition = new QueryDefinition()
            {
                CommandText = "UPDATE vehicle_log " +
                              "SET DeletedBy = ? " +
                              "WHERE VehicleID = ? " +
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
                Value = model.VehicleID
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void Insert(Vehicle model)
        {

            long id;
            
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "INSERT INTO vehicle (Brand, Year, Model, VehicleType, Capacity, CreatedBy, LastModifiedBy) " +
                              "VALUES (?,?,?,?,?,?,?)",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            //define parameters
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Brand",
                Type = DbType.String,
                Value = model.Brand
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Year",
                Type = DbType.Int16,
                Value = model.Year
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Model",
                Type = DbType.String,
                Value = model.Model
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_VehicleType",
                Type = DbType.Int32,
                Value = model.VehicleType
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Capacity",
                Type = DbType.Int16,
                Value = model.Capacity
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

            model.VehicleID = id;
        }

        protected override Vehicle Map(IDataReader reader)
        {
            return new Vehicle(reader.GetValueOrDefault<long>("VehicleID"),
                reader.GetValueOrDefault<string>("Brand"),
                reader.GetValueOrDefault<int>("Year"),
                reader.GetValueOrDefault<string>("Model"),
                (Enums.TruckMaxCapacity)reader.GetValueOrDefault<int>("VehicleType"),
                reader.GetValueOrDefault<int>("Capacity"));
        }

        #endregion
    }
}
