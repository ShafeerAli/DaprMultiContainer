using Refit;

namespace MyFrontEnd.Services.Interfaces
{
    public interface IWeatherForecast
    {
        [Get("/weatherforecast")]
        public Task<IEnumerable<WeatherForecast>> GetWeatherForecast();
    }
}
