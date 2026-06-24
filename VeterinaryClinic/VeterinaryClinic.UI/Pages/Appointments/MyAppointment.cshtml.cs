using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;
using X.PagedList;

namespace VeterinaryClinic.UI.Pages.Appointments
{
    [Authorize(Roles = "Customer")]
    public class MyAppointmentModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MyAppointmentModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<Appointment> AppointmentList { get; set; } = new List<Appointment>();
        public IPagedList<Appointment> PagedMyAppointmentList { get; set; }

        [BindProperty]
        public AppointmentDto Appointment { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            if(!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync("https://localhost:7037/api/appointments/my-appointments");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                AppointmentList = JsonSerializer.Deserialize<List<Appointment>>(content, options) ?? new List<Appointment>();
            }
            else
            {
                AppointmentList = new List<Appointment>(); 
            }

            PagedMyAppointmentList = AppointmentList.ToPagedList(pageNumber, 7);

            return Page();
        }
    }
}
