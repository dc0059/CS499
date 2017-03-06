using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using ToolKit.Data.Factories;

namespace ToolKit.Data
{
    /// <summary>
    /// This class will be the concrete implementation of the 
    /// functionality of the database queries
    /// </summary>
    internal class Database : IDatabase
    {

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="userName">user name of the current logged in user</param>
        public Database(string userName)
        {
            this.UserName = userName;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Execute query that will return one or more objects
        /// </summary>
        /// <typeparam name="T">Type of object returned</typeparam>
        /// <param name="definition">query definition</param>
        /// <param name="mapping">mapping function</param>
        /// <returns>list of type of object</returns>
        public IEnumerable<T> ExecuteListQuery<T>(QueryDefinition definition, Func<IDataReader, T> mapping)
        {

            // create new connection
            using (IDbConnection conn = ConnectionFactory.GetConnection(definition))
            {

                // open connectioin
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw new DatabaseException(string.Format("Failed to connect to ({0}) database.",
                        definition.Database.ToString()), ex, ErrorTypes.DbConnectionFailure);
                }

                // create new command
                using (IDbCommand command = this.CreateCommand(definition, conn))
                {

                    // create new reader
                    IDataReader reader;
                    try
                    {
                        reader = command.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        throw new DatabaseException(string.Format("({0}) Database query failed.",
                            definition.Database.ToString()), ex, ErrorTypes.DbQueryFailure);
                    }

                    while (reader.Read())
                    {

                        yield return mapping(reader);

                    }

                    // clean up
                    reader.Close();
                    reader.Dispose();

                }

            }

        }

        /// <summary>
        /// Execute query that returns a datatable
        /// </summary>
        /// <param name="definition">query definition</param>
        /// <returns>single datatable</returns>
        public DataTable ExecuteDataTableQuery(QueryDefinition definition)
        {

            DataTable dataTable = new DataTable("Data");

            // create new connection
            using (IDbConnection conn = ConnectionFactory.GetConnection(definition))
            {

                // open connectioin
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw new DatabaseException(string.Format("Failed to connect to ({0}) database.",
                        definition.Database.ToString()), ex, ErrorTypes.DbConnectionFailure);
                }

                // create new command
                using (IDbCommand command = this.CreateCommand(definition, conn))
                {

                    // create new reader
                    IDataReader reader;
                    try
                    {
                        reader = command.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        throw new DatabaseException(string.Format("({0}) Database query failed.",
                            definition.Database.ToString()), ex, ErrorTypes.DbQueryFailure);
                    }

                    // load data from reader
                    dataTable.Load(reader);

                    // clean up
                    reader.Close();
                    reader.Dispose();

                }

            }

            return dataTable;

        }

        /// <summary>
        /// Execute query that will return a single object
        /// </summary>
        /// <typeparam name="T">Type of object returned</typeparam>
        /// <param name="definition">query definition</param>
        /// <param name="mapping">mapping function</param>
        /// <returns>single instance of type of object</returns>
        public T ExecuteSingleQuery<T>(QueryDefinition definition, Func<IDataReader, T> mapping)
        {

            // create new connection
            using (IDbConnection conn = ConnectionFactory.GetConnection(definition))
            {

                // open connectioin
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw new DatabaseException(string.Format("Failed to connect to ({0}) database.",
                        definition.Database.ToString()), ex, ErrorTypes.DbConnectionFailure);
                }

                // create new command
                using (IDbCommand command = this.CreateCommand(definition, conn))
                {

                    // create new reader
                    try
                    {

                        using (IDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                return mapping(reader);

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new DatabaseException(string.Format("({0}) Database query failed.",
                            definition.Database.ToString()), ex, ErrorTypes.DbQueryFailure);
                    }

                }

            }

            return default(T);

        }

        /// <summary>
        /// Execute query that does not return an object
        /// </summary>
        /// <param name="definition">query definition</param>
        public void ExecuteModQuery(QueryDefinition definition)
        {

            this.ExecuteModQueryInternal(definition);

        }

