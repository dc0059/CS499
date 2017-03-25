using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Models;
using CS499.TCMS.View.Resources;
using CS499.TCMS.View.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS499.TCMS.View.ViewModels
{
    /// <summary>
    /// This class will handle the collection of Assembly information
    /// </summary>
    public class AssemblyInformationViewModel : WorkspaceViewModel
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyInformationViewModel"/> class.
        /// </summary>
        /// <param name="taskManager">The task manager.</param>
        public AssemblyInformationViewModel(ITaskManager taskManager)
        {
            this.TaskManager = taskManager;
            this.ContentId = this.DisplayName = Messages.AssemblyInformationDisplayName;
            this.IsNew = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        /// <value>
        /// The assemblies.
        /// </value>
        public List<AssemblyInformation> Assemblies
        {
            get
            {
                return CoreAssembly.GetLoadedAssemblies();
            }
        }

        /// <summary>
        /// Gets the update location.
        /// </summary>
        /// <value>
        /// The update location.
        /// </value>
        public string UpdateLocation
        {
            get
            {
                return CoreAssembly.GetUpdateLocation();
            }
        }

        /// <summary>
        /// Gets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string DatabaseName
        {
            get
            {
                return CoreAssembly.GetDatabaseName();
            }
        }

        /// <summary>
        /// Gets the task log.
        /// </summary>
        /// <value>
        /// The task log.
        /// </value>
        public ObservableCollection<Tuple<string, DateTime>> TaskLog
        {
            get { return this.TaskManager.TaskLog; }
        }

        #endregion

    }

}
