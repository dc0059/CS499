using System.Windows.Controls;

namespace CS499.TCMS.View.Controls
{
    /// <summary>
    /// This class will subclass the datagrid and force it to scroll the selected item into view
    /// </summary>
    public class DataGridScrollToSelectedItem : DataGrid
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DataGridScrollToSelectedItem()
            : base()
        {

            // create selectionchanged event handler
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
