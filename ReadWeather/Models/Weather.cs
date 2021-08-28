using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadWeather.Models
{
    public class Weather
    {
        public float humidity { get; set; }

        public float temperature { get; set; }

        public float min_temperature { get; set; }

        public float max_temperature { get; set; }
    }
}
