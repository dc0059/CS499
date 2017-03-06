using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ToolKit.Data
{
    /// <summary>
    /// This class will hold the query definitions
    /// </summary>
    public class QueryDefinition
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public QueryDefinition()
        {
            this.Parameters = new List<ParameterDefinition>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Database connection type
        /// </summary>
        public ConnectionType Type { get; set; }

        /// <summary>
        /// Database to connect to when using MySQL connection type
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Command type
        /// </summary>
        public CommandType cType { get; set; }

        /// <summary>
        /// Command text
        /// </summary>
        public string CommandText { get; set; }

        /// <summary>
        /// List of parameter definition
        /// </summary>
        public List<ParameterDefinition> Parameters { get; set; }
        
        #endregion

    }
}
