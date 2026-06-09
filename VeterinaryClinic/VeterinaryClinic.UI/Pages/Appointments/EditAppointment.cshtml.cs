using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.UI.Pages.Appointments
{
    public class EditAppointmentModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditAppointmentModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public AppointmentDto Appointment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync($"https://localhost:7037/api/appointments/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                Appointment = JsonSerializer.Deserialize<AppointmentDto>(content, options) ?? new AppointmentDto();
                return Page();
            }
            else
            {
                Appointment = new AppointmentDto();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.PutAsJsonAsync($"https://localhost:7037/api/appointments/{Appointment.Id}", Appointment);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Appointments/Index");
            }
            else
            {
                Appointment = new AppointmentDto(); 
            }
            return Page();
        }
    }
}
