using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.UI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public LoginDto LoginInput { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("https://localhost:7037/api/user/login", LoginInput);
            var content = await response.Content.ReadAsStringAsync();
            
            if (response.IsSuccessStatusCode)
            {
                var result = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(content,
                    new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true});
                HttpContext.Response.Cookies.Append("JwtToken", result.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,                        
                    SameSite = SameSiteMode.Strict,     
                    IsEssential = true,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

                return RedirectToPage("/Animals/Animals");
            }

            ErrorMessage = $"Status: {response.StatusCode} | {content}";

            return Page();
        }

        public class LoginResponse
        {
            public string Token { get; set; }   
        }
    }
}
