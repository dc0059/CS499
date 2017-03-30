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
    public class DataAccess_MaintenancePart_Tests
    {
        [TestMethod]
        public void MaintenancePartInsertTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenancePartRepository maintenancePartRepo = factory.Create<IMaintenancePartRepository>();

            MaintenancePart newPart = new MaintenancePart(1234, 1, 2, 2);

            maintenancePartRepo.Insert(newPart);
        }

        [TestMethod]
        public void MaintenancePartGetSingleTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenancePartRepository maintenancePartRepo = factory.Create<IMaintenancePartRepository>();

            MaintenancePart returnPart = maintenancePartRepo.GetSingle(1);

            Assert.IsTrue(returnPart.IsValid);
            System.Diagnostics.Debug.Print(returnPart.MaintenancePartID.ToString());
            System.Diagnostics.Debug.Print(returnPart.Quantity.ToString());
            System.Diagnostics.Debug.Print(returnPart.DetailID.ToString());
            System.Diagnostics.Debug.Print(returnPart.PartID.ToString());
        }

        [TestMethod]
        public void MaintenancePartGetAllTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenancePartRepository maintenancePartRepo = factory.Create<IMaintenancePartRepository>();

            foreach(MaintenancePart x in maintenancePartRepo.GetAll())
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.MaintenancePartID.ToString());
                System.Diagnostics.Debug.Print(x.Quantity.ToString());
                System.Diagnostics.Debug.Print(x.DetailID.ToString());
                System.Diagnostics.Debug.Print(x.PartID.ToString());
            }
        }

        [TestMethod]
        public void MaintenancePartUpdateTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenancePartRepository maintenancePartRepo = factory.Create<IMaintenancePartRepository>();

            MaintenancePart updatePart = new MaintenancePart(1, 200, 2, 2);

            maintenancePartRepo.Update(updatePart);
        }

        [TestMethod]
        public void MaintenancePartDeleteTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenancePartRepository maintenancePartRepo = factory.Create<IMaintenancePartRepository>();

            maintenancePartRepo.Delete(maintenancePartRepo.GetSingle(1));
        }

        [TestMethod]
        public void MaintenancePartDeleteByRecordTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenancePartRepository maintenancePartRepo = factory.Create<IMaintenancePartRepository>();

            maintenancePartRepo.DeleteByMaintenanceRecord(2);
        }
    }
}
