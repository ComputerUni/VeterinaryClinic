using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Net.Http.Headers;
using VeterinaryClinic.Business.Concrete;
using VeterinaryClinic.DataAccess.EntityFramework;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;
using System.Text;
using X.PagedList;

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
        public IPagedList<Animal> PagedAnimalList { get; set; }

        [BindProperty]
        public AnimalDto Animal { get; set; }

        public WeatherDto Weather { get; set; } 

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await client.GetAsync("https://localhost:7037/api/animals");
            

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                AnimalList = JsonSerializer.Deserialize<List<Animal>>(content, options) ?? new List<Animal>();
                PagedAnimalList = AnimalList.ToPagedList(pageNumber, 2);
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


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];

            if(!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.DeleteAsync($"https://localhost:7037/api/animals/{id}");

            if(response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Animals/Animals");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Silerken hata oluştu");
                return Page();
            }
        }
    }
}
