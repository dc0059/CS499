//STILL NEED TO VERIFY QUERIES AND DATABASE TABLE NAMES, COLUMN NAMES, ETC
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.DataAccess.IRepositories;
using CS499.TCMS.Model;
using ToolKit.Data;

namespace CS499.TCMS.DataAccess.Repositories
{
    class BusinessPartnerRepository : GenericRepository<BusinessPartner>, IBusinessPartnerRepository
    {

        #region Constructor
        /// <summary>
        /// Business Partner Repository Constructor
        /// </summary>
        /// <param name="database"></param>
        public BusinessPartnerRepository(IDatabase database) : base(database)
        {
        }
        #endregion


        /// <summary>
        /// Delete Business Partner Argument from Database
        /// </summary>
        /// <param name="model"></param>
        void IRepository<BusinessPartner>.Delete(BusinessPartner model)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM businesspartners " +
                              "WHERE CompanyID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_CompanyID",
                Type = DbType.Int64,
                Value = model.CompanyID
            });

            this.Database.ExecuteModQuery(definition);

            // Create query definition
            definition = new QueryDefinition()
            {
                CommandText = "UPDATE businesspartners_log " +
                              "SET DeletedBy = ? " +
                              "WHERE CompanyID = ? " +
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
                Value = model.CompanyID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Uses ID field from BusinessPartner object as the argument
        /// </summary>
        /// <param name="PartnerID"></param>
        void IBusinessPartnerRepository.Delete(long PartnerID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM businesspartners " +
                              "WHERE CompanyID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_CompanyID",
                Type = DbType.Int64,
                Value = PartnerID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Deletes a BusinessPartner by Name Value
        /// </summary>
        /// <param name="PartnerName"></param>
        void IBusinessPartnerRepository.Delete(string PartnerName)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM businesspartners " +
                              "WHERE CompanyName = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_CompanyName",
                Type = DbType.String,
                Value = PartnerName
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Delete BusinessPartner(s) by State
        /// </summary>
        /// <param name="State"></param>
        void IBusinessPartnerRepository.DeleteByState(string State)
        {
            //Find all Partners from argument State

            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM businesspartners " +
                              "WHERE State = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_State",
                Type = DbType.String,
                Value = State
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Delete BusinessPartner(s) by Zip Code
        /// </summary>
        /// <param name="Zip"></param>
        void IBusinessPartnerRepository.DeleteByZipCode(int Zip)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM businesspartners " +
                              "WHERE ZipCode = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ZipCode",
                Type = DbType.Int32,
                Value = Zip
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Get all BusinessPartners in the Database
        /// </summary>
        /// <returns>Collection of each unique BusinessPartner Object</returns>
        IEnumerable<BusinessPartner> IRepository<BusinessPartner>.GetAll()
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT CompanyID, CompanyName, Address, City, State, ZipCode, PhoneNumber" +
                              "FROM businessPartner " +
                              "ORDER BY CompanyID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            return this.Database.ExecuteListQuery<BusinessPartner>(definition, Map);
        }


        IEnumerable<BusinessPartner> IBusinessPartnerRepository.GetPartnersByState(string State)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT CompanyID, CompanyName, Address, City, State, ZipCode, PhoneNumber" +
                              "FROM businesspartners " +
                              "WHERE State = ? " +
                              "ORDER BY CompanyID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_State",
                Type = DbType.Int64,
                Value = State
            });
            return this.Database.ExecuteListQuery<BusinessPartner>(definition, Map);
        }


        IEnumerable<BusinessPartner> IBusinessPartnerRepository.GetPartnersByZipCode(int Zip)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT CompanyID, CompanyName, Address, City, State, ZipCode, PhoneNumber" +
                              "FROM businesspartners " +
                              "WHERE ZipCode = ? " +
                              "ORDER BY CompanyID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_State",
                Type = DbType.Int64,
                Value = Zip
            });
            return this.Database.ExecuteListQuery<BusinessPartner>(definition, Map);
        }


        /// <summary>
        /// Finds a BusinessPartner in the database matching the ID argument. Argument should be a long int.
        /// </summary>
        /// <param name="id">long Integer Representing Partner ID</param>
        /// <returns>Business Partner Object matching Partner's ID</returns>
        BusinessPartner IRepository<BusinessPartner>.GetSingle(object id)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT CompanyID, CompanyName, Address, City, State, ZipCode, PhoneNumber" +
                              "FROM businesspartners " +
                              "WHERE CompanyID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartnerID",
                Type = DbType.Int64,
                Value = id
            });

            return this.Database.ExecuteSingleQuery<BusinessPartner>(definition, Map);
        }


        /// <summary>
        /// Finds a BusinessPartner in the database matching the ID argument
        /// </summary>
        /// <param name="id">Integer Representing Partner ID</param>
        /// <returns>Business Partner Object matching Partner's ID</returns>
        BusinessPartner IBusinessPartnerRepository.GetSingle(long PartnerID)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT CompanyID, CompanyName, Address, City, State, ZipCode, PhoneNumber" +
                              "FROM businesspartners " +
                              "WHERE CompanyID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartnerID",
                Type = DbType.Int64,
                Value = PartnerID
            });
            return this.Database.ExecuteSingleQuery<BusinessPartner>(definition, Map);

        }


        /// <summary>
        /// Finds a BusinessPartner in the Database by Name. 
        /// </summary>
        /// <param name="PartnerName"></param>
        /// <returns>Business Partner Object with matching name field</returns>
        BusinessPartner IBusinessPartnerRepository.GetSingle(string PartnerName)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT CompanyID, CompanyName, Address, City, State, ZipCode, PhoneNumber" +
                              "FROM businesspartners " +
                              "WHERE CompanyName = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartnerName",
                Type = DbType.String,
                Value = PartnerName
            });
            return this.Database.ExecuteSingleQuery<BusinessPartner>(definition, Map);
        }


        /// <summary>
        /// Inserts a business Partner object into the Database
        /// </summary>
        /// <param name="model"></param>
        void IRepository<BusinessPartner>.Insert(BusinessPartner model)
        {

            long id;
            
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "INSERT INTO businesspartners (CompanyName, Address, City, State, ZipCode, PhoneNumber, CreatedBy, LastModifiedBy) " +
                              "VALUES (?,?,?,?,?,?,?,?)",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_CompanyName",
                Type = DbType.String,
                Value = model.CompanyName
            });

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Address",
                Type = DbType.String,
                Value = model.Address
            });

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_City",
                Type = DbType.String,
                Value = model.City
            });

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_State",
                Type = DbType.String,
                Value = model.State
            });

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ZipCode",
                Type = DbType.Int64,
                Value = model.ZipCode
            });

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PhoneNumber",
                Type = DbType.String,
                Value = model.PhoneNumber
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

            model.CompanyID = id;
        }


        /// <summary>
        /// Updates the BusinessPartner in the database represented by the object Argument
        /// </summary>
        /// <param name="model"></param>
        void IRepository<BusinessPartner>.Update(BusinessPartner model)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "UPDATE businesspartners " +
                              "SET CompanyName = ?, Address = ?, City = ?, State = ?, ZipCode = ?, PhoneNumber = ?, LastModifiedBy = ? " +
                              "WHERE CompanyID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };



            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_CompanyName",
                Type = DbType.String,
                Value = model.CompanyName
            });

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Address",
                Type = DbType.String,
                Value = model.Address
            });

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_City",
                Type = DbType.String,
                Value = model.City
            });

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_State",
                Type = DbType.String,
                Value = model.State
            });

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ZipCode",
                Type = DbType.Int32,
                Value = model.ZipCode
            });

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PhoneNumber",
                Type = DbType.String,
                Value = model.PhoneNumber
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
                Name = "P_CompanyID",
                Type = DbType.Int64,
                Value = model.CompanyID
            });

            this.Database.ExecuteModQuery(definition);
        }


        /// <summary>
        /// Map the IDataReader to the Business Partner model
        /// </summary>
        /// <param name="reader">DataReader</param>
        /// <returns>new BusinessPartner model</returns>
        protected override BusinessPartner Map(IDataReader reader)
        {
            return new BusinessPartner(reader.GetValueOrDefault<Int64>("CompanyID"),
                reader.GetValueOrDefault<string>("CompanyName"),
                reader.GetValueOrDefault<string>("Address"),
                reader.GetValueOrDefault<string>("City"),
                reader.GetValueOrDefault<string>("State"),
                reader.GetValueOrDefault<Int32>("ZipCode"),
                reader.GetValueOrDefault<string>("PhoneNumber"));
        }

    }
}
