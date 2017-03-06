using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using ToolKit.Data;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess
{
    /// <summary>
    /// This interface will extend the IRepository interface
    /// </summary>
    public interface IUserRepository : IRepository<Model.User>, IRepositoryBase
    {

        #region Methods

        /// <summary>
        /// Get all users by the notification type
        /// </summary>
        /// <param name="type">notification type</param>
        /// <returns>Enumerator to traverse the collection of models</returns>
        IEnumerable<Model.User> GetUsersByNotification(Enums.NotificationTypes type);

        /// <summary>
        /// Get user from active directory
        /// </summary>
        /// <param name="userName">string for the username</param>
        /// <returns>user model</returns>
        Model.User GetUserFromAD(string userName);

        #endregion

    }
}