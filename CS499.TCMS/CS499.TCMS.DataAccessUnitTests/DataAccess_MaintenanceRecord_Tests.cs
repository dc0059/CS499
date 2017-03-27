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
    public class DataAccess_MaintenanceRecord_Tests
    {
        [TestMethod]
        public void MaintenanceRecordInsertTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenanceRecordRepository mainRecordRepo = factory.Create<IMaintenanceRecordRepository>();

            MaintenanceRecord newRecord = new MaintenanceRecord(12345, 4, new DateTime(2008, 4, 12), "Stuff happened.");

            mainRecordRepo.Insert(newRecord);
        }

        [TestMethod]
        public void MaintenanceRecordGetSingleTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenanceRecordRepository mainRecordRepo = factory.Create<IMaintenanceRecordRepository>();

            MaintenanceRecord returnRecord = mainRecordRepo.GetSingle(1);

            Assert.IsTrue(returnRecord.IsValid);
            System.Diagnostics.Debug.Print(returnRecord.MaintenanceID.ToString());
            System.Diagnostics.Debug.Print(returnRecord.VehicleID.ToString());
            System.Diagnostics.Debug.Print(returnRecord.MaintenanceDate.ToString());
            System.Diagnostics.Debug.Print(returnRecord.MaintenanceDescription);
        }

        [TestMethod]
        public void MaintenanceRecordGetAllTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenanceRecordRepository mainRecordRepo = factory.Create<IMaintenanceRecordRepository>();

            foreach(MaintenanceRecord x in mainRecordRepo.GetAll())
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.MaintenanceID.ToString());
                System.Diagnostics.Debug.Print(x.VehicleID.ToString());
                System.Diagnostics.Debug.Print(x.MaintenanceDate.ToString());
                System.Diagnostics.Debug.Print(x.MaintenanceDescription);
            }
        }

        [TestMethod]
        public void MaintenanceRecordUpdateTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenanceRecordRepository mainRecordRepo = factory.Create<IMaintenanceRecordRepository>();

            MaintenanceRecord updateRecord = new MaintenanceRecord(1, 4, new DateTime(2017, 11, 9), "New Stuff happened");

            mainRecordRepo.Update(updateRecord);
        }
        
        [TestMethod]
        public void MaintenanceRecordDeleteTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenanceRecordRepository mainRecordRepo = factory.Create<IMaintenanceRecordRepository>();

            MaintenanceRecord delRecord = mainRecordRepo.GetSingle(1);

            mainRecordRepo.Delete(delRecord);
        }
    }
}
