using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS499.TCMS.Model;

namespace CS499.TCMS.ModelUnitTest
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void UserModelTest()
        {
            User testUser = new User(123456, "jadams63", "James", "William", "Adams", "495 Trevor Lane", "Macon", "GA", 31201, "7063156775", "7063156775",
                "jwadams@gmail.com", 30000.00, new DateTime(2012, 6, 18), Enums.AccessLevel.Full, "Store A", "Technician", true, "stuff", "otherstuff");

            Assert.IsTrue(testUser.IsValid);
        }

        [TestMethod]
        public void VehicleModelTest()
        {
            Vehicle testVehicle = new Vehicle(12345, "Mercedes-Benz", 2004, "Actros",  Enums.TruckMaxCapacity.class8, 17500);
            Assert.IsTrue(testVehicle.IsValid);
        }

        [TestMethod]
        public void MaintenanceRecordModelTest()
        {
            MaintenanceRecord testRecord = new MaintenanceRecord(12345, 1234, new DateTime(2008, 4, 12), "Stuff happened.");
            Assert.IsTrue(testRecord.IsValid);
        }

        [TestMethod]
        public void MaintenanceRecordDetailsModelTest()
        {
            MaintenanceRecordDetail testDetail = new MaintenanceRecordDetail(123456, 12345, 12345, "Stuff happened.", new DateTime(2010, 8, 14));
            Assert.IsTrue(testDetail.IsValid);
        }

        [TestMethod]
        public void MaintenancePartModelTest()
        {
            MaintenancePart testMainPart = new MaintenancePart(1234, 1, 12345, 123456);
            Assert.IsTrue(testMainPart.IsValid);
        }

        [TestMethod]
        public void ManifestModelTest()
        {
            Manifest testManifest = new Manifest(12345, "Outgoing", 12345, new DateTime(2012, 10, 2), new DateTime(2012, 10, 4), true, 20000.00, 123456);
            Assert.IsTrue(testManifest.IsValid);
        }

        [TestMethod]
        public void PurchaseOrderModelTest()
        {
            PurchaseOrder testOrder = new PurchaseOrder(12345, 123, 905, 438, 12345, false);
            Assert.IsTrue(testOrder.IsValid);
        }

        [TestMethod]
        public void PurchaseItemModelTest()
        {
            PurchaseItem testItem = new PurchaseItem(12345, 12345, 6, 123456, "Some");
            Assert.IsTrue(testItem.IsValid);
        }

        [TestMethod]
        public void PartModelTest()
        {
            Part testPart = new Part(123456, "A part.", 13245, 25.00, 10, 200);
            Assert.IsTrue(testPart.IsValid);
        }

        [TestMethod]
        public void BusinessPartnerModelTest()
        {
            BusinessPartner testPartner = new BusinessPartner(496, "Company A", "123 Main Street", "Nashville", "TN", 48293, "4125550122");
            Assert.IsTrue(testPartner.IsValid);
        }

        [TestMethod]
        public void PayrollModelTest()
        {
            Payroll testPayroll = new Payroll(12345, 123456, new DateTime(2016, 11, 8), 3560.52, 80);
            Assert.IsTrue(testPayroll.IsValid);
        }
    }
}