using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    public interface IMaintenanceRecordDetailsRepository : IRepository<MaintenanceRecordDetails>
    {
        MaintenanceRecordDetails GetSingle(long DetailID);

        IEnumerable<MaintenanceRecordDetails> GetDetailsByEmployee(long EmployeeID);
        IEnumerable<MaintenanceRecordDetails> GetDetailsByMaintenanceID(long MaintenanceID);
        IEnumerable<MaintenanceRecordDetails> GetDetailsByDate(DateTime Earliest, DateTime Latest);

        void Delete(long DetailID);
        void DeleteByEmployee(long EmployeeID);
        void DeleteByMaintenanceID(long MaintenanceID);
        void DeleteByDate(DateTime Earliest, DateTime Latest);
    }
}
