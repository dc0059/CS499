using System;
using System.Collections.Generic;
using System.Data;

namespace ToolKit.Data
{
    /// <summary>
    /// This interface will be the Facade to the concrete implementation
    /// of the database class
    /// </summary>
    public interface IDatabase
    {

        /// <summary>
        /// Execute query that will return one or more objects
        /// </summary>
        /// <typeparam name="T">Type of object returned</typeparam>
        /// <param name="definition">query definition</param>
        /// <param name="mapping">mapping function</param>
        /// <returns>list of type of object</returns>
        IEnumerable<T> ExecuteListQuery<T>(QueryDefinition definition, Func<IDataReader, T> mapping);

        /// <summary>
        /// Execute query that will return a single object
        /// </summary>
        /// <typeparam name="T">Type of object returned</typeparam>
        /// <param name="definition">query definition</param>
        /// <param name="mapping">mapping function</param>
        /// <returns>single instance of type of object</returns>
        T ExecuteSingleQuery<T>(QueryDefinition definition, Func<IDataReader, T> mapping);

        /// <summary>
        /// Execute query that does not return an object
        /// </summary>
        /// <param name="definition">query definition</param>
        void ExecuteModQuery(QueryDefinition definition);

        /// <summary>
        /// Execute query that does not return an object
        /// </summary>
        /// <param name="definition">query definition</param>
        /// <param name="id">last inserted id</param>
        void ExecuteModQuery(QueryDefinition definition, out long id);

        /// <summary>
        /// Execute query that returns a datatable
        /// </summary>
        /// <param name="definition">query definition</param>
        /// <returns>single datatable</returns>
        DataTable ExecuteDataTableQuery(QueryDefinition definition);

        /// <summary>
        /// Will hold a reference to the user that is currently
        /// logged into the application
        /// </summary>
        string UserName { get; set; }

    }
}
