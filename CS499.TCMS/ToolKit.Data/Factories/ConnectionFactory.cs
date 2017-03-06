using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ToolKit.Data.Factories
{
    /// <summary>
    /// This class will handle creating the database connections
    /// </summary>
    internal static class ConnectionFactory
    {

        /// <summary>
        /// Get database connection
        /// </summary>
        /// <param name="type">type of connection</param>
        /// <param name="database">query definition</param>
        /// <returns>new database connection</returns>
        public static IDbConnection GetConnection(QueryDefinition definition)
        {

            IDbConnection conn = null;

            switch (definition.Type)
            {

                case ConnectionType.MySQL:

                    if (definition.Database == null)
                        throw new ArgumentNullException("database");

                    conn = GetMySQLConnection(definition);

                    break;
                default:
                    break;
            }

            return conn;
        }

        /// <summary>
        /// Get a MySQL connection
        /// </summary>
        /// <param name="type">connection type</param>
        /// <param name="database">query definition</param>
        /// <returns>new MySQL connection</returns>
        private static MySqlConnection GetMySQLConnection(QueryDefinition definition)
        {
            return new MySqlConnection(ConnectionStringFactory.GetConnectionString(definition));
        }

    }
}
