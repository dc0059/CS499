using System.Data;
using ToolKit.Data;

namespace ToolKit.Data
{
    /// <summary>
    /// This class will hold a reference to the database class
    /// and mapping method
    /// </summary>
    public abstract class GenericRepository<T>
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="database">Database class</param>
        public GenericRepository(IDatabase database)
        {
            this.Database = database;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Maps a DataRecord to the Model
        /// </summary>
        /// <param name="reader">DataReader</param>
        /// <returns>a new instance of the Model</returns>
        protected abstract T Map(IDataReader reader);

        #endregion

        #region Properties

        protected IDatabase Database;

        #endregion

    }
}
