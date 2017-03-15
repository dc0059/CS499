using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS499.TCMS.Model;
using CS499.TCMS.DataAccess;

namespace CS499.TCMS.DataAccessUnitTests
{
    [TestClass]
    public class DataAccessTest
    {
        [TestMethod]
        public void UserInsertTest()
        {
            User testUser = new User(1, "jadams63", "James", "William", "Adams", "495 Trevor Lane", "Macon", "GA", 31201, "7063156775", "7063156775",
               "jwadams@gmail.com", 30000.00, new DateTime(2012, 6, 18), 1234, "Store A", "Technician", true, "stuff", "other stuff");

            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IUserRepository userRepository = factory.Create<IUserRepository>();

            userRepository.Insert(testUser);
        }

        [TestMethod]
        public void UserGetSingleTest()
        {

            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IUserRepository userRepository = factory.Create<IUserRepository>();

            User returnUser = userRepository.getSingle(123464);

            Assert.IsTrue(returnUser.IsValid);
        }

        [TestMethod]
        public void UserGetAllTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IUserRepository userRepository = factory.Create<IUserRepository>();

            User testUser1 = new User(123456, "jadams63", "James", "William", "Adams", "495 Trevor Lane", "Macon", "GA", 31201, "7063156775", "7063156775",
               "jwadams@gmail.com", 30000.00, new DateTime(2012, 6, 18), 1234, "Store A", "Technician", true, "stuff", "other stuff");
            User testUser2 = new User(123456, "jadams63", "James", "William", "Adams", "495 Trevor Lane", "Macon", "GA", 31201, "7063156775", "7063156775",
               "jwadams@gmail.com", 30000.00, new DateTime(2012, 6, 18), 1234, "Store A", "Technician", true, "stuff", "other stuff");

            userRepository.Insert(testUser1);
            userRepository.Insert(testUser2);

            foreach(User x in userRepository.getAll())
            {
                Assert.IsTrue(x.IsValid);
            }
        }

        [TestMethod]
        public void UserDeleteTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IUserRepository userRepository = factory.Create<IUserRepository>();

            User delUser = userRepository.getSingle(123469);

            userRepository.Delete(delUser);
        }

        [TestMethod]
        public void UserUpdateTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IUserRepository userRepository = factory.Create<IUserRepository>();

            User updateUser = new User(123464, "Johnsza", "Zach", "Taylor", "Johnson", "495 Trevor Lane", "Macon", "GA", 31201, "7063156775", "7063156775",
               "johnsza@gmail.com", 30000.00, new DateTime(2012, 6, 18), 1234, "Store A", "CEO", false, "stuff", "other stuff");

            userRepository.Update(updateUser);
        }

        [TestMethod]
        public void UserGetSingleNameTest()
        {

            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IUserRepository userRepository = factory.Create<IUserRepository>();

            User returnUser = userRepository.getSingleByName("Zach", "Taylor", "Johnson");

            Assert.IsTrue(returnUser.IsValid);
        }

        [TestMethod]
        public void GetUserByJobAssignTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IUserRepository userRepository = factory.Create<IUserRepository>();

            User testUser1 = new User(123456, "jadams63", "James", "William", "Adams", "495 Trevor Lane", "Macon", "GA", 31201, "7063156775", "7063156775",
               "jwadams@gmail.com", 30000.00, new DateTime(2012, 6, 18), 1233, "Store A", "Technician", true, "stuff", "other stuff");

            userRepository.Insert(testUser1);

            foreach (User x in userRepository.getUsersByJobAssignment(1234))
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.JobID.ToString());
            }
        }


    }
}
