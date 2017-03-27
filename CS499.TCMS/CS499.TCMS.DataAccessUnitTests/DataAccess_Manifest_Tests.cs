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
    public class DataAccess_Manifest_Tests
    {
        [TestMethod]
        public void ManifestInsertTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IManifestRepository manifestRepo = factory.Create<IManifestRepository>();

            Manifest insertMan = new Manifest(12345, "Outgoing", 4, new DateTime(2012, 10, 2), 
                new DateTime(2012, 10, 4), true, 20000.00, 3);

            manifestRepo.Insert(insertMan);
        }

        [TestMethod]
        public void ManifestGetSingleTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IManifestRepository manifestRepo = factory.Create<IManifestRepository>();

            Manifest returnManifest = manifestRepo.GetSingle(3);

            Assert.IsTrue(returnManifest.IsValid);
            System.Diagnostics.Debug.Print(returnManifest.ManifestID.ToString());
            System.Diagnostics.Debug.Print(returnManifest.ShipmentType);
            System.Diagnostics.Debug.Print(returnManifest.VehicleID.ToString());
            System.Diagnostics.Debug.Print(returnManifest.DepartureTime.ToString());
            System.Diagnostics.Debug.Print(returnManifest.ETA.ToString());
            System.Diagnostics.Debug.Print(returnManifest.Arrived.ToString());
            System.Diagnostics.Debug.Print(returnManifest.ShippingCost.ToString());
            System.Diagnostics.Debug.Print(returnManifest.EmployeeID.ToString());
        }

        [TestMethod]
        public void ManifestGetAllTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IManifestRepository manifestRepo = factory.Create<IManifestRepository>();

            foreach(Manifest x in manifestRepo.GetAll())
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.ManifestID.ToString());
                System.Diagnostics.Debug.Print(x.ShipmentType);
                System.Diagnostics.Debug.Print(x.VehicleID.ToString());
                System.Diagnostics.Debug.Print(x.DepartureTime.ToString());
                System.Diagnostics.Debug.Print(x.ETA.ToString());
                System.Diagnostics.Debug.Print(x.Arrived.ToString());
                System.Diagnostics.Debug.Print(x.ShippingCost.ToString());
                System.Diagnostics.Debug.Print(x.EmployeeID.ToString());
            }
        }

        [TestMethod]
        public void ManifestUpdateTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IManifestRepository manifestRepo = factory.Create<IManifestRepository>();

            Manifest updateManifest = new Manifest(3, "Incoming", 4, new DateTime(2017, 4, 1),
                new DateTime(2017, 10, 4), false, 150000.50, 3);

            manifestRepo.Update(updateManifest);
        }

        [TestMethod]
        public void ManifestDeleteTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IManifestRepository manifestRepo = factory.Create<IManifestRepository>();

            Manifest delManifest = manifestRepo.GetSingle(3);

            manifestRepo.Delete(delManifest);
        }
    }
}
