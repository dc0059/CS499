﻿using System.Collections.Generic;

namespace ToolKit.Data
{
    /// <summary>
    /// This interface will define a generic interface for the Read Only Repository classes
    /// </summary>
    public interface IReadOnlyRepository<T> where T : class
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

        #endregion

    }
}
