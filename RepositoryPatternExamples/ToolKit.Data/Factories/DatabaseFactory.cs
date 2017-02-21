using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolKit.Data.Factories
{
    /// <summary>
    /// This class will create a new instance of the IDatabase
    /// </summary>
    public static class DatabaseFactory
    {

        /// <summary>
        /// Create new instance of the database
        /// </summary>
        /// <param name="userName">user name of the current logged in user</param>
        /// <returns>instance of a database</returns>
        public static IDatabase Create(string userName)
        {
            return new Database(userName);
        }

    }
}
