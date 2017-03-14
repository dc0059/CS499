
namespace CS499.TCMS.View.Models
{
    /// <summary>
    /// This will hold reference to an assembly version and information
    /// </summary>
    public class AssemblyInformation
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">name of the assembly</param>
        /// <param name="version">version of the assembly</param>
        public AssemblyInformation(string name, string version)
        {
            this.Name = name;
            this.Version = version;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Name of the assembly
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Version of the assembly
        /// </summary>
        public string Version { get; set; }

        #endregion

    }
}
