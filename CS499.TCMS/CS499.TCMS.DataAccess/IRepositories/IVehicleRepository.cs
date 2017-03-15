using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess
{
    public interface IVehicleRepository : IRepository<Vehicle>, IRepositoryBase
    {
        //Vehicle getSingle(long VehicleID);

        /// <summary>
        /// Method to get every vehicle by its specifications (Brand, Model, Year)
        /// </summary>
        /// <param name="Brand">string representing the make of the vehicle</param>
        /// <param name="Model">string representing the model of the vehicle</param>
        /// <param name="Year">4 digit integer representing the year of the model</param>
        /// <returns>the object based on the ID or null if its not found</returns>
        IEnumerable<Vehicle> getVehiclesBySpecs(string Brand = null, string Model = null, int Year = 2000);
        IEnumerable<Vehicle> getVehiclesByType(string VehicleType);
        IEnumerable<Vehicle> getVehiclesByCapacity(int CapacityClass);

        //void Delete(long VehicleID);
        void DeleteBySpecs(string Brand = null, string Model = null, int Year = 2000);
        void DeleteByType(string VehicleType);
        void DeleteByCapacity(int CapacityClass);
    }
}
