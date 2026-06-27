using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text.Json;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.UI.Pages.Treatments
{
    public class AddTreatmentModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AddTreatmentModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public TreatmentDto Treatment { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await client.PostAsJsonAsync("https://localhost:7037/api/treatments", Treatment);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Treatment = JsonSerializer.Deserialize<TreatmentDto>(content, options) ?? new TreatmentDto();
                return RedirectToPage("/Treatments/Index");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Hata: {response.StatusCode} - {errorContent}");
            }
            return Page();
        }

    }
}
