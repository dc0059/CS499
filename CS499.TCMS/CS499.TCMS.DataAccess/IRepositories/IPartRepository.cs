using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess
{
    public interface IPartRepository : IRepository<Part>, IRepositoryBase
    {

        IEnumerable<Part> getPartsByNumber(long PartNum);

        /// <summary>
        /// Method to get all objects in the list with quantity > 0
        /// </summary>
        /// <returns>a list of all objects with quanity greater than 0</returns>
        IEnumerable<Part> getPartsByAvailability();

        void Delete(long PartID);
    }
}
