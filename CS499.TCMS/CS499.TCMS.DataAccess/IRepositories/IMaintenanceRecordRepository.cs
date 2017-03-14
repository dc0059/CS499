using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    interface IMaintenanceRecordRepository : IRepository<MaintenanceRecord>
    {
        MaintenanceRecord getSingle(long RecordID);

        IEnumerable<MaintenanceRecord> getRecordsByVehicle(long VehicleID);
        IEnumerable<MaintenanceRecord> getRecordsByVehicleAndDate(long VehicleID, DateTime Earliest, DateTime Latest);
        IEnumerable<MaintenanceRecord> getRecordsByDate(DateTime Earliest, DateTime Latest);

        void Delete(long RecordID);
        void DeleteByVehicle(long VehicleID);
        void DeleteByDate(DateTime Earliest, DateTime Latest);
        void DeleteByVehicleAndDate(long VehicleID, DateTime Earliest, DateTime Latest);
    }
}
