using System.Collections.Generic;
using ToolKit.Data;

namespace RepositoryPatternExamples
{
    /// <summary>
    /// This interface will extend the IRepository interface
    /// </summary>
    public interface IUserRepository : IRepository<User>, IRepositoryBase
    {

        #region Methods

        /// <summary>
        /// Get all users by the notification type
        /// </summary>
        /// <param name="type">notification type</param>
        /// <returns>Enumerator to traverse the collection of models</returns>
        IEnumerable<User> GetUsersByNotification(Enums.NotificationTypes type);

        /// <summary>
        /// Get user from active directory
        /// </summary>
        /// <param name="userName">string for the username</param>
        /// <returns>user model</returns>
        User GetUserFromAD(string userName);

        #endregion

    }
}
