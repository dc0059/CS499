using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    interface IMaintenancePartRepository : IRepository<MaintenancePart>
    {
        MaintenancePart getSingle(long MaintenancePartID);

        IEnumerable<MaintenancePart> getPartByMaintenanceRecord(long RecordID);
        IEnumerable<MaintenancePart> getPartByPart(long PartID);

        void Delete(long MaintenancePartID);
        void DeleteByMaintenanceRecord(long RecordID);
        void DeleteByPart(long PartID);
    }
}
