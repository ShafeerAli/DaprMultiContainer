using Refit;

namespace MyFrontEnd.Services.Interfaces
{
    public interface IWeatherForecast
    {
        [Get("/WeatherForecast")]
        public Task<IEnumerable<WeatherForecast>> GetWeatherForecast();
    }
}
