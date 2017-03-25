using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS499.TCMS.View.ViewModels
{

    /// <summary>
    /// This class will handle creating each workspace to add to the MainWindow
    /// </summary>
    public static class WorkspaceFactory
    {

        #region Methods

        /// <summary>
        /// Create workspace ViewModel
        /// </summary>
        /// <typeparam name="T">type of ViewModel</typeparam>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <returns>
        /// new instance of the ViewModel
        /// </returns>
        public static T Create<T>(params object[] constructorArgs) where T : WorkspaceViewModel
        {

            // get subclass type
            Type subType = typeof(T);

            // get concrete type
            Type concreteType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => subType.IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

            // create new instance of the concrete type
            if (concreteType != null)
            {

                WorkspaceViewModel o = Activator.CreateInstance(concreteType, constructorArgs) as T;
                return o as T;

            }

            return null;

        }

        #endregion

    }

}
