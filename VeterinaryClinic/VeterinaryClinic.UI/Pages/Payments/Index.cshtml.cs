using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;
using VeterinaryClinic.Entities.Status;
using X.PagedList;

namespace VeterinaryClinic.UI.Pages.Payments
{
    [Authorize(Roles = "Manager")]
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<Payment> PaymentList { get; set; } = new List<Payment>();
        public IPagedList<Payment> PagedPaymentList { get; set; }

        [BindProperty]
        public PaymentDto Payment { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? StatusFilter { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            if(!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync("https://localhost:7037/api/payments");

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                PaymentList = JsonSerializer.Deserialize<List<Payment>>(content, options) ?? new List<Payment>();


                if (!string.IsNullOrWhiteSpace(StatusFilter) && Enum.TryParse<PaymentStatus>(StatusFilter, out var status))
                {
                    PaymentList = PaymentList
                        .Where(a => a.PaymentMethod == status)
                        .ToList();
                }

            }
            else
            {
                PaymentList = new List<Payment>();
            }

            PagedPaymentList = PaymentList.ToPagedList(pageNumber, 7);

            return Page();
        }
    }
}
