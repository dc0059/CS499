using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;
using System.Data;

namespace CS499.TCMS.DataAccess.IRepositories
{
    public interface IManifestRepository : IRepository<Manifest>, IRepositoryBase
    {
        //Manifest GetSingle(long ManifestID);

        /// <summary>
        /// Gets the manifest by employee identifier.
        /// </summary>
        /// <param name="employeeID">The employee identifier.</param>
        /// <returns>a <see cref="DataTable"/> containing the manifest information</returns>
        DataTable GetManifestByEmployeeID(long employeeID);

        IEnumerable<Manifest> GetManifestByDepartureDate(DateTime DepartureTime);
        IEnumerable<Manifest> GetManifestByArrivalDate(DateTime ArrivalDate);
        IEnumerable<Manifest> GetAllByEmployee(long EmployeeID);
        IEnumerable<Manifest> GetAllByIncOrOut(string type);

        void Delete(long ManifestID);
        void DeleteByEmployee(long EmployeeID);
    }
}
