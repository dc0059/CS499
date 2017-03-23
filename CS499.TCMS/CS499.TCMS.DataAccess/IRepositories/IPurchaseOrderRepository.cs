using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess
{
    public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>, IRepositoryBase
    {
        //PurchaseOrder GetSingle(long OrderID);

        IEnumerable<PurchaseOrder> GetOrderByNumber(long OrderNum);
        IEnumerable<PurchaseOrder> GetOrderBySource(long SourceCompany);
        IEnumerable<PurchaseOrder> GetOrderByDestination(long DestCompany);
        IEnumerable<PurchaseOrder> GetOrderByManifest(long ManifestID);

        void DeleteByNumber(long OrderNum);
        void DeleteBySource(long SourceComp);
        void DeleteByDest(long DestComp);
        void DeleteByManifest(long ManifestID);

    }
}
