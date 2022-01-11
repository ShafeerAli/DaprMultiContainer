using MyBackEnd;
using Refit;

namespace MyBackEnd.Services.Interfaces
{
    public interface IWeatherForecast
    {
        [Get("/weather")]
        public Task<IEnumerable<WeatherForecast>> GetWeather();
    }
}
