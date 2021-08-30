using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReadWeather.Filters;
using ReadWeather.Models;
using ReadWeather.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReadWeather.Controllers
{
    //[AuthenticationFilter]
    public class WeatherController : Controller
    {
       
        //Injecting Databse Service 
        private readonly AppDbContext _context;
        public WeatherController(AppDbContext context )
        {

            _context = context;
    
        }

        // GET: Weathers
        public async Task<IActionResult> Index()
        {
            var newWeather = new Weather();
            var weatherModel = new List<Weather>();
            var w = FromApi();
            newWeather.Id = new Guid();
            newWeather.humidity = w.humidity;
            newWeather.temperature = w.temperature;
            newWeather.min_temperature = w.min_temperature;
            newWeather.max_temperature = w.max_temperature;
            weatherModel.Add(newWeather);
            weatherModel.AddRange(await _context.Weather.ToListAsync());
            return View(weatherModel);
        }

        //Getting JsonData and convert From the API URL
        //Deserialize Json Object 
        public WeatherView FromApi()
        {
            var weather = new WeatherView();
            try
            {
                var w = new WeatherView();
                var httpClient1 = new HttpClient();
                var apiUrl = "http://demo4567044.mockable.io/";
                httpClient1.BaseAddress = new Uri(apiUrl);
                var response = httpClient1.GetAsync("weather").Result;
                if (response.IsSuccessStatusCode)
                {
                    var message = response.Content.ReadAsStringAsync().Result;
                    weather = JsonConvert.DeserializeObject<WeatherView>(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return weather;
        }

        // GET: Weathers/Create View
        [AuthenticationFilter]
        // Users Can not Autherizeed for the Create Data
        public IActionResult Create()
        {
            
            return View();
        }

        //POST: Weathers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("humidity,temperature,min_temperature,max_temperature")] 
        Weather weather)
        {
            if (ModelState.IsValid)
            {
                weather.Id = Guid.NewGuid();
                _context.Add(weather);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(weather);
        }
        private bool WeatherExists(Guid id)
        {
            return _context.Weather.Any(e => e.Id == id);
        }

    }
}
