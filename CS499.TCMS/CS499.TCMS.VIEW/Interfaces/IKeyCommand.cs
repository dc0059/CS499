using System.Windows.Input;

namespace CS499.TCMS.View.Interfaces
{
    /// <summary>
    /// This interface will contain the methods to hook up key combinations
    /// to method calls
    /// </summary>
    public interface IKeyCommand
    {

        /// <summary>
        /// Send key stroke to the ViewModel
        /// </summary>
        /// <param name="e">Key event args</param>
        void SendKeys(KeyEventArgs e);

    }
}
