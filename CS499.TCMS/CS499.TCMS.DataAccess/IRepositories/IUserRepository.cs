using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolKit.Data;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess
{
    /// <summary>
    /// This interface will extend the IRepository interface
    /// </summary>
    public interface IUserRepository : IRepository<User>, IRepositoryBase
    {

        #region Methods

        User getSingleByName(String firstname, String middlename, String lastname);

        User getUserByUserName(String username);
        //User getSingle(DateTime mostConvenient);

        IEnumerable<User> getUsersByJobAssignment(int JobAssignment);

        /// <summary>
        /// Change the user's password
        /// </summary>
        /// <param name="model">user model</param>
        /// <param name="newHash">new password hash</param>
        void updatePassword(User model, String newHash);

        IEnumerable<User> getUsersByZipCode(int zip);

        IEnumerable<User> getUsersByHomeStore(String HomeStore);

        /// <summary>
        /// Method that returns a collection of users that started working on a date between the given parameters
        /// </summary>
        /// <param name="earliestDate">Earliest Date in the range</param>
        /// <param name="mostRecentDate">Most recent Date in the range</param>
        /// <returns>a collection of users that started working on a date between the given parameters</returns>        
        //IEnumerable<User> getUsersByTimeWorked(DateTime earliestDate, DateTime LastestDate);

        #endregion

    }
}