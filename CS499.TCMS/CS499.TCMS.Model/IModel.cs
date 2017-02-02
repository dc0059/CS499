using System.ComponentModel;

namespace CS499.TCMS.Model
{
    /// <summary>
    /// This interface will expose common model class functions and properies
    /// </summary>
    public interface IModel : IDataErrorInfo
    {

        /// <summary>
        /// Flag indicating the model passes the verification test
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Unique identifier
        /// </summary>
        long ID { get; set; }

    }
}
