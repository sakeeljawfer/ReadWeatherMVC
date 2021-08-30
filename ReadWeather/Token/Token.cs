using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReadWeather.Token
{
    public class Token
    {
        [Key] 
        public Guid TokenValue { get; set; }
    }
}
