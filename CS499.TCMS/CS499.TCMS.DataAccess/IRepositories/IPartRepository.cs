using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    public interface IPartRepository : IRepository<Part>, IRepositoryBase
    {

        //Part GetSingle(long PartID);

        IEnumerable<Part> GetPartsByNumber(long PartNum);

        /// <summary>
        /// Method to Get all objects in the list with quantity > 0
        /// </summary>
        /// <returns>a list of all objects with quanity greater than 0</returns>
        IEnumerable<Part> GetPartsByAvailability();

        void Delete(long PartID);
    }
}
