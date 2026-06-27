using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;
using X.PagedList;

namespace VeterinaryClinic.UI.Pages.Treatments
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<Treatment> TreatmentList { get; set; } = new List<Treatment>();
        public IPagedList<Treatment> PagedTreatmentList { get; set; }

        [BindProperty]
        public TreatmentDto Treatment { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await client.GetAsync("https://localhost:7037/api/treatments");

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                TreatmentList = JsonSerializer.Deserialize<List<Treatment>>(content, options) ?? new List<Treatment>();
                PagedTreatmentList = TreatmentList.ToPagedList(pageNumber, 7);
            }
            else
            {
                TreatmentList = new List<Treatment>();
            }
            return Page();
        }
    }
}
