﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CS499.TCMS.DataAccess
{
    /// <summary>
    /// Exposes a generic interface that all interfaces will inherit from
    /// </summary>
    public interface IRepository<T> where T : class
    {
        
        /// <summary>
        /// Generic method to get the specific object by its ID
        /// </summary>
        /// <param name="id">unique identifier</param>
        /// <returns>the object based on the ID or null if its not found</returns>
        T getSingle(object id);


        /// <summary>
        /// Generic method to get all objects in the list
        /// </summary>
        /// <returns>a list of all objects</returns>
        IEnumerable<T> getAll();


        /// <summary>
        /// Generic method to update the object
        /// </summary>
        /// <param name="model">the object to update</param>
        void Update(T model);


        /// <summary>
        /// Generic method to delete the object
        /// </summary>
        /// <param name="model">the object to be deleted</param>
        void Delete(T model);


        /// <summary>
        /// Generic method to insert a new object
        /// </summary>
        /// <param name="model">the object to be inserted</param>
        void Insert(T model);


   
        
    }
}