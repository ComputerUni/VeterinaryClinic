using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.UI.Pages.Dashboard
{
    [Authorize(Roles = "Manager")]
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
   
        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;;
        }

        public DashboardReportDto ReportData { get; set; } = new DashboardReportDto();
        
        public async Task<IActionResult> OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            if(!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync("https://localhost:7037/api/reports/dashboard");

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                ReportData = JsonSerializer.Deserialize<DashboardReportDto>(content, options) ?? new DashboardReportDto(); 
            }

            return Page();

        }
    }
}
