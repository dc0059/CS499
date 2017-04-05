using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS499.TCMS.DataAccess;
using CS499.TCMS.DataAccess.IRepositories;
using System.Data;

namespace CS499.TCMS.DataAccessUnitTests
{
    [TestClass]
    public class DataAccess_Form_Tests
    {
        [TestMethod]
        public void PayrollDataTableTest()
        {
            RepositoryFactory factory = new RepositoryFactory("johnsza", "cs_499_tcms");
            IReportRepository formRepo = factory.Create<IReportRepository>();

            DataTable table = formRepo.GetPayrollReport(new DateTime(2017,3,25));

            foreach(DataRow row in table.Rows)
            {
                System.Diagnostics.Debug.Print("");
                for(int x = 0; x < table.Columns.Count; x++)
                {
                    System.Diagnostics.Debug.Print(row[x].ToString() + " ");
                }
            }
        }
    }
}
