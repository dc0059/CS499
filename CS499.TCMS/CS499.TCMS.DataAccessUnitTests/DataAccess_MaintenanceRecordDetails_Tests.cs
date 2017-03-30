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
    public class DataAccess_MaintenanceRecordDetails_Tests
    {
        [TestMethod]
        public void MaintenanceRecordDetailsInsertTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenanceRecordDetailRepository recordDetailsRepo = factory.Create<IMaintenanceRecordDetailRepository>();

            MaintenanceRecordDetail newDetails = new MaintenanceRecordDetail(123456, 2, 1, "Stuff happened.", new DateTime(2010, 8, 14));

            recordDetailsRepo.Insert(newDetails);
        }

        [TestMethod]
        public void MaintenanceRecordDetailsGetSingleTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenanceRecordDetailRepository recordDetailsRepo = factory.Create<IMaintenanceRecordDetailRepository>();

            MaintenanceRecordDetail returnDetails = recordDetailsRepo.GetSingle(1);

            Assert.IsTrue(returnDetails.IsValid);
            System.Diagnostics.Debug.Print(returnDetails.DetailID.ToString());
            System.Diagnostics.Debug.Print(returnDetails.MaintenanceID.ToString());
            System.Diagnostics.Debug.Print(returnDetails.EmployeeID.ToString());
            System.Diagnostics.Debug.Print(returnDetails.RepairDate.ToString());
        }

        [TestMethod]
        public void MaintenanceDetailsGetAllTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenanceRecordDetailRepository recordDetailsRepo = factory.Create<IMaintenanceRecordDetailRepository>();

            foreach(MaintenanceRecordDetail x in recordDetailsRepo.GetAll())
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.DetailID.ToString());
                System.Diagnostics.Debug.Print(x.MaintenanceID.ToString());
                System.Diagnostics.Debug.Print(x.EmployeeID.ToString());
                System.Diagnostics.Debug.Print(x.RepairDate.ToString());
            }
        }

        [TestMethod]
        public void MaintenanceDetailsUpdateTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenanceRecordDetailRepository recordDetailsRepo = factory.Create<IMaintenanceRecordDetailRepository>();

            MaintenanceRecordDetail updateDetails = new MaintenanceRecordDetail(1, 2, 1, "MORE stuff happened.", new DateTime(2016, 6, 30));

            recordDetailsRepo.Update(updateDetails);
        }

        [TestMethod]
        public void MaintenanceDetailsDeleteTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IMaintenanceRecordDetailRepository recordDetailsRepo = factory.Create<IMaintenanceRecordDetailRepository>();

            recordDetailsRepo.Delete(recordDetailsRepo.GetSingle(1));
        }
    }
}
