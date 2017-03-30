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
    public class DataAccess_PurchaseItem_Test
    {
        [TestMethod]
        public void PurchaseItemInsertTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPurchaseItemRepository purchaseItemRepo = factory.Create<IPurchaseItemRepository>();

            PurchaseItem newItem = new PurchaseItem(123, 4, 3, 2);

            purchaseItemRepo.Insert(newItem);
        }

        [TestMethod]
        public void PurchaseItemGetSingleTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPurchaseItemRepository purchaseItemRepo = factory.Create<IPurchaseItemRepository>();

            PurchaseItem getItem = purchaseItemRepo.GetSingle(1);

            Assert.IsTrue(getItem.IsValid);
            System.Diagnostics.Debug.Print(getItem.ItemID.ToString());
            System.Diagnostics.Debug.Print(getItem.OrderID.ToString());
            System.Diagnostics.Debug.Print(getItem.Quantity.ToString());
            System.Diagnostics.Debug.Print(getItem.PartID.ToString());
        }

        [TestMethod]
        public void PurchaseItemGetAllTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPurchaseItemRepository purchaseItemRepo = factory.Create<IPurchaseItemRepository>();

            foreach(PurchaseItem x in purchaseItemRepo.GetAll())
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.ItemID.ToString());
                System.Diagnostics.Debug.Print(x.OrderID.ToString());
                System.Diagnostics.Debug.Print(x.Quantity.ToString());
                System.Diagnostics.Debug.Print(x.PartID.ToString());
            }
        }

        [TestMethod]
        public void PurchaseItemUpdateTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPurchaseItemRepository purchaseItemRepo = factory.Create<IPurchaseItemRepository>();

            PurchaseItem updateItem = new PurchaseItem(1, 4, 250, 2);

            purchaseItemRepo.Update(updateItem);
        }

        [TestMethod]
        public void PurchaseItemDeleteTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPurchaseItemRepository purchaseItemRepo = factory.Create<IPurchaseItemRepository>();

            PurchaseItem delItem = purchaseItemRepo.GetSingle(1);

            purchaseItemRepo.Delete(delItem);
        }

        [TestMethod]
        public void PurchaseItemGetByOrderIDTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPurchaseItemRepository purchaseItemRepo = factory.Create<IPurchaseItemRepository>();

            foreach (PurchaseItem x in purchaseItemRepo.GetItemsByOrderID(4))
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.ItemID.ToString());
                System.Diagnostics.Debug.Print(x.OrderID.ToString());
                System.Diagnostics.Debug.Print(x.Quantity.ToString());
                System.Diagnostics.Debug.Print(x.PartID.ToString());
            }
        }

        [TestMethod]
        public void PurchaseItemDeleteByOrderTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPurchaseItemRepository purchaseItemRepo = factory.Create<IPurchaseItemRepository>();

            purchaseItemRepo.DeleteItemsByOrderID(4);
        }
    }
}
