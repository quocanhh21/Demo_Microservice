﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Microservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherClient _client;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,WeatherClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet]
        [Route("{city}")] // /weather/seattle
        public async Task<WeatherForecast> Get(string city)
        {
            var forecast = await _client.GetCurrentWeatherAsync(city);

            return new WeatherForecast
            {
                Summary = forecast.weather[0].description,
                TemperatureC = (int)forecast.main.temp,
                Date = DateTimeOffset.FromUnixTimeSeconds(forecast.dt).DateTime
            };
        }
    }
}
