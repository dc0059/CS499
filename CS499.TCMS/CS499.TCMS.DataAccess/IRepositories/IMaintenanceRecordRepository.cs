using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    public interface IMaintenanceRecordRepository : IRepository<MaintenanceRecord>, IRepositoryBase
    {
        //MaintenanceRecord GetSingle(long RecordID);

        IEnumerable<MaintenanceRecord> GetRecordsByVehicle(long VehicleID);
        IEnumerable<MaintenanceRecord> GetRecordsByVehicleAndDate(long VehicleID, DateTime Earliest, DateTime Latest);
        IEnumerable<MaintenanceRecord> GetRecordsByDate(DateTime Earliest, DateTime Latest);

        void Delete(long RecordID);
        void DeleteByVehicle(long VehicleID);
        void DeleteByDate(DateTime Earliest, DateTime Latest);
        void DeleteByVehicleAndDate(long VehicleID, DateTime Earliest, DateTime Latest);
    }
}
