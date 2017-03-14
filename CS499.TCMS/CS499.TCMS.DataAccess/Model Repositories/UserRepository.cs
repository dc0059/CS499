using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices.AccountManagement;
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
                CommandText = "DELETE FROM users " +
                              "WHERE UserID = ?",
                cType = CommandType.Text,
                Database = "database_name",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ID",
                Type = DbType.Int64,
                Value = model.UserID
            });

            this.Database.ExecuteModQuery(definition);

            // Create query definition
            definition = new QueryDefinition()
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
                Value = model.UserID
            });

            this.Database.ExecuteModQuery(definition);

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
                CommandText = "SELECT users.UserID, users.UserName, users.EmailAddress " +
                              "FROM users " +
                              "ORDER BY UserName",
                cType = CommandType.Text,
                Database = "database_name",
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
                CommandText = "SELECT users.UserID, users.UserName, users.EmailAddress " +
                              "FROM users " +
                              "WHERE UserName = ?",
                cType = CommandType.Text,
                Database = "database_name",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ID",
                Type = DbType.String,
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
                CommandText = "INSERT INTO users (UserName, EmailAddress, CreatedBy, LastModifiedBy) " +
                              "VALUES (?,?,?,?)",
                cType = CommandType.Text,
                Database = "database_name",
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
                Name = "P_EmailAddress",
                Type = DbType.String,
                Value = model.EmailAddress
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
                CommandText = "UPDATE users " +
                              "SET UserName = ?, EmailAddress = ?, LastModifiedBy = ? " +
                              "WHERE UserID = ?",
                cType = CommandType.Text,
                Database = "database_name",
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
                Name = "P_EmailAddress",
                Type = DbType.String,
                Value = model.EmailAddress
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
                Name = "P_ID",
                Type = DbType.Int64,
                Value = model.UserID
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
            return new User(reader.GetValueOrDefault<Int64>("UserID"),
                reader.GetValueOrDefault<string>("UserName"),
                reader.GetValueOrDefault<string>("EmailAddress"));
        }

        /// <summary>
        /// Map the UserPrincipal to the user model
        /// </summary>
        /// <param name="user">UserPrincipal</param>
        /// <returns>new user model</returns>
        private User Map(UserPrincipal user)
        {
            return new User(0,
                user.SamAccountName,
                user.EmailAddress);
        }




        #endregion

    }
}