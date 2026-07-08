using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using VeterinaryClinic.Entities.Concrete;
using VeterinaryClinic.Entities.Models;
using X.PagedList;

namespace VeterinaryClinic.UI.Pages.Animals
{
    [Authorize(Roles = "Customer")]
    public class MyAnimalsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MyAnimalsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<Animal> MyAnimalList { get; set; } = new List<Animal>();
        
        public IPagedList<Animal> PagedMyAnimalList { get; set; }

        [BindProperty]
        public AnimalDto Animal { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            if(!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 
            }
            var response = await client.GetAsync("https://localhost:7037/api/animals/my-animals");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                MyAnimalList = JsonSerializer.Deserialize<List<Animal>>(content, options) ?? new List<Animal>();
                PagedMyAnimalList = MyAnimalList.ToPagedList(pageNumber, 5);
            }
            else
            {
                MyAnimalList = new List<Animal>();
            }
            return Page();
        }
    }
}
