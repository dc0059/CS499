using System.Windows.Controls;

namespace CS499.TCMS.View.Controls
{
    /// <summary>
    /// This class will subclass the listbox and force it to scroll the selected item into view
    /// </summary>
    public class ListBoxScrollToSelectedItem : ListBox
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ListBoxScrollToSelectedItem()
            : base()
        {
            this.SelectionChanged += (s, e) =>
            {

                if (this.SelectedItem != null)
                {
                    this.ScrollIntoView(this.SelectedItem);
                }

            };
        }

        #endregion

    }
}
