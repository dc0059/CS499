using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS499.TCMS.View.ViewModels;
using CS499.TCMS.DataAccess;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Services;
using CS499.TCMS.ViewUnitTest.DummyClasses;
using CS499.TCMS.Model;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;
using CS499.TCMS.DataAccess.IRepositories;

namespace CS499.TCMS.ViewUnitTest
{
    [TestClass]
    public class AllUserViewModelTest
    {

        /// <summary>
        /// create new user repository
        /// </summary>
        private static IUserRepository userRepository = new DummyUserRepository();

        /// <summary>
        /// create new dialog service
        /// </summary>
        private static IDialogService dialog = new DummyDialogService(new DummyDialogCoordinator(), null);

        /// <summary>
        /// create new task manager
        /// </summary>
        private static ITaskManager taskManager = new TaskManager(dialog);

        /// <summary>
        /// create new user model
        /// </summary>
        private User model = new User(123456, "jadams63", "James", "William", "Adams", "495 Trevor Lane", "Macon", "GA", 31201, "7063156775", "7063156775",
            "jwadams@gmail.com", 30000.00, new DateTime(2012, 6, 18), Enums.AccessLevel.Full, "Store A", "Technician", true, "stuff", "otherstuff");

        private AllUserViewModel viewModel;

        public AllUserViewModelTest()
        {

            // create new all user viewmodel
            viewModel = new AllUserViewModel(dialog, taskManager, userRepository);

            // register add workspace function with messanger
            viewModel.MessengerInstance.Register<NotificationMessage<WorkspaceViewModel>>(this, (n) => this.AddWorspace(n));

        }

        private void AddWorspace(NotificationMessage<WorkspaceViewModel> notification)
        {

            WorkspaceViewModel viewModel = notification.Content as WorkspaceViewModel;
            if (viewModel.IsNew)
            {
                Debug.Print("Create new viewmodel");
            }
            else
            {
                Debug.Print("Edit viewmodel");
            }

        }

        [TestMethod]
        public void AllUserViewModelTesting()
        {

            // new test
            this.NewTest();

            // edit test
            this.EditTest();

            // delete test
            this.DeleteTest();

        }
        
        public void NewTest()
        {

            if (viewModel.CommandNew.CanExecute(null))
            {
                viewModel.CommandNew.Execute(null);
            }
            else
            {
                Assert.Fail("Failed to create new ViewModel");
            }

            
        }
        
        public void EditTest()
        {

            // add new user viewmodel to the collection
            UserViewModel user = new UserViewModel(model, userRepository, taskManager, false);
            viewModel.ViewModels.AddItem(user);

            // set selected viewmodel
            viewModel.SelectedViewModel = user;

            if (viewModel.CommandEdit.CanExecute(null))
            {
                viewModel.CommandEdit.Execute(null);
            }
            else
            {
                Assert.Fail("Failed to edit ViewModel");
            }

        }

        public void DeleteTest()
        {

            if (viewModel.CommandDelete.CanExecute(null))
            {
                viewModel.CommandDelete.Execute(null);
                Assert.AreEqual(0, viewModel.ViewModels.Count);
            }
            else
            {
                Assert.Fail("Failed to delete ViewModel");
            }

        }

    }
}
