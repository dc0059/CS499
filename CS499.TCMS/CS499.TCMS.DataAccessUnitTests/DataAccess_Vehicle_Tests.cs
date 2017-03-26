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
    public class DataAccess_Vehicle_Tests
    {
        [TestMethod]
        public void VehicleInsertTest()
        {
            Vehicle testVehicle = new Vehicle(12345, "Mercedes-Benz", 2004, "Actros", Enums.TruckMaxCapacity.class5, 17500);

            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IVehicleRepository vehicleRepository = factory.Create<IVehicleRepository>();

            vehicleRepository.Insert(testVehicle);
        }

        [TestMethod]
        public void VehicleGetSingleTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IVehicleRepository vehicleRepository = factory.Create<IVehicleRepository>();

            Vehicle returnVehicle = vehicleRepository.GetSingle(1);

            Assert.IsTrue(returnVehicle.IsValid);
            System.Diagnostics.Debug.Print(returnVehicle.VehicleID.ToString());
            System.Diagnostics.Debug.Print(returnVehicle.Brand);
            System.Diagnostics.Debug.Print(returnVehicle.Year.ToString());
            System.Diagnostics.Debug.Print(returnVehicle.Model);
            System.Diagnostics.Debug.Print(returnVehicle.VehicleType.ToString());
            System.Diagnostics.Debug.Print(returnVehicle.Capacity.ToString());
        }

        [TestMethod]
        public void VehicleGetAllTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IVehicleRepository vehicleRepository = factory.Create<IVehicleRepository>();

            foreach (Vehicle x in vehicleRepository.GetAll())
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.VehicleID.ToString());
                System.Diagnostics.Debug.Print(x.Brand);
                System.Diagnostics.Debug.Print(x.Year.ToString());
                System.Diagnostics.Debug.Print(x.Model);
                System.Diagnostics.Debug.Print(x.VehicleType.ToString());
                System.Diagnostics.Debug.Print(x.Capacity.ToString());
            }
        }

        [TestMethod]
        public void VehicleDeleteTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IVehicleRepository vehicleRepository = factory.Create<IVehicleRepository>();

            Vehicle delVehicle = vehicleRepository.GetSingle(1);

            vehicleRepository.Delete(delVehicle);
        }

        [TestMethod]
        public void VehicleUpdateTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IVehicleRepository vehicleRepository = factory.Create<IVehicleRepository>();

            Vehicle updateVehicle = new Vehicle(1, "Chevy", 2016, "Thing", Enums.TruckMaxCapacity.class3, 9000);

            vehicleRepository.Update(updateVehicle);
        }

        [TestMethod]
        public void VehicleGetAllBySpecsTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IVehicleRepository vehicleRepository = factory.Create<IVehicleRepository>();

            foreach (Vehicle x in vehicleRepository.GetVehiclesBySpecs("Mercedes-Benz", "Actros", 2004))
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.VehicleID.ToString());
                System.Diagnostics.Debug.Print(x.Brand);
                System.Diagnostics.Debug.Print(x.Year.ToString());
                System.Diagnostics.Debug.Print(x.Model);
                System.Diagnostics.Debug.Print(x.VehicleType.ToString());
                System.Diagnostics.Debug.Print(x.Capacity.ToString());
            }
        }

        [TestMethod]
        public void VehicleGetAllByVehicleTypeTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IVehicleRepository vehicleRepository = factory.Create<IVehicleRepository>();

            foreach (Vehicle x in vehicleRepository.GetVehiclesByType("Class 5"))
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.VehicleID.ToString());
                System.Diagnostics.Debug.Print(x.Brand);
                System.Diagnostics.Debug.Print(x.Year.ToString());
                System.Diagnostics.Debug.Print(x.Model);
                System.Diagnostics.Debug.Print(x.VehicleType.ToString());
                System.Diagnostics.Debug.Print(x.Capacity.ToString());
            }
        }

        [TestMethod]
        public void VehicleGetAllByVehicleCapacityTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IVehicleRepository vehicleRepository = factory.Create<IVehicleRepository>();

            foreach (Vehicle x in vehicleRepository.GetVehiclesByCapacity(17500))
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.VehicleID.ToString());
                System.Diagnostics.Debug.Print(x.Brand);
                System.Diagnostics.Debug.Print(x.Year.ToString());
                System.Diagnostics.Debug.Print(x.Model);
                System.Diagnostics.Debug.Print(x.VehicleType.ToString());
                System.Diagnostics.Debug.Print(x.Capacity.ToString());
            }
        }

        [TestMethod]
        public void VehicleDeleteBySpecsTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IVehicleRepository vehicleRepository = factory.Create<IVehicleRepository>();

            vehicleRepository.DeleteBySpecs("Mercedes-Benz", "Actros", 2004);
        }

        [TestMethod]
        public void VehicleDeleteByTypeTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IVehicleRepository vehicleRepository = factory.Create<IVehicleRepository>();

            vehicleRepository.DeleteByType("Class 5");
        }

        [TestMethod]
        public void VehicleDeleteByCapacityTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IVehicleRepository vehicleRepository = factory.Create<IVehicleRepository>();

            vehicleRepository.DeleteByCapacity(17500);
        }
    }
}
