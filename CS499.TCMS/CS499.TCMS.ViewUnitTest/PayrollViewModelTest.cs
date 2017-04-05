using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS499.TCMS.Model;
using CS499.TCMS.View.ViewModels;
using CS499.TCMS.DataAccess.IRepositories;
using CS499.TCMS.ViewUnitTest.DummyClasses;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Services;
using System.Collections.Generic;

namespace CS499.TCMS.ViewUnitTest
{
    [TestClass]
    public class PayrollViewModelTest
    {
        [TestMethod]
        public void ParollViewModelTesting()
        {
            // create new payroll repository
            IPayrollRepository payrollRepository = new DummyPayrollRepository();
           
            // create new task manager
            ITaskManager taskManager = new TaskManager(new DummyDialogService(new DummyDialogCoordinator(), null));

            // create new payroll model
            Payroll model = new Payroll(100, 100, DateTime.Now, 100, 100);

            // create new payroll viewmodel
            PayrollViewModel viewModel = new PayrollViewModel(model, payrollRepository, taskManager, true, new ObservableCollectionExtended<User>(new List<User>()));

            if (viewModel.CommandSave.CanExecute(null))
            {
                viewModel.CommandSave.Execute(null);
            }
            else
            {
                Assert.Fail("Failed to save ViewModel...");
            }
        }
    }
}
