using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS499.TCMS.Model;
using CS499.TCMS.DataAccess;
using CS499.TCMS.DataAccess.IRepositories;

namespace CS499.TCMS.DataAccessUnitTests
{

    [TestClass]
    public class DataAccess_Payroll_Tests
    {
        [TestMethod]
        public void PayrollInsertTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPayrollRepository payRepository = factory.Create<IPayrollRepository>();

            Payroll insertPay = new Payroll(123, 1, new DateTime(2017, 3, 26), 5500, 55.5);

            payRepository.Insert(insertPay);
        }

        [TestMethod]
        public void PayrollGetSingleTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPayrollRepository payRepository = factory.Create<IPayrollRepository>();

            Payroll returnPay = payRepository.GetSingle(5);

            Assert.IsTrue(returnPay.IsValid);
            System.Diagnostics.Debug.Print(returnPay.PayrollID.ToString());
            System.Diagnostics.Debug.Print(returnPay.EmployeeID.ToString());
            System.Diagnostics.Debug.Print(returnPay.PaymentDate.ToString());
            System.Diagnostics.Debug.Print(returnPay.Payment.ToString());
            System.Diagnostics.Debug.Print(returnPay.HoursWorked.ToString());
        }

        [TestMethod]
        public void PayrollGetAllTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPayrollRepository payRepository = factory.Create<IPayrollRepository>();

            foreach(Payroll x in payRepository.GetAll())
            {
                Assert.IsTrue(x.IsValid);
                System.Diagnostics.Debug.Print(x.PayrollID.ToString());
                System.Diagnostics.Debug.Print(x.EmployeeID.ToString());
                System.Diagnostics.Debug.Print(x.PaymentDate.ToString());
                System.Diagnostics.Debug.Print(x.Payment.ToString());
                System.Diagnostics.Debug.Print(x.HoursWorked.ToString());
            }
        }

        [TestMethod]
        public void PayrollUpdateTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPayrollRepository payRepository = factory.Create<IPayrollRepository>();

            Payroll updatePay = new Payroll(5, 1, new DateTime(2015, 3, 5), 100.5, 5);

            payRepository.Update(updatePay);
        }

        [TestMethod]
        public void PayrollDeleteTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IPayrollRepository payRepository = factory.Create<IPayrollRepository>();

            Payroll delPay = payRepository.GetSingle(5);

            payRepository.Delete(delPay);
        }
    }
}
