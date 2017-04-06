using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace CS499.TCMS.View.Views
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : UserControl
    {
        public ReportView()
        {
            InitializeComponent();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            if (e.PropertyName.Any<char>((c) => c.Equals('.')
                || c.Equals('/') || c.Equals('[') || c.Equals(']')
                || c.Equals('(') || c.Equals(')')))
            {

                DataGridBoundColumn dataGridCol = e.Column as DataGridBoundColumn;
                dataGridCol.Binding = new Binding("[" + e.PropertyName + "]");

            }
        }

    }
}
