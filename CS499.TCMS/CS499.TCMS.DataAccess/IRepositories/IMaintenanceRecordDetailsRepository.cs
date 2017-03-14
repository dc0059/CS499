using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    interface IMaintenanceRecordDetailsRepository : IRepository<MaintenanceRecordDetails>
    {
        MaintenanceRecordDetails getSingle(long DetailID);

        IEnumerable<MaintenanceRecordDetails> getDetailsByEmployee(long EmployeeID);
        IEnumerable<MaintenanceRecordDetails> getDetailsByMaintenanceID(long MaintenanceID);
        IEnumerable<MaintenanceRecordDetails> getDetailsByDate(DateTime Earliest, DateTime Latest);

        void Delete(long DetailID);
        void DeleteByEmployee(long EmployeeID);
        void DeleteByMaintenanceID(long MaintenanceID);
        void DeleteByDate(DateTime Earliest, DateTime Latest);
    }
}
