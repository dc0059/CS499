using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS499.TCMS.Model;

namespace CS499.TCMS.ModelUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UserModelTest()
        {
            User testUser = new User(123456, "jm0062", "Jonathan", "Dean", "Mullen", "603 Willow Crest Lane", "Madison", "AL", 35756, 7063156775, 7063156775,
                "jm0062@uah.edu", 30000, new DateTime(2012, 6, 18), 1234, "Store A", "Technician", true);
            Assert.IsTrue(testUser.IsValid);
        }

        [TestMethod]
        public void VehicleModelTest()
        {
            Vehicle testVehicle = new Vehicle(123456, "Mercedes-Benz", 2004, "Actros", "Class 5", 17500);
            Assert.IsTrue(testVehicle.IsValid);
        }

        [TestMethod]
        public void MaintenanceRecordModelTest()
        {
            MaintenanceRecord testRecord = new MaintenanceRecord(123456, 1234, new DateTime(2014, 4, 12), "Did something.");
            Assert.IsTrue(testRecord.IsValid);
        }

        [TestMethod]
        public void MaintenanceRecordDetailModelTest()
        {
            MaintenanceRecordDetails testDetails = new MaintenanceRecordDetails(123456, 123456, 123456, "Did stuff.", new DateTime(2014, 4, 12));
            Assert.IsTrue(testDetails.IsValid);
        }

        [TestMethod]
        public void MaintenancePartModelTest()
        {
            MaintenancePart testMainPart = new MaintenancePart(1234, 3, 123456, 12345);
            Assert.IsTrue(testMainPart.IsValid);
        }

        [TestMethod]
        public void ManifestModelTest()
        {
            Manifest testManifest = new Manifest(123456, "Outgoing", 12345, new DateTime(2015, 5, 21), new DateTime(2015, 5, 23), true, 20000, 123456);
            Assert.IsTrue(testManifest.IsValid);
        }

        [TestMethod]
        public void PurchaseOrderModelTest()
        {
            PurchaseOrder testOrder = new PurchaseOrder(123456, 1234, 123, 456, 12345);
            Assert.IsTrue(testOrder.IsValid);

        }

        [TestMethod]
        public void PurchaseItemModelTest()
        {

        }

        [TestMethod]
        public void PartModelTest()
        {

        }

        [TestMethod]
        public void BusinessPartnerModelTest()
        {

        }

        [TestMethod]
        public void PayrollModelTest()
        {

        }
    }
}
