using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.UI.Pages.Appointments
{
    public class AddAppointmentModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AddAppointmentModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public AppointmentDto Appointment { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            if(!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await client.PostAsJsonAsync("https://localhost:7037/api/appointments", Appointment);

            if(response.IsSuccessStatusCode)
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
