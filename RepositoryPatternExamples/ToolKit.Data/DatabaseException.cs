using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ToolKit.Data
{
    /// <summary>
    /// This exception class will wrap any database errors to provide a more meaningful
    /// error message to the applications that use this library
    /// </summary>
    public class DatabaseException : Exception, ISerializable
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DatabaseException()
            : base()
        {

        }

        /// <summary>
        /// Exception with message
        /// </summary>
        /// <param name="message">description of the exception</param>
        public DatabaseException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Exception with message and Inner Exception
        /// </summary>
        /// <param name="message">description of the exception</param>
        /// <param name="innerException">inner exception</param>
        public DatabaseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Exception with message and Inner Exception
        /// </summary>
        /// <param name="message">description of the exception</param>
        /// <param name="innerException">inner exception</param>
        public DatabaseException(string message, Exception innerException, ErrorTypes errorType)
            : base(message, innerException)
        {
            this.ErrorType = errorType;
        }

        /// <summary>
        /// Serializable Exception for remote work stations
        /// </summary>
        /// <param name="info">serialization information</param>
        /// <param name="context">serialization context</param>
        public DatabaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Specifies the type of error
        /// </summary>
        public ErrorTypes ErrorType { get; set; }

        #endregion

    }

    #region Enum

    /// <summary>
    /// Type of error incountered
    /// </summary>
    public enum ErrorTypes
    {
        Unknown = 0,
        DbConnectionFailure,
        DbQueryFailure,
    }

    #endregion

}
