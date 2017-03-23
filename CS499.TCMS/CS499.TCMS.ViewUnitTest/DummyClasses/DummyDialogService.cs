using CS499.TCMS.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;

namespace CS499.TCMS.ViewUnitTest.DummyClasses
{
    public class DummyDialogService : IDialogService
    {

        public DummyDialogService(IDialogCoordinator dialog, object viewModel)
        {
            this.Dialog = dialog;
            this.ViewModel = viewModel;   
        }              

        public IDialogCoordinator Dialog { get; set; }

        public object ViewModel { get; set; }

        string IDialogService.OpenFileDialog(string fileFilter)
        {
            throw new NotImplementedException();
        }

        string IDialogService.OpenFileDialog(string startUpPath, string fileFilter)
        {
            throw new NotImplementedException();
        }

        string IDialogService.SaveFileDialog(string fileFilter)
        {
            throw new NotImplementedException();
        }

        string IDialogService.SaveFileDialog(string startUpPath, string fileFilter)
        {
            throw new NotImplementedException();
        }
    }
}
