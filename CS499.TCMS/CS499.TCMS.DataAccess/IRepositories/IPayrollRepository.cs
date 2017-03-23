using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    public interface IPayrollRepository : IRepository<Payroll>, IRepositoryBase
    {
        //Payroll GetSingle(long PayrollID);

        IEnumerable<Payroll> GetStubsByEmployee(long EmployeeID);
        IEnumerable<Payroll> GetStubsByDate(DateTime date);

        void Delete(long PayrollID);
    }
}
