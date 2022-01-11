using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using MyBackEnd.Services.Interfaces;

namespace MyBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecast _daprClient;

        public WeatherForecastController(IWeatherForecast daprClient, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            try
            {
                return await _daprClient.GetWeather();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<WeatherForecast>();
            }
        }
    }
}   