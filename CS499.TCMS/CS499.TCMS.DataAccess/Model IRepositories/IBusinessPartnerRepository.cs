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

        User getSingle(String name);
        User getSingle(long ID);

        IEnumerable<BusinessPartner> getPartnersByZipCode(Payroll minimum);
        

    }
}
