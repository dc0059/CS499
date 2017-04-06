using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolKit.Data;
using CS499.TCMS.Model;

namespace CS499.TCMS.DataAccess.IRepositories
{
    public interface IReportRepository: IRepositoryBase
    {
        DataTable GetPayrollReport(DateTime startDate, DateTime endDate);
        DataTable GetMaintenanceCostReport();
        DataTable GetVehicleMaintenanceReport(Vehicle model);
        DataTable GetIncomingShipmentReport();
        DataTable GetOutgoingShipmentReport();
    }
}
