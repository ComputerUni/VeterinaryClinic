using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VeterinaryClinic.Business.Abstract;

namespace VeterinaryClinic.API.Controllers
{
    [Route("api/reports")]
    public class ReportsController : BaseController
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("dashboard")]
        [Authorize(Roles = "Manager")] 
        public async Task<IActionResult> GetDashboardReport()
        {
            var report = await _reportService.GetDashboardReportAsync();
            return Ok(report);
        }
    }
}