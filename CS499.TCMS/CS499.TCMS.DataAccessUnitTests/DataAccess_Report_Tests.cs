using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS499.TCMS.DataAccess;
using CS499.TCMS.DataAccess.IRepositories;
using System.Data;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccessUnitTests
{
    [TestClass]
    public class DataAccess_Report_Tests
    {
        [TestMethod]
        public void PayrollDataTableTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IReportRepository reportRepo = factory.Create<IReportRepository>();

            DataTable table = reportRepo.GetPayrollReport(new DateTime(2017,3,25), new DateTime(2017,4,6));

            foreach(DataRow row in table.Rows)
            {
                System.Diagnostics.Debug.Print("");
                for(int x = 0; x < table.Columns.Count; x++)
                {
                    System.Diagnostics.Debug.Print(row[x].ToString() + " ");
                }
            }
        }

        [TestMethod]
        public void VehicleMaintenanceReportTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IReportRepository reportRepo = factory.Create<IReportRepository>();

            Vehicle TestVehicle = new Vehicle(4, "Mercedes-Benz", 2004, "Actros", Enums.TruckMaxCapacity.class_5, 17500);
            DataTable table = reportRepo.GetVehicleMaintenanceReport(TestVehicle);

            foreach (DataRow row in table.Rows)
            {
                System.Diagnostics.Debug.Print("");
                for (int x = 0; x < table.Columns.Count; x++)
                {
                    System.Diagnostics.Debug.Print(row[x].ToString() + " ");
                }
            }
        }

        [TestMethod]
        public void GetOutgoingShipmentReportTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IReportRepository reportRepo = factory.Create<IReportRepository>();

            DataTable table = reportRepo.GetOutgoingShipmentReport();

            Assert.IsTrue(table.Rows.Count > 0);
        }

        [TestMethod]
        public void GetMaintenanceReport()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IReportRepository reportRepo = factory.Create<IReportRepository>();

            DataTable table = reportRepo.GetMaintenanceCostReport(DateTime.Now.AddMonths(-2), DateTime.Now);

            Assert.IsTrue(table.Rows.Count > 0);
        }
    }
}
