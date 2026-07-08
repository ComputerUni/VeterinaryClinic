using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text.Json;
using VeterinaryClinic.Business.Validators;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.UI.Pages.Animals
{
    [Authorize(Roles = "Manager")]
    public class AddAnimalsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AddAnimalsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public AnimalDto Animal { get; set; }
        public SelectList CustomerSelectList { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:7037/api/users/customers");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var customers = JsonSerializer.Deserialize<List<User>>(content, options);
                CustomerSelectList = new SelectList(customers, "Id", "FullName");
            }
            else
            {
                CustomerSelectList = new SelectList(new List<User>(), "Id", "FullName");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var validator = new AnimalValidator();
            var result = await validator.ValidateAsync(Animal);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError($"Animal.{error.PropertyName}", error.ErrorMessage);
                }
                await OnGetAsync();
                return Page();
            }


            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.PostAsJsonAsync("https://localhost:7037/api/animals", Animal);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                Animal = JsonSerializer.Deserialize<AnimalDto>(content, options) ?? new AnimalDto();
                return RedirectToPage("/Animals/Animals");

            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Hata: {response.StatusCode} - {errorContent}");
                await OnGetAsync();
            }
            return Page();
        }
    }
}
