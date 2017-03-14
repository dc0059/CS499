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
        Manifest getSingle(long ManifestID);

        IEnumerable<Manifest> getManifestByDepartureDate(DateTime DepartureTime);
        IEnumerable<Manifest> getManifestByArrivalDate(DateTime ArrivalDate);
        IEnumerable<Manifest> getAllByEmployee(long EmployeeID);
        IEnumerable<Manifest> getAllByIncOrOut(string type);

        Manifest Delete(long ManifestID);
        Manifest DeleteByEmployee(long EmployeeID);
    }
}
