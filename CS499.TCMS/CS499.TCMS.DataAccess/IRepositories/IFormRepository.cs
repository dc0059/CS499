using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolKit.Data;

namespace CS499.TCMS.DataAccess.IRepositories
{
    public interface IReportRepository: IRepositoryBase
    {
        DataTable GetPayrollReport(DateTime date);
        DataTable GetMaintenanceCostReport();
        DataTable GetVehicleMaintenanceReport();
        DataTable GetIncomingShipmentReport();
        DataTable GetOutgoingShipmentReport();
    }
}
