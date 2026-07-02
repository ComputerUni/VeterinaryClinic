using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;
using X.PagedList;

namespace VeterinaryClinic.UI.Pages.Payments
{
    [Authorize(Roles = "Customer")]
    public class MyPaymentsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MyPaymentsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<Payment> PaymentList { get; set; } = new List<Payment>();
        public IPagedList<Payment> PagedMyPaymentList { get; set; }

        [BindProperty]
        public PaymentDto Payment { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync("https://localhost:7037/api/payments/my-payments");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                PaymentList = JsonSerializer.Deserialize<List<Payment>>(content, options) ?? new List<Payment>();
            }
            else
            {
                PaymentList = new List<Payment>();
            }

            PagedMyPaymentList = PaymentList.ToPagedList(pageNumber, 7);

            return Page();


        }
    }
}
