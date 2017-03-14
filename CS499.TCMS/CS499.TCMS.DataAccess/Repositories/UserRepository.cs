using System;
using System.Collections.Generic;
using System.Data;
//using System.DirectoryServices.AccountManagement;
using ToolKit.Data;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess
{
    /// <summary>
    /// This class will handle all the CRUD operations for the users
    /// </summary>
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="database">database connection</param>
        public UserRepository(IDatabase database)
            : base(database)
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="model">user model</param>
        void IRepository<User>.Delete(User model)
        {

            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM user " +
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
                Value = model.EmployeeID
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

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Enumerator to traverse the collection of models</returns>
        IEnumerable<User> IRepository<User>.getAll()
        {

            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT EmployeeID, UserName, FirstName, MiddleName, LastName, Address, City, State, ZipCode, HomePhone, CellPhone, " +
                              "EmailAddress, PayRate, EmploymentDate, JobID, HomeStore, JobDescription, IsActive, HashKey, PassPhrase " +
                              "FROM user " +
                              "ORDER BY EmployeeID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            return this.Database.ExecuteListQuery<User>(definition, Map);

        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">unique identifier</param>
        /// <returns>user model</returns>
        User IRepository<User>.getSingle(object id)
        {

            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT EmployeeID, UserName, FirstName, MiddleName, LastName, Address, City, State, ZipCode, HomePhone, CellPhone, " +
                              "EmailAddress, PayRate, EmploymentDate, JobID, HomeStore, JobDescription, IsActive, HashKey, PassPhrase " +
                              "FROM user " +
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
                Value = id
            });

            return this.Database.ExecuteSingleQuery<User>(definition, Map);

        }

        /// <summary>
        /// Insert user
        /// </summary>
        /// <param name="model">user model</param>
        void IRepository<User>.Insert(User model)
        {

            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "INSERT INTO user (UserName, FirstName, MiddleName, LastName, Address, City, State, ZipCode, HomePhone, CellPhone, EmailAddress, " +
                              "PayRate, EmploymentDate, JobID, HomeStore, JobDescription, IsActive) " +
                              "VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            /*definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "EmployeeID",
                Type = DbType.Int64,
                Value = model.EmployeeID
            });*/
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_UserName",
                Type = DbType.String,
                Value = model.UserName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_FirstName",
                Type = DbType.String,
                Value = model.FirstName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MiddleName",
                Type = DbType.String,
                Value = model.MiddleName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_LastName",
                Type = DbType.String,
                Value = model.LastName
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
                Name = "P_HomePhone",
                Type = DbType.String,
                Value = model.HomePhone
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_CellPhone",
                Type = DbType.String,
                Value = model.CellPhone
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_EmailAddress",
                Type = DbType.String,
                Value = model.EmailAddress
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PayRate",
                Type = DbType.Double,
                Value = model.PayRate
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_EmploymentDate",
                Type = DbType.Date,
                Value = model.EmploymentDate
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_JobID",
                Type = DbType.Int64,
                Value = model.JobID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_HomeStore",
                Type = DbType.String,
                Value = model.HomeStore
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_JobDescription",
                Type = DbType.String,
                Value = model.JobDescription
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_IsActive",
                Type = DbType.Boolean,
                Value = model.IsActive
            });

            this.Database.ExecuteModQuery(definition);

        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="model">user model</param>
        void IRepository<User>.Update(User model)
        {

            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "UPDATE user " +
                              "SET UserName = ?, FirstName = ?, MiddleName = ?, LastName = ?, Address = ?, City = ?, State = ?, ZipCode = ?, " +
                              "HomePhone = ?, CellPhone = ?, EmailAddress = ?, PayRate = ?, EmploymentDate = ?, JobID = ?, HomeStore = ?, " +
                              "JobDescription = ?, IsActive = ? " +
                              "WHERE EmployeeID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_UserName",
                Type = DbType.String,
                Value = model.UserName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_FirstName",
                Type = DbType.String,
                Value = model.FirstName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_MiddleName",
                Type = DbType.String,
                Value = model.MiddleName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_LastName",
                Type = DbType.String,
                Value = model.LastName
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
                Name = "P_HomePhone",
                Type = DbType.String,
                Value = model.HomePhone
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_CellPhone",
                Type = DbType.String,
                Value = model.CellPhone
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_EmailAddress",
                Type = DbType.String,
                Value = model.EmailAddress
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PayRate",
                Type = DbType.Double,
                Value = model.PayRate
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_EmploymentDate",
                Type = DbType.DateTime,
                Value = model.EmploymentDate
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_JobID",
                Type = DbType.Int64,
                Value = model.JobID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_HomeStore",
                Type = DbType.String,
                Value = model.HomeStore
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_JobDescription",
                Type = DbType.String,
                Value = model.JobDescription
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_IsActive",
                Type = DbType.Boolean,
                Value = model.IsActive
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
        /// Map the IDataReader to the user model
        /// </summary>
        /// <param name="reader">DataReader</param>
        /// <returns>new user model</returns>
        protected override User Map(IDataReader reader)
        {
            return new User(reader.GetValueOrDefault<Int64>("EmployeeID"),
                reader.GetValueOrDefault<string>("UserName"),
                reader.GetValueOrDefault<string>("FirstName"),
                reader.GetValueOrDefault<string>("MiddleName"),
                reader.GetValueOrDefault<string>("LastName"),
                reader.GetValueOrDefault<string>("Address"),
                reader.GetValueOrDefault<string>("City"),
                reader.GetValueOrDefault<string>("State"),
                reader.GetValueOrDefault<Int32>("ZipCode"),
                reader.GetValueOrDefault<string>("HomePhone"),
                reader.GetValueOrDefault<string>("CellPhone"),
                reader.GetValueOrDefault<string>("EmailAddress"),
                reader.GetValueOrDefault<Double>("PayRate"),
                reader.GetValueOrDefault<DateTime>("EmploymentDate"),
                reader.GetValueOrDefault<Int64>("JobID"),
                reader.GetValueOrDefault<string>("HomeStore"),
                reader.GetValueOrDefault<string>("JobDescription"),
                reader.GetValueOrDefault<Boolean>("IsActive"),
                reader.GetValueOrDefault<string>("HashKey"),
                reader.GetValueOrDefault<string>("PassPhrase"));
        }

        /// <summary>
        /// Map the UserPrincipal to the user model
        /// </summary>
        /// <param name="user">UserPrincipal</param>
        /// <returns>new user model</returns>
        /*private User Map(UserPrincipal user)
        {
            return new User(0,
                user.SamAccountName,
                user.EmailAddress);
        }*/




        #endregion

    }
}