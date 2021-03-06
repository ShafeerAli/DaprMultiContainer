using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFrontEnd.Services.Interfaces;

namespace MyFrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWeatherForecast _weatherForecast;

        public IndexModel(IWeatherForecast weatherForecast, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _weatherForecast = weatherForecast;
        }

        public async Task OnGet()
        {
            try
            {
                var forecasts = await _weatherForecast.GetWeatherForecast();
                ViewData["WeatherForecastData"] = forecasts;
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex;
                throw;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            
        }
    }
}