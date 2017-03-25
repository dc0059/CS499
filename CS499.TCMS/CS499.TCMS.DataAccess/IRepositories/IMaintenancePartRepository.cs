using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    public interface IMaintenancePartRepository : IRepository<MaintenancePart>
    {
        MaintenancePart GetSingle(long MaintenancePartID);

        IEnumerable<MaintenancePart> GetPartByMaintenanceRecord(long RecordID);
        IEnumerable<MaintenancePart> GetPartByPart(long PartID);

        void Delete(long MaintenancePartID);
        void DeleteByMaintenanceRecord(long RecordID);
        void DeleteByPart(long PartID);
    }
}
