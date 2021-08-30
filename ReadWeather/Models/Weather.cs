using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReadWeather.Models
{
    //Model for Databse retrive data
    public class Weather
{
        [Key] public Guid Id { get; set; }

        [DisplayName("Humidity")]
        public int humidity { get; set; }

        [DisplayName("Temperature")]
        public int temperature { get; set; }

        [DisplayName("Min_temperature")]
        public int min_temperature { get; set; }

        [DisplayName("Max_temperature")]
        public int max_temperature { get; set; }
    }
}
