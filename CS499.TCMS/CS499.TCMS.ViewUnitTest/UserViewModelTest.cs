using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS499.TCMS.View.ViewModels;
using CS499.TCMS.DataAccess;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Services;
using CS499.TCMS.ViewUnitTest.DummyClasses;
using CS499.TCMS.Model;
using CS499.TCMS.DataAccess.IRepositories;

namespace CS499.TCMS.ViewUnitTest
{
    [TestClass]
    public class UserViewModelTest
    {
        [TestMethod]
        public void UserViewModelTesting()
        {

            // create new user repository
            IUserRepository userRepository = new DummyUserRepository();

            // create new task manager
            ITaskManager taskManager = new TaskManager(new DummyDialogService(new DummyDialogCoordinator(), null));

            // create new user model
            User model = new User(123456, "jadams63", "James", "William", "Adams", "495 Trevor Lane", "Macon", "GA", 31201, "7063156775", "7063156775",
                "jwadams@gmail.com", 30000.00, new DateTime(2012, 6, 18), Enums.AccessLevel.Full, "Store A", "Technician", true, "stuff", "otherstuff");

            // create new user viewmodel
            UserViewModel viewModel = new UserViewModel(model, userRepository, taskManager, true);

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
