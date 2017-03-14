using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    interface IPurchaseItemRepository : IRepository<PurchaseItem>
    {
        PurchaseItem getSingle(long ItemID);

        IEnumerable<PurchaseItem> getItemsByOrderID(long OrderID);
        IEnumerable<PurchaseItem> getItemsByPart(long PartID);

        void Delete(long ItemID);
        void DeleteItemsByOrderID(long OrderID);
        void DeleteItemsByPartID(long PartID);

    }
}
