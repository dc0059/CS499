using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ToolKit.Data
{
    /// <summary>
    /// This class contains some helper db methods
    /// </summary>
    public static class DbHelper
    {

        /// <summary>
        /// Get the value of the DataReader
        /// </summary>
        /// <typeparam name="T">The type to cast too</typeparam>
        /// <param name="reader">The DataReader</param>
        /// <param name="columnName">The column name to return the value of</param>
        /// <returns>the value of the DataReader at the specific column or the default value of T</returns>
        public static T GetValueOrDefault<T>(this IDataReader reader, string columnName)
        {
            int index = reader.GetOrdinal(columnName);
            object value = reader.GetValue(index);
            return value == DBNull.Value ? default(T) : (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Escapes a string for MySql queries
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EscapeString(this string value)
        {
            return MySqlHelper.EscapeString(value);
        }

    }

}
