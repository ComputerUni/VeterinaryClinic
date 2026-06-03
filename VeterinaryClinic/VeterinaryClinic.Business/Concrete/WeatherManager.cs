using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.Business.Concrete
{
    public class WeatherManager : IWeatherService
    {
        IWeatherService _weatherService;

        public WeatherManager(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<WeatherInfo> GetWeatherInfoAsync(string cityName)
        {
            return await _weatherService.GetWeatherInfoAsync(cityName);
        }
    }
}
