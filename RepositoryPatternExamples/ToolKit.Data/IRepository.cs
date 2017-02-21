using System.Collections.Generic;

namespace ToolKit.Data
{
    /// <summary>
    /// This interface will define a generic interface for the Repository classes
    /// </summary>
    public interface IRepository<T> where T : class
    {

        #region Methods

        /// <summary>
        /// Generic method to get all objects in the list
        /// </summary>
        /// <returns>a list of all objects</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Generic method to get the specific object by its ID
        /// </summary>
        /// <param name="id">unique identifier</param>
        /// <returns>the object based on the ID or null if its not found</returns>
        T GetSingle(object id);

        /// <summary>
        /// Generic method to update the object
        /// </summary>
        /// <param name="model">the object to update</param>
        void Update(T model);

        /// <summary>
        /// Generic method to insert a new object
        /// </summary>
        /// <param name="model">the object to be inserted</param>
        void Insert(T model);

        /// <summary>
        /// Generic method to delete the object
        /// </summary>
        /// <param name="model">the object to be deleted</param>
        void Delete(T model);

        #endregion

    }
}
