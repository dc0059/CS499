using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS499.TCMS.Model;
using CS499.TCMS.DataAccess;
using CS499.TCMS.DataAccess.IRepositories;

namespace CS499.TCMS.DataAccessUnitTests
{
    [TestClass]
    public class DataAccess_BusinessPartner_Test
    {
        [TestMethod]
        public void BusinessPartInsertTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IBusinessPartnerRepository businessPartRepo = factory.Create<IBusinessPartnerRepository>();

            BusinessPartner insertPartner = new BusinessPartner(1234, "Some Store", "1414 SomewhereLane", "Somewhereville",
                "MA", 56569, "4561237879");

            businessPartRepo.Insert(insertPartner);
        }

        [TestMethod]
        public void BusinessPartGetSingleTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IBusinessPartnerRepository businessPartRepo = factory.Create<IBusinessPartnerRepository>();

            BusinessPartner returnPartner = businessPartRepo.GetSingle(1);

            Assert.IsTrue(returnPartner.IsValid);
            System.Diagnostics.Debug.Print(returnPartner.CompanyID.ToString());
            System.Diagnostics.Debug.Print(returnPartner.CompanyName);
            System.Diagnostics.Debug.Print(returnPartner.Address);
            System.Diagnostics.Debug.Print(returnPartner.City);
            System.Diagnostics.Debug.Print(returnPartner.State);
            System.Diagnostics.Debug.Print(returnPartner.ZipCode.ToString());
            System.Diagnostics.Debug.Print(returnPartner.PhoneNumber);
        }

        [TestMethod]
        public void BusinessPartGetAllTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IBusinessPartnerRepository businessPartRepo = factory.Create<IBusinessPartnerRepository>();

            foreach(BusinessPartner x in businessPartRepo.GetAll())
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.CompanyID.ToString());
                System.Diagnostics.Debug.Print(x.CompanyName);
                System.Diagnostics.Debug.Print(x.Address);
                System.Diagnostics.Debug.Print(x.City);
                System.Diagnostics.Debug.Print(x.State);
                System.Diagnostics.Debug.Print(x.ZipCode.ToString());
                System.Diagnostics.Debug.Print(x.PhoneNumber);
            }
        }

        [TestMethod]
        public void BusinessPartUpdateTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IBusinessPartnerRepository businessPartRepo = factory.Create<IBusinessPartnerRepository>();

            BusinessPartner updatePartner = new BusinessPartner(2, "Other Store", "1414 OtherLane", "Otherville",
                "CA", 42458, "1234567894");

            businessPartRepo.Update(updatePartner);
        }

        [TestMethod]
        public void BusinessPartDeleteTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IBusinessPartnerRepository businessPartRepo = factory.Create<IBusinessPartnerRepository>();

            BusinessPartner delPartner = businessPartRepo.GetSingle(1);

            businessPartRepo.Delete(delPartner);
        }
    }
}
