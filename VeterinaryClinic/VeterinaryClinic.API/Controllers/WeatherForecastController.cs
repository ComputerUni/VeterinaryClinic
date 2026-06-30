using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VeterinaryClinic.API.Controllers
{
    [Route("api/external/weather")]
    public class WeatherForecastController : BaseController
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("city/{city_name}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWeatherByCity(string city_name)
        {
            _logger.LogInformation("Hava durumu isteği alındı.", city_name);
            using var httpClient = new HttpClient();
            var apiKey = _configuration["OpenWeather:API_KEY"];
            var baseUrl = _configuration["OpenWeather:BASE_URL"];
            var url = $"{baseUrl}?q={city_name}&appid={apiKey}&units=metric";
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
