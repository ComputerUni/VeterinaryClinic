using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.UI.Pages.Animals
{
    public class EditAnimalsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditAnimalsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public AnimalDto Animal { get; set; }

        public SelectList CustomerSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var customerResponse = await client.GetAsync($"https://localhost:7037/api/users/customers");

            if(customerResponse.IsSuccessStatusCode)
            {
                var content = await customerResponse.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var customers = JsonSerializer.Deserialize<List<User>>(content, options);
                CustomerSelectList = new SelectList(customers, "Id", "FullName");
            }
            else
            {
                CustomerSelectList = new SelectList(new List<User>(), "Id", "FullName");
            }


            var response = await client.GetAsync($"https://localhost:7037/api/animals/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                Animal = JsonSerializer.Deserialize<AnimalDto>(content, options) ?? new AnimalDto();
                return Page();
            }
            else
            {
                Animal = new AnimalDto();
            }
            return Page();   
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.PutAsJsonAsync("https://localhost:7037/api/animals" + "/" + Animal.Id, Animal);

            if (response.IsSuccessStatusCode)
            {               
                return RedirectToPage("/Animals/Animals");
            }
            else
            {
                Animal = new AnimalDto();
            }
            return Page();
        }
    }
}
