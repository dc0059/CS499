
namespace CS499.TCMS.View.Interfaces
{
    /// <summary>
    /// This interface will implement a check for changes method
    /// to make all ViewModels have a common method
    /// </summary>
    public interface IChanges
    {

        /// <summary>
        /// Will check for any changes made to the underlying model
        /// </summary>
        /// <returns>true if there are any changes found</returns>
        bool CheckForChanges();

    }
}
