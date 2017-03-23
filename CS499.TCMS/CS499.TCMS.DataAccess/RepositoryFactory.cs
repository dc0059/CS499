using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ToolKit.Data;
using ToolKit.Data.Factories;

namespace CS499.TCMS.DataAccess
{
    /// <summary>
    /// This class will handle creating the various repositories
    /// </summary>
    public class RepositoryFactory
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userName">user name of the user accessing the database</param>
        /// <param name="databaseName">name of the MySQL database to connect too</param>
        public RepositoryFactory(string userName, string databaseName)
        {
            this.userName = userName;
            this.database = DatabaseFactory.Create(userName);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create repository
        /// </summary>
        /// <typeparam name="TInterface">repository Interface type</typeparam>        
        /// <returns>new instance of the concrete repository</returns>
        public TInterface Create<TInterface>() where TInterface : class, IRepositoryBase
        {

            // Get types for the repository
            Type interfaceType = typeof(TInterface);
            Type concreteType = null;

            // Get the concrete repository class that implements TInterface
            concreteType = this.GetConcreteType(interfaceType);

            // create new concrete instance of the class
            if (concreteType != null)
            {
                TInterface o = Activator.CreateInstance(concreteType,
                    new object[] { database }) as TInterface;
                return o;
            }

            return null;

        }

        /// <summary>
        /// Get concrete repository type
        /// </summary>
        /// <param name="interfaceType">type of interface</param>
        /// <returns>concrete type</returns>
        private Type GetConcreteType(Type interfaceType)
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => (interfaceType.IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface));
        }

        #endregion

        #region Properties

        private string userName;
        private IDatabase database;

        #endregion

    }
}