using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Demo_Microservice
{
    public class WeatherClient
    {
        private readonly HttpClient _httpClient;
        private readonly ServiceSettings _settings; 

        public WeatherClient(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            this._httpClient = httpClient;
            this._settings = options.Value;
        }

        public record Weather(string description);

        public record Main(decimal temp);

        public record Forecast (Weather[] weather,Main main,long dt);

        public async Task<Forecast> GetCurrentWeatherAsync(string city)
        {
            var forecast = await _httpClient.GetFromJsonAsync<Forecast>($"https://{_settings.OpenWeatherHost}/data/2.5/weather?q={city}&appid={_settings.ApiKey}");
            return forecast;
        }
    }
}
