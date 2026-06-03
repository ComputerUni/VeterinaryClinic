using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using VeterinaryClinic.Business.Concrete;
using VeterinaryClinic.DataAccess.EntityFramework;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;

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
        public WeatherDto Weather { get; set; } 

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

            var weatherResponse = await client.GetAsync("https://localhost:7037/api/external/weather/city/Gaziantep");

            if(weatherResponse.IsSuccessStatusCode)
            {
                var content = await weatherResponse.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Weather = JsonSerializer.Deserialize<WeatherDto>(content, options) ?? new WeatherDto();
            }
            else
            {
                Weather = new WeatherDto();
            }

            return Page();
        }
    }
}
