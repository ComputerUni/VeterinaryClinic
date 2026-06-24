using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json.Serialization;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.UI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(IHttpClientFactory httpClientFactory, ILogger<LoginModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [BindProperty]
        public LoginDto LoginInput { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("Giriş denemesi başlatıldı: {Username}", LoginInput.Username);
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("https://localhost:7037/api/users/login", LoginInput);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(content,
                    new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                HttpContext.Response.Cookies.Append("JwtToken", result.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    IsEssential = true,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

                var handler = new JwtSecurityTokenHandler();
                handler.InboundClaimTypeMap.Clear();
                var jwtToken = handler.ReadJwtToken(result.Token);

                var role = jwtToken.Claims.FirstOrDefault(c => c.Type == "role"
                || c.Type == ClaimTypes.Role)?.Value;

                var sub = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == ClaimTypes.NameIdentifier)?.Value;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, sub ?? ""),
                    new Claim(ClaimTypes.Name, LoginInput.Username),
                    new Claim(ClaimTypes.Role, role ?? "")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                    });

                _logger.LogInformation("Giriş başarılı. Kullanıcı: {Username}, Rol: {Role}", LoginInput.Username, role);
                var allClaims = string.Join(", ", jwtToken.Claims.Select(c => $"{c.Type}={c.Value}"));
                _logger.LogWarning("TÜM CLAIMLER: {Claims}", allClaims);

                if (role == "Manager")
                {
                    return RedirectToPage("/Animals/Animals");
                }
                else
                {
                    return RedirectToPage("/Appointments/MyAppointment");
                }

            }
            _logger.LogWarning("Giriş başarısız. Kullanıcı: {Username}, Status: {StatusCode}", LoginInput.Username, response.StatusCode);

            ErrorMessage = $"Status: {response.StatusCode} | {content}";

            return Page();
        }

        public class LoginResponse
        {
            [JsonPropertyName("token")]
            public string Token { get; set; }
        }
    }
}
