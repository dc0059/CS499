using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.Model_IRepositories
{
    interface IVehicleRepository : IRepository<Vehicle>
    {
        Vehicle getSingle(long VehicleID);

        /// <summary>
        /// Method to get every vehicle by its specifications (Brand, Model, Year)
        /// </summary>
        /// <param name="Brand">string representing the make of the vehicle</param>
        /// <param name="Model">string representing the model of the vehicle</param>
        /// <param name="Year">4 digit integer representing the year of the model</param>
        /// <returns>the object based on the ID or null if its not found</returns>
        IEnumerable<Vehicle> getVehicleBySpecs(string Brand = null, string Model = null, int Year = 2000);
        IEnumerable<Vehicle> getVehicleByType(string VehicleType);
        IEnumerable<Vehicle> getVehicleByCapacity(int CapacityClass);

        void Delete(long VehicleID);
        void DeleteBySpecs(string Brand = null, string Model = null, int Year = 2000);
        void DeleteByType(string VehicleType);
        void DeleteByCapacity(int CapacityClass);
    }
}
