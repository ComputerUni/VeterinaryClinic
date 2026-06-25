using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.UI.Pages.Appointments
{
    [Authorize(Roles = "Manager, Customer")]
    public class AddAppointmentModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AddAppointmentModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public AppointmentDto Appointment { get; set; }
        public List<Animal> MyAnimals { get; set; } = new List<Animal>();
        
        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string apiUrl = User.IsInRole("Manager")
                ? "https://localhost:7037/api/animals/manager-animals"
                : "https://localhost:7037/api/animals/my-animals";

            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                MyAnimals = JsonSerializer.Deserialize<List<Animal>>(content, options) ?? new List<Animal>(); 
            }
        }

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
                if (User.IsInRole("Manager"))
                {
                    return RedirectToPage("/Appointments/Index");
                }
                return RedirectToPage("Appointments/MyAppointment");
            }
            else
            {
                Appointment = new AppointmentDto();
            }
            return Page();
        }
    }
}
