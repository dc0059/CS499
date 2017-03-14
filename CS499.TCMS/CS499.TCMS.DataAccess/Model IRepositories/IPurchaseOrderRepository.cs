using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.Model_IRepositories
{
    interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
    {
        PurchaseOrder getSingle(long OrderID);

        IEnumerable<PurchaseOrder> getOrderByNumber(long OrderNum);
        IEnumerable<PurchaseOrder> getOrderBySource(long SourceCompany);
        IEnumerable<PurchaseOrder> getOrderByDestination(long DestCompany);
        IEnumerable<PurchaseOrder> getOrderByManifest(long ManifestID);

        void DeleteByNumber(long OrderNum);
        void DeleteBySource(long SourceComp);
        void DeleteByDest(long DestComp);
        void DeleteByManifest(long ManifestID);

    }
}
