using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS499.TCMS.Model;
using CS499.TCMS.DataAccess;

namespace CS499.TCMS.DataAccessUnitTests
{
    [TestClass]
    public class DataAccess_Part_Tests
    {
        [TestMethod]
        public void PartInsertTest()
        {
            Part testPart = new Part(123456, "A part.", 13245, 25.00, 10, 200);

            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IPartRepository partRepository = factory.Create<IPartRepository>();

           partRepository.Insert(testPart);
        }

        [TestMethod]
        public void PartGetSingleTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IPartRepository partRepository = factory.Create<IPartRepository>();

            Part returnPart = partRepository.getSingle(1);

            Assert.IsTrue(returnPart.IsValid);
            System.Diagnostics.Debug.Print(returnPart.PartID.ToString());
            System.Diagnostics.Debug.Print(returnPart.PartDescription);
            System.Diagnostics.Debug.Print(returnPart.PartNumber.ToString());
            System.Diagnostics.Debug.Print(returnPart.PartPrice.ToString());
            System.Diagnostics.Debug.Print(returnPart.PartWeight.ToString());
            System.Diagnostics.Debug.Print(returnPart.QuantityInStock.ToString());
        }
        
        [TestMethod]
        public void PartGetAllTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IPartRepository vehicleRepository = factory.Create<IPartRepository>();

            foreach (Part x in vehicleRepository.getAll())
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.PartID.ToString());
                System.Diagnostics.Debug.Print(x.PartDescription);
                System.Diagnostics.Debug.Print(x.PartNumber.ToString());
                System.Diagnostics.Debug.Print(x.PartPrice.ToString());
                System.Diagnostics.Debug.Print(x.PartWeight.ToString());
                System.Diagnostics.Debug.Print(x.QuantityInStock.ToString());
            }
        }

        [TestMethod]
        public void PartDeleteTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IPartRepository partRepository = factory.Create<IPartRepository>();

            Part delPart = partRepository.getSingle(3);

            partRepository.Delete(delPart);
        }

        [TestMethod]
        public void PartUpdateTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IPartRepository partRepository = factory.Create<IPartRepository>();

            Part updatePart = new Part(1, "Z.Part", 12335, 15.50, 8.5, 3);

           partRepository.Update(updatePart);
        }
        
        [TestMethod]
        public void PartGetAllByAvailabilityTest()
        {
            RepositoryFactory factory = new RepositoryFactory("cs_499_tcms", "johnsza");
            IPartRepository vehicleRepository = factory.Create<IPartRepository>();

            foreach (Part x in vehicleRepository.getPartsByAvailability())
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.PartID.ToString());
                System.Diagnostics.Debug.Print(x.PartDescription);
                System.Diagnostics.Debug.Print(x.PartNumber.ToString());
                System.Diagnostics.Debug.Print(x.PartPrice.ToString());
                System.Diagnostics.Debug.Print(x.PartWeight.ToString());
                System.Diagnostics.Debug.Print(x.QuantityInStock.ToString());
            }
        }
    }
}
