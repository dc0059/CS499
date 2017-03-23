using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    interface IManifestRepository : IRepository<Manifest>
    {
        Manifest GetSingle(long ManifestID);

        IEnumerable<Manifest> GetManifestByDepartureDate(DateTime DepartureTime);
        IEnumerable<Manifest> GetManifestByArrivalDate(DateTime ArrivalDate);
        IEnumerable<Manifest> GetAllByEmployee(long EmployeeID);
        IEnumerable<Manifest> GetAllByIncOrOut(string type);

        void Delete(long ManifestID);
        void DeleteByEmployee(long EmployeeID);
    }
}
