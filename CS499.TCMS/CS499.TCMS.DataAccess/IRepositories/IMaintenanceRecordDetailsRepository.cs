using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    public interface IMaintenanceRecordDetailsRepository : IRepository<MaintenanceRecordDetail>, IRepositoryBase
    {
        //MaintenanceRecordDetails GetSingle(long DetailID);

        IEnumerable<MaintenanceRecordDetail> GetDetailsByEmployee(long EmployeeID);
        IEnumerable<MaintenanceRecordDetail> GetDetailsByMaintenanceID(long MaintenanceID);
        IEnumerable<MaintenanceRecordDetail> GetDetailsByDate(DateTime Earliest, DateTime Latest);

        void Delete(long DetailID);
        void DeleteByEmployee(long EmployeeID);
        void DeleteByMaintenanceID(long MaintenanceID);
        void DeleteByDate(DateTime Earliest, DateTime Latest);
    }
}
