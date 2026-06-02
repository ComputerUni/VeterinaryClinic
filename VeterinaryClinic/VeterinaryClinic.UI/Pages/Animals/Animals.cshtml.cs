using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using VeterinaryClinic.Business.Concrete;
using VeterinaryClinic.DataAccess.EntityFramework;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.UI.Pages.Animals
{
    public class AnimalsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AnimalsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<Animal> AnimalList { get; set; } = new List<Animal>();

        public async Task<IActionResult> OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7037/api/animals");
            

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                AnimalList = JsonSerializer.Deserialize<List<Animal>>(content, options) ?? new List<Animal>();
            }
            else
            {
                AnimalList = new List<Animal>();
            }
            return Page();
        }
    }
}
