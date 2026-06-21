using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.UI.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public RegisterDto RegisterInput { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("https://localhost:7037/api/users/register", RegisterInput);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Login");
            }

            var content = await response.Content.ReadAsStringAsync();
            ErrorMessage = "Kayıt işlemi başarısız : " + content;

            return Page();
        }

    }
}
