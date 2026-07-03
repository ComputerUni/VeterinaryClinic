using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.UI.Pages.Payments
{
    [Authorize(Roles = "Manager")]
    public class EditPaymentModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditPaymentModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<Payment> PaymentList { get; set; }
        public List<Appointment> AppointmentList { get; set; }

        [BindProperty]
        public PaymentDto Payment { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var paymentResponse = await client.GetAsync($"https://localhost:7037/api/payments/{id}");

            if (paymentResponse.IsSuccessStatusCode)
            {
                var content = await paymentResponse.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Payment = JsonSerializer.Deserialize<PaymentDto>(content, options) ?? new PaymentDto();
            }

            var appointmentResponse = await client.GetAsync($"https://localhost:7037/api/appointments");

            if (appointmentResponse.IsSuccessStatusCode)
            {
                var content = await appointmentResponse.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                AppointmentList = JsonSerializer.Deserialize<List<Appointment>>(content, options) ?? new List<Appointment>(); ;
            }
            else
            {
                AppointmentList = new List<Appointment>();
            }

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

            var response = await client.PutAsJsonAsync($"https://localhost:7037/api/payments/{Payment.Id}", Payment);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Payments/Index");
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
