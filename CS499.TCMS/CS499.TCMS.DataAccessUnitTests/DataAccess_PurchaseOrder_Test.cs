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
    public class DataAccess_PurchaseOrder_Test
    {
        [TestMethod]
        public void PurchaseOrderInsertTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPurchaseOrderRepository purchaseOrderRepo = factory.Create<IPurchaseOrderRepository>();

            PurchaseOrder newOrder = new PurchaseOrder(12345, 123, 2, 3, 4, true);

            purchaseOrderRepo.Insert(newOrder);
        }

        [TestMethod]
        public void PurchaseOrderGetSingleTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPurchaseOrderRepository purchaseOrderRepo = factory.Create<IPurchaseOrderRepository>();

            PurchaseOrder getOrder = purchaseOrderRepo.GetSingle(3);

            Assert.IsTrue(getOrder.IsValid);
            System.Diagnostics.Debug.Print(getOrder.OrderID.ToString());
            System.Diagnostics.Debug.Print(getOrder.OrderNumber.ToString());
            System.Diagnostics.Debug.Print(getOrder.SourceID.ToString());
            System.Diagnostics.Debug.Print(getOrder.DestinationID.ToString());
            System.Diagnostics.Debug.Print(getOrder.ManifestID.ToString());
            System.Diagnostics.Debug.Print(getOrder.PaymentMade.ToString());
        }

        [TestMethod]
        public void PurchaseOrderGetAllTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPurchaseOrderRepository purchaseOrderRepo = factory.Create<IPurchaseOrderRepository>();

            foreach (PurchaseOrder x in purchaseOrderRepo.GetAll())
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.OrderID.ToString());
                System.Diagnostics.Debug.Print(x.OrderNumber.ToString());
                System.Diagnostics.Debug.Print(x.SourceID.ToString());
                System.Diagnostics.Debug.Print(x.DestinationID.ToString());
                System.Diagnostics.Debug.Print(x.ManifestID.ToString());
                System.Diagnostics.Debug.Print(x.PaymentMade.ToString());
            }
        }

        [TestMethod]
        public void PurchaseOrderUpdateTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPurchaseOrderRepository purchaseOrderRepo = factory.Create<IPurchaseOrderRepository>();

            PurchaseOrder updateOrder = new PurchaseOrder(3, 456, 2, 3, 4, false);

            purchaseOrderRepo.Update(updateOrder);
        }

        [TestMethod]
        public void PurchaseOrderDeleteTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPurchaseOrderRepository purchaseOrderRepo = factory.Create<IPurchaseOrderRepository>();

            PurchaseOrder delOrder = purchaseOrderRepo.GetSingle(3);

            purchaseOrderRepo.Delete(delOrder);
        }
    }
}
