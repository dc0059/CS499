using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.DataAccess;
using ToolKit.Data;
using CS499.TCMS.Model;
using System.Data;

namespace CS499.TCMS.DataAccess
{
    class PartRepository : GenericRepository<Part>, IPartRepository
    {

        #region Constructor
        public PartRepository(IDatabase database) : base(database)
        {
        }
        #endregion

        #region Methods
        public void Delete(Part model)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM parts " +
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
                Value = model.PartID
            });

            this.Database.ExecuteModQuery(definition);

            // Create query definition
            definition = new QueryDefinition()
            {
                CommandText = "UPDATE parts_log " +
                              "SET DeletedBy = ? " +
                              "WHERE PartID = ? " +
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
                Value = model.PartID
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void Delete(long PartID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM parts " +
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

            // Create query definition
            /*definition = new QueryDefinition()
            {
                CommandText = "UPDATE users_log " +
                              "SET DeletedBy = ? " +
                              "WHERE UserID = ? " +
                              "AND ModifiedStatus = 'D'",
                cType = CommandType.Text,
                Database = "database_name",
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
                Value = model.EmployeeID
            });

            this.Database.ExecuteModQuery(definition);*/
        }

        public IEnumerable<Part> GetAll()
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT PartID, PartDescription, PartNumber, PartPrice, PartWeight, QuantityInStock " +
                              "FROM parts " +
                              "ORDER BY PartID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            return this.Database.ExecuteListQuery<Part>(definition, Map);
        }

        public IEnumerable<Part> GetPartsByAvailability()
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT PartID, PartDescription, PartNumber, PartPrice, PartWeight, QuantityInStock " +
                              "FROM parts " +
                              "WHERE QuantityInStock > 0 " + 
                              "ORDER BY PartID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            return this.Database.ExecuteListQuery<Part>(definition, Map);
        }

        public IEnumerable<Part> GetPartsByNumber(long PartNum)
        {
            throw new NotImplementedException();
        }

        public Part GetSingle(object id)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT PartID, PartDescription, PartNumber, PartPrice, PartWeight, QuantityInStock " +
                              "FROM parts " +
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
                Value = id
            });

            return this.Database.ExecuteSingleQuery<Part>(definition, Map);
        }


        //public Part GetSingle(long PartID)
        //{
            //throw new NotImplementedException();
        //}

        public void Insert(Part model)
        {

            long id;
            
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "INSERT INTO parts (PartDescription, PartNumber, PartPrice, PartWeight, QuantityInStock, CreatedBy, LastModifiedBy) " +
                              "VALUES (?,?,?,?,?,?,?)",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartDescription",
                Type = DbType.String,
                Value = model.PartDescription
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartNumber",
                Type = DbType.Int64,
                Value = model.PartNumber
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartPrice",
                Type = DbType.Double,
                Value = model.PartPrice
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartWeight",
                Type = DbType.Int16,
                Value = model.PartWeight
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_QuantityInStock",
                Type = DbType.Int16,
                Value = model.QuantityInStock
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

            model.PartID = id;
        }

        public void Update(Part model)
        {
            //create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "UPDATE parts " +
                              "SET PartDescription = ?, PartNumber = ?, PartPrice = ?, PartWeight = ?, QuantityInStock = ? " +
                              "WHERE PartID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartDescription",
                Type = DbType.String,
                Value = model.PartDescription
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartNumber",
                Type = DbType.Int64,
                Value = model.PartNumber
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartPrice",
                Type = DbType.Double,
                Value = model.PartPrice
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartWeight",
                Type = DbType.Int16,
                Value = model.PartWeight
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_QuantityInStock",
                Type = DbType.Int16,
                Value = model.QuantityInStock
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartID",
                Type = DbType.Int64,
                Value = model.PartID
            });

            this.Database.ExecuteModQuery(definition);
        }

        protected override Part Map(IDataReader reader)
        {
            return new Part(reader.GetValueOrDefault<Int64>("PartID"),
               reader.GetValueOrDefault<string>("PartDescription"),
               reader.GetValueOrDefault<Int16>("PartNumber"),
               reader.GetValueOrDefault<double>("PartPrice"),
               reader.GetValueOrDefault<Int16>("PartWeight"),
               reader.GetValueOrDefault<Int16>("QuantityInStock"));
        }

        #endregion
    }
}
