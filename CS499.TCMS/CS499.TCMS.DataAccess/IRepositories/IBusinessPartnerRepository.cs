using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    interface IBusinessPartnerRepository : IRepository<BusinessPartner>, IRepositoryBase
    {

        BusinessPartner getSingle(string PartnerName);
        BusinessPartner getSingle(long PartnerID);

        IEnumerable<BusinessPartner> getPartnersByZipCode(int Zip);
        IEnumerable<BusinessPartner> getPartnersByState(string State);

        void Delete(string PartnerName);
        void Delete(long PartnerID);
        void DeleteByZipCode(int Zip);
        void DeleteByState(string State);

    }
}
