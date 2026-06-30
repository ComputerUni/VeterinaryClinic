using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.UI.Pages.Treatments
{
    [Authorize(Roles = "Manager")]
    public class EditTreatmentModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditTreatmentModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public TreatmentDto Treatment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];

            if(!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync($"https://localhost:7037/api/treatments/{id}");

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Treatment = JsonSerializer.Deserialize<TreatmentDto>(content, options) ?? new TreatmentDto();
                return Page();
            }
            else
            {
                Treatment = new TreatmentDto();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];

            if(!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.PutAsJsonAsync($"https://localhost:7037/api/treatments/{Treatment.Id}", Treatment);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Treatments/Index");
            }
            else
            {
                Treatment = new TreatmentDto();
            }
            return Page();
        }

    }
}
