using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ToolKit.Data
{
    /// <summary>
    /// This class will hold the parameters definitions
    /// </summary>
    public class ParameterDefinition
    {

        /// <summary>
        /// Name of the parameter
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of the parameter
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Parameter direction
        /// </summary>
        public ParameterDirection Direction { get; set; }

        /// <summary>
        /// Parameter DbType
        /// </summary>
        public DbType Type { get; set; }

    }

}
