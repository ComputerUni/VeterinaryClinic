using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.Business.Abstract
{
    public interface IReportService
    {
        Task<DashboardReportDto> GetDashboardReportAsync();
    }
}
