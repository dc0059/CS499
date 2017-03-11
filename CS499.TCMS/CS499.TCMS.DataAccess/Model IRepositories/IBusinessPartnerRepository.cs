using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.Model_IRepositories
{
    interface IBusinessPartnerRepository : IRepository<BusinessPartner>
    {

        User getSingle(string PartnerName);
        User getSingle(long PartnerID);

        IEnumerable<BusinessPartner> getPartnersByZipCode(int Zip);
        IEnumerable<BusinessPartner> getPartnersByState(string State);

        void Delete(string PartnerName);
        void Delete(long PartnerID);
        void DeleteByZipCode(int Zip);
        void DeleteByState(string State);

    }
}
