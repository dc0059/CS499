using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS499.TCMS.Model;
using CS499.TCMS.DataAccess.IRepositories;
using CS499.TCMS.DataAccess;

namespace CS499.TCMS.DataAccessUnitTests
{
    [TestClass]
    public class DataAccess_User_Test
    {
        [TestMethod]
        public void UserInsertTest()
        {
            User testUser = new User(000, "ztj0002", "Zach", "Taylor", "Johnson", "4545 Over There", "Guntersville", "AL", 35976, "7063156775", "7063156775",
               "ztj0002@uah.edu", 45, new DateTime(2012, 6, 18), Enums.AccessLevel.ShippingData, "Store A", "Technician", true, "stuff", "other stuff");

            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IUserRepository userRepository = factory.Create<IUserRepository>();

            userRepository.Insert(testUser);
        }

        [TestMethod]
        public void UserGetSingleTest()
        {

            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IUserRepository userRepository = factory.Create<IUserRepository>();

            User returnUser = userRepository.GetSingle(1);

            Assert.IsTrue(returnUser.IsValid);
        }

        [TestMethod]
        public void UserGetAllTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IUserRepository userRepository = factory.Create<IUserRepository>();

            foreach(User x in userRepository.GetAll())
            {
                Assert.IsTrue(x.IsValid);
            }
        }

        [TestMethod]
        public void UserDeleteTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IUserRepository userRepository = factory.Create<IUserRepository>();

            User delUser = userRepository.GetSingle(2);

            userRepository.Delete(delUser);
        }

        [TestMethod]
        public void UserUpdateTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IUserRepository userRepository = factory.Create<IUserRepository>();

            User updateUser = new User(1, "Johnsza", "Zach", "Taylor", "Johnson", "495 Trevor Lane", "Macon", "GA", 31201, "7063156775", "7063156775",
               "johnsza@gmail.com", 45, new DateTime(2012, 6, 18), Enums.AccessLevel.MaintenanceData, "Store A", "Bro", false, "stuff", "other stuff");

            userRepository.Update(updateUser);
        }

        [TestMethod]
        public void UserGetSingleNameTest()
        {

            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IUserRepository userRepository = factory.Create<IUserRepository>();

            User returnUser = userRepository.GetSingleByName("Zach", "Taylor", "Johnson");

            Assert.IsTrue(returnUser.IsValid);
        }

        [TestMethod]
        public void GetUserByUserNameTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IUserRepository userRepository = factory.Create<IUserRepository>();

            User returnUser = userRepository.GetUserByUserName("Johnsza");

            Assert.IsTrue(returnUser.IsValid);
            System.Diagnostics.Debug.Print(returnUser.UserName);
        }
    }
}
