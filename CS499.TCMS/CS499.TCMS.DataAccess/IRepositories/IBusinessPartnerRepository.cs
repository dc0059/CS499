using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    public interface IBusinessPartnerRepository : IRepository<BusinessPartner>, IRepositoryBase
    {

        BusinessPartner GetSingle(string PartnerName);
        //BusinessPartner GetSingle(long PartnerID);

        IEnumerable<BusinessPartner> GetPartnersByZipCode(int Zip);
        IEnumerable<BusinessPartner> GetPartnersByState(string State);

        void Delete(string PartnerName);
        void Delete(long PartnerID);
        void DeleteByZipCode(int Zip);
        void DeleteByState(string State);

    }
}
