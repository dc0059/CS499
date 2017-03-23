using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    public interface IPurchaseItemRepository : IRepository<PurchaseItem>, IRepositoryBase
    {
        //PurchaseItem GetSingle(long ItemID);

        IEnumerable<PurchaseItem> GetItemsByOrderID(long OrderID);
        IEnumerable<PurchaseItem> GetItemsByPart(long PartID);

        void Delete(long ItemID);
        void DeleteItemsByOrderID(long OrderID);
        void DeleteItemsByPartID(long PartID);

    }
}
