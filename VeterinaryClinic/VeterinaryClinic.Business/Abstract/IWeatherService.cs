using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.Business.Abstract
{
    public interface IWeatherService
    {
        Task<WeatherInfo> GetWeatherInfoAsync(string cityName);
    }
}
