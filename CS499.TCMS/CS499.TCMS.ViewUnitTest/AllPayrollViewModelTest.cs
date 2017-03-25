using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS499.TCMS.DataAccess.IRepositories;
using CS499.TCMS.ViewUnitTest.DummyClasses;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Services;
using CS499.TCMS.Model;
using CS499.TCMS.View.ViewModels;

namespace CS499.TCMS.ViewUnitTest
{
    [TestClass]
    public class AllPayrollViewModelTest
    {

        /// <summary>
        /// create new payroll repository
        /// </summary>
        private static IPayrollRepository payrollRepository = new DummyPayrollRepository();

        /// <summary>
        /// create new dialog service
        /// </summary>
        private static IDialogService dialog = new DummyDialogService(new DummyDialogCoordinator(), null);

        /// <summary>
        /// create new task manager
        /// </summary>
        private static ITaskManager taskManager = new TaskManager(dialog);

        /// <summary>
        /// create new payroll model
        /// </summary>
        private Payroll model = new Payroll(100, 100, DateTime.Now, 800, 80);

        private AllPayrollViewModel viewModel;

        public AllPayrollViewModelTest()
        {

            // create new all payroll viewmodel
            viewModel = new AllPayrollViewModel();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
