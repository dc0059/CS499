using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    interface IPayrollRepository : IRepository<Payroll>
    {
        Payroll getSingle(long PayrollID);

        IEnumerable<Payroll> getStubsByEmployee(long EmployeeID);
        IEnumerable<Payroll> getStubsByDate(DateTime date);

        void Delete(long PayrollID);
    }
}
