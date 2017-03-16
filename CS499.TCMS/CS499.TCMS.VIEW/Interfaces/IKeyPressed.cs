using System.Windows.Input;

namespace CS499.TCMS.View.Interfaces
{
    /// <summary>
    /// This interface will contain the events when keys are pressed
    /// </summary>
    public interface IKeyPressed
    {

        // <summary>
        /// Event raised when a key is presses and the app has focus
        /// </summary>
        /// <param name="sender">Window/control</param>
        /// <param name="e">KeyEventArgs</param>
        void OnKeyPress(object sender, KeyEventArgs e);

    }
}
