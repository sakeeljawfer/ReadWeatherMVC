using Microsoft.AspNetCore.Mvc;
using ReadWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReadWeather.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public IEnumerable<Weather> Weather { get; set; }

        const string BASE_URL = "http://demo4567044.mockable.io/weather";
        public WeatherController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri($"{BASE_URL}api/Weather");
            message.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                Weather = await JsonSerializer.DeserializeAsync<IEnumerable<Weather>>(responseStream);
            }
            else
            {
                Weather = Array.Empty<Weather>();
            }

            return View(Weather);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("humidity,temperature,min_temperature,max_temperature")] Weather student)
        {
            if (ModelState.IsValid)
            {
                HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(student), Encoding.UTF8);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var message = new HttpRequestMessage();
                message.Content = httpContent;
                message.Method = HttpMethod.Post;
                message.RequestUri = new Uri($"{BASE_URL}api/Weather");

                HttpClient client = _clientFactory.CreateClient();
                HttpResponseMessage response = await client.SendAsync(message);

                var result = await response.Content.ReadAsStringAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(Weather);
        }

    }
}
