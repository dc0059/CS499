using System;

namespace ToolKit.Data.Factories
{
    /// <summary>
    /// This class will handle the connection string creation
    /// </summary>
    internal static class ConnectionStringFactory
    {

        /// <summary>
        /// Get the connection string for the connection type
        /// </summary>
        /// <param name="definition">query definitions</param>
        /// <returns>connection string based on the connection type</returns>
        public static string GetConnectionString(QueryDefinition definition)
        {
            string conn = null;

            switch (definition.Type)
            {
                case ConnectionType.MySQL:

                    if (definition.Database == null)
                        throw new ArgumentNullException("database");

                    conn = GetMySQLConnectionString(definition.Database);

                    break;
                default:
                    break;
            }

            return conn;
        }

        /// <summary>
        /// Get the MySQL connection string
        /// </summary>
        /// <param name="database">name of the database to connect to</param>
        /// <returns>connection string</returns>
        private static string GetMySQLConnectionString(string database)
        {
            return @"Server=68.186.187.139;Database=" + database + @";uid=cs499;pwd=TCMS499;Pooling=True";
        }


    }
}
