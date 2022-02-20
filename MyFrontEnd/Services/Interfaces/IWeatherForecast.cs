using Refit;

namespace MyFrontEnd.Services.Interfaces
{
    public interface IWeatherForecast
    {
        [Get("/WeatherForecast/GetWeather")]
        public Task<IEnumerable<WeatherForecast>> GetWeatherForecast();
    }
}
