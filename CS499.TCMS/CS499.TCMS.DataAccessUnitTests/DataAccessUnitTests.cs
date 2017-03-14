using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS499.TCMS.DataAccess;

namespace CS499.TCMS.DataAccessUnitTests
{
    [TestClass]
    public class DataAccessUnitTests
    {
        [TestMethod]
        public void TestUserInsert()
        {
            Model.User testUser = new Model.User(123456, "jadams63", "James", "William", "Adams", "495 Trevor Lane", "Macon", "GA", 31201, 7063156775, 7063156775,
                "jwadams@gmail.com", 30000.00, new DateTime(2012, 6, 18), 1234, "Store A", "Technician", true, "stuff", "other stuff");

            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IUserRepository userRepository = factory.Create<IUserRepository>();

            userRepository.Insert(testUser);
        }
    }
}
