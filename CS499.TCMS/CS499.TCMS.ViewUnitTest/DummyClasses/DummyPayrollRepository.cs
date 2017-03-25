using CS499.TCMS.DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.DataAccess;
using CS499.TCMS.Model;
using System.Diagnostics;

namespace CS499.TCMS.ViewUnitTest.DummyClasses
{
    public class DummyPayrollRepository : IPayrollRepository
    {
        void IRepository<Payroll>.Delete(Payroll model)
        {
            Debug.Print("Delete user with EmployeeID = {0}", model.EmployeeID);
        }

        void IPayrollRepository.Delete(long PayrollID)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Payroll> IRepository<Payroll>.GetAll()
        {
            Debug.Print("GetAll users");
            return new List<Payroll>();
        }

        Payroll IRepository<Payroll>.GetSingle(object id)
        {
            Debug.Print("GetSingle payroll with PayrollID = {0}", id);
            return new Payroll((long)id, 100, DateTime.Now, 800, 80);
        }

        IEnumerable<Payroll> IPayrollRepository.GetStubsByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Payroll> IPayrollRepository.GetStubsByEmployee(long EmployeeID)
        {
            throw new NotImplementedException();
        }

        void IRepository<Payroll>.Insert(Payroll model)
        {
            Debug.Print("Insert payroll with PayrollID = {0}", model.PayrollID);
        }

        void IRepository<Payroll>.Update(Payroll model)
        {
            Debug.Print("Update payroll with PayrollID = {0}", model.PayrollID);
        }
    }
}