        /// <summary>
        /// Execute query that does not return an object
        /// </summary>
        /// <param name="definition">query definition</param>
        /// <param name="id">last inserted id</param>
        public void ExecuteModQuery(QueryDefinition definition, out long id)
        {

            id = this.ExecuteModQueryInternal(definition);

        }

        /// <summary>
        /// Execute query that does not return an object
        /// </summary>
        /// <param name="definition">query definition</param>
        /// <returns>last inserted id</returns>
        private long ExecuteModQueryInternal(QueryDefinition definition)
        {
            long id = -1;

            // check to make sure the we are not trying to modify PFS or Baan
            if (definition.Type != ConnectionType.MySQL)
                throw new ArgumentException("Baan and PFS are read only connections");

            // create new connection
            using (IDbConnection conn = ConnectionFactory.GetConnection(definition))
            {

                // open connection
                this.OpenConnection(definition, conn);

                // create new command
                using (IDbCommand command = this.CreateCommand(definition, conn))
                {

                    // execute query
                    try
                    {
                        command.ExecuteNonQuery();

                        // get last insertion id
                        id = (command as MySqlCommand).LastInsertedId;
                    }
                    catch (Exception ex)
                    {
                        throw new DatabaseException(string.Format("({0}) Database query failed.",
                            definition.Database.ToString()), ex, ErrorTypes.DbQueryFailure);
                    }

                }

            }

            return id;

        }

        /// <summary>
        /// Open connection
        /// </summary>
        /// <param name="definition">query definition</param>
        /// <param name="conn">database connection</param>
        private void OpenConnection(QueryDefinition definition, IDbConnection conn)
        {

            // try to connection more than once
            for (int i = 0; i < CONNECTION_RETRIES; i++)
            {

                try
                {

                    conn.Open();
                    break;

                }
                catch (Exception ex)
                {

                    // throw the connection exception if the last attempt failed
                    if (i == (CONNECTION_RETRIES - 1))
                    {
                        throw new DatabaseException(string.Format("Failed to connect to ({0}) database.",
                            definition.Database.ToString()), ex, ErrorTypes.DbConnectionFailure);
                    }
                    else
                    {

                        // sleep if connection fails, but wait longer for each failed connection attempt
                        Thread.Sleep((i + 1) * 1000);

                    }

                }

            }

        }

        /// <summary>
        /// Create a command based on the query definition
        /// </summary>
        /// <param name="definition">query definition</param>
        /// <param name="conn">connection to the database</param>
        /// <returns>a new command</returns>
        private IDbCommand CreateCommand(QueryDefinition definition, IDbConnection conn)
        {

            IDbCommand command = null;

            // Create command
            switch (definition.Type)
            {

                case ConnectionType.MySQL:

                    command = new MySqlCommand(definition.CommandText, (MySqlConnection)conn);

                    break;
                default:
                    break;
            }

            // set command type
            command.CommandType = definition.cType;

            // set command timeout
            command.CommandTimeout = int.MaxValue;

            // create parameters
            this.CreateParameters(command, definition);

            return command;

        }

        /// <summary>
        /// Create the parameters for the command
        /// </summary>
        /// <param name="command">new command</param>
        /// <param name="definition">query defintions</param>
        private void CreateParameters(IDbCommand command, QueryDefinition definition)
        {

            if (definition.Parameters == null)
                return;

            // loop through each query definition parameter
            foreach (var para in definition.Parameters)
            {

                // create new parameter
                IDbDataParameter newParameter = command.CreateParameter();

                // set values
                newParameter.Direction = para.Direction;
                newParameter.ParameterName = para.Name;
                newParameter.Value = para.Value;
                newParameter.DbType = para.Type;

                // add to command parameter list
                command.Parameters.Add(newParameter);

            }

        }

        #endregion

        #region Properties

        /// <summary>
        /// Will hold a reference to the user that is currently
        /// logged into the application
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The number of times to retry the connection
        /// </summary>
        private const int CONNECTION_RETRIES = 3;

        #endregion

    }
}
