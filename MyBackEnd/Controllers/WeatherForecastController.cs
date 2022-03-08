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
        private readonly IOrder _daprClient;

        public WeatherForecastController(IOrder daprClient, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetWeather()
        {
            try
            {
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = _daprClient.GetOrder().Result //Summaries[Random.Shared.Next(Summaries.Length)]
                })
            .ToArray();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Enumerable.Empty<WeatherForecast>();
            }
        }
    }
}   