using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Entities.Models
{
    public class WeatherDto
    {
        public string Name { get; set; }
        public MainData Main { get; set; }
        public List<WeatherData> Weather { get; set; }
    }

    public class MainData
    {
        public double Temp { get; set; }
    }

    public class WeatherData
    {
        public string Icon { get; set; }    
        public string Description { get; set; }

        public string LniIcon => Icon switch
        {
            "01d" => "lni-sun",
            "01n" => "lni-night",
            "02d" or "02n" => "lni-cloudy-sun",
            "03d" or "03n" or "04d" or "04n" => "lni-cloud",
            "09d" or "09n" or "10d" or "10n" => "lni-rain-drop",
            "11d" or "11n" => "lni-thunder",
            "13d" or "13n" => "lni-snowfall",
            "50d" or "50n" => "lni-drop",
            _ => "lni-cloud"
        };
    }
}
