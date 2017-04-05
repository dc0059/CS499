using CS499.TCMS.View.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CS499.TCMS.View.Views
{
    /// <summary>
    /// Interaction logic for MyInfoView.xaml
    /// </summary>
    public partial class MyInfoView : UserControl
    {
        public MyInfoView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the PasswordChanged event of the PasswordBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as MyInfoViewModel).Password = Password.SecurePassword;
        }

        /// <summary>
        /// Handles the PasswordChanged event of the RetryPasswordBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RetryPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as MyInfoViewModel).RetryPassword = RetryPassword.SecurePassword;
        }

    }
}
