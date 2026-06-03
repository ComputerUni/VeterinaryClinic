using Microsoft.AspNetCore.Mvc;

namespace VeterinaryClinic.API.Controllers
{
    [Route("api/external/weather")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private string API_KEY = "YOUR_API_KEY";
        private string BASE_URL = "https://api.openweathermap.org/data/2.5/weather";

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("city/{city_name}")]
        public async Task<IActionResult> GetWeatherByCity(string city_name)
        {
            _logger.LogInformation("Hava durumu isteği alındı.", city_name);
            using var httpClient = new HttpClient();
            var url = $"{BASE_URL}?q={city_name}&appid={API_KEY}&units=metric";
            var response = await httpClient.GetAsync(url);
            try
            {
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Content(content, "application/json");
                }
                _logger.LogWarning("Hava durumu verisi alınamadı.");
                return StatusCode((int)response.StatusCode, "Error fetching weather data");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Hata oluştu");
                return StatusCode((int)response.StatusCode, "Error fetching weather data");
            }


        }
    }
}
