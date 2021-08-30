using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadWeather.Models
{
    //Model for API retrive data
    public class WeatherView
    {
        [JsonProperty("humidity")] public int humidity { get; set; }

        [JsonProperty("temperature")] public int temperature { get; set; }

        [JsonProperty("min_temperature")] public int min_temperature { get; set; }

        [JsonProperty("max_temperature")] public int max_temperature { get; set; }
    }
}
